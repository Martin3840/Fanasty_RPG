using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WAIT, WON, LOST }
public class BattleSystem : MonoBehaviour
{
    public GameObject world;
    public GameObject titleMessage;
    public GameObject playerObject;
    public GameObject textPrefab;
    public GameObject playerPrefab;
    public Transform[] playerLocation;

    public GameObject enemyObject;
    public GameObject enemyPrefab;
    public Transform[] enemyLocation;

    public BattleState state;

    public Character playerTarget;
    private GameObject Crosshair = null;
    public GameObject CrosshairPrefab;

    List<Character> playerParty = new List<Character>();
    List<Character> enemyParty = new List<Character>();
    List<Character> turnOrder = new List<Character>();

    [SerializeField]
    private GameObject CharacterList;
    [SerializeField]
    private GameObject CharacterUIPrefab;
    GameObject newCharUI;

    // Start is called before the first frame update
    public void StartBattle()
    {
        state = BattleState.START;
        StartCoroutine(Setup());
    }
    IEnumerator Setup()
    {
        //Setup party members location and stats
        int charUIOffset = 150;
        int locationIndex = 0;
        foreach (Character character in playerParty)
        {
            turnOrder.Add(character);


            newCharUI = Instantiate(CharacterUIPrefab);
            newCharUI.transform.SetParent(CharacterList.transform);
            newCharUI.transform.localPosition = new Vector3(0, charUIOffset, 0);
            newCharUI.transform.localScale = new Vector2(1,1);
            charUIOffset -= 100;

            character.characterUI = newCharUI.GetComponent<CharacterUI>();
            character.UpdateUI();
            locationIndex++;
        }

        //Setup Enemy
        foreach (Character enemy in enemyParty) { 
            turnOrder.Add(enemy);
        }
        SetTarget(enemyParty[0]);

        yield return new WaitForSeconds(2f);

        SortTurn();
    }
    
    IEnumerator PlayerAttack(Character attacker, Character target)
    {
        state = BattleState.WAIT;
        float damage = target.TakeDamage(attacker.stats["physical_attack"].Value);

        if (target.IsDead())
        {
            Kill(target);
        }

        yield return new WaitForSeconds(1f);

        CheckBattle();
        
        attacker.av += 1000 / attacker.stats["speed"].Value;
        SortTurn();
    }
    void Kill(Character target)
    {
        if (target.isPlayer)
        {
            playerParty.Remove(target);
        }
        else
        {
            enemyParty.Remove(target);
            if (enemyParty.Count > 0)
                SetTarget(enemyParty[0]);
            else
                Destroy(Crosshair);
        }
        turnOrder.Remove(target);
        Destroy(target);
    }
    void PlayerTurn()
    {
        state = BattleState.PLAYERTURN;
        Debug.Log("Your Turns");
    }

    IEnumerator EnemyAttack(Character attacker, Character target)
    {
        state = BattleState.WAIT;
        float damage = target.TakeDamage(attacker.stats["physical_attack"].Value);

        if (target.IsDead())
        {
            Kill(target);
        }

        yield return new WaitForSeconds(1f);

        CheckBattle();
        
        attacker.TurnEnd();
        SortTurn();
    }
    void EnemyTurn()
    {
        state = BattleState.ENEMYTURN;
        Debug.Log("Enemy's Turns");
        StartCoroutine(EnemyAttack(turnOrder[0],playerParty[Random.Range(0,playerParty.Count-1)]));
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN) return;
        StartCoroutine(PlayerAttack(turnOrder[0],playerTarget));
    }

    void SortTurn()
    {
        int size = turnOrder.Count;
        for (int i = 1; i < size; i++)
        {
            Character key = turnOrder[i];
            int j = i - 1;

            while (j >= 0 && turnOrder[j].av > key.av)
            {
                turnOrder[j+1] = turnOrder[j];
                j--;
            }
            turnOrder[j+1] = key;
        }
        for (int i = size - 1; i >= 0; i--)
        {
            turnOrder[i].av -= turnOrder[0].av;
        }

        if (turnOrder[0].isPlayer) {
            PlayerTurn();
        } else {
            EnemyTurn();
        }
    }
    void CheckBattle()
    {
        if (enemyParty.Count <= 0)
        {
            state = BattleState.WON;
            titleMessage.GetComponent<TextMeshProUGUI>().SetText("Nah, I'd Win!");
            titleMessage.GetComponent<TextMeshProUGUI>().color = new(255,186,20);
            Destroy(enemyObject);
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            world.SetActive(true);
            return;
        }
        else if (playerParty.Count <= 0)
        {
            state = BattleState.LOST;
            titleMessage.GetComponent<TextMeshProUGUI>().SetText("You Lost!");
            titleMessage.GetComponent<TextMeshProUGUI>().color = new(226,18,0);
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            world.SetActive(true);
            playerObject.transform.localPosition = new Vector3(-475,-251,-2);
            return;
        }
    }

    public void SetTarget(GameObject target)
    {
        foreach (Character enemy in enemyParty)
        {
            if (enemy.spriteObject = target)
                playerTarget = enemy;
        }
    }
    public void SetTarget(Character target)
    {
        playerTarget = target;
    }

    public Character GetTarget()
    {
        return playerTarget;
    }

    IEnumerator ShowDamage(float damage, bool crit, Transform target)
    {
        GameObject popup = Instantiate(textPrefab,target.transform);
        if (crit) {
            TextMeshPro content = popup.GetComponent<TextMeshPro>();
            content.text = "-" + damage.ToString() + "\nCritical\nDamage";
        }
        else
        {
            popup.GetComponent<TextMeshPro>().text = "-" + damage.ToString();
        }
        yield return new WaitForSeconds(1f);
    }
    public List<Character> GetEnemies() { return enemyParty; }

    public List<Character> GetPartyMember() { return playerParty; }
}
