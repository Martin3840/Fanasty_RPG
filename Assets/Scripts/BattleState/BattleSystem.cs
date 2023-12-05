using System.Collections;
using System.Collections.Generic;
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

    public GameObject playerTarget;
    private GameObject Crosshair = null;
    public GameObject CrosshairPrefab;

    List<GameObject> playerParty = new List<GameObject>();
    List<GameObject> enemyParty = new List<GameObject>();

    GameObject newStats;
    CombatStats newCombatStats;
    List<GameObject> turnOrder = new List<GameObject>();
    List<RPGStats> party = new List<RPGStats>();

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
        party = playerObject.GetComponent<Team>().characters;
        int charUIOffset = 150;
        for (int i = 0; i < party.Count; i++) {
            newStats = Instantiate(playerPrefab,playerLocation[i]);
            newCombatStats = newStats.GetComponent<CombatStats>();

            newCombatStats.stats = party[i];
            newCombatStats.UpdateStats();

            newCharUI = Instantiate(CharacterUIPrefab);
            newCharUI.transform.SetParent(CharacterList.transform);
            newCharUI.transform.localPosition = new Vector3(0, charUIOffset, 0);
            newCharUI.transform.localScale = new Vector2(1,1);
            charUIOffset -= 100;

            newCombatStats.characterUI = newCharUI.GetComponent<CharacterUI>();
            newCombatStats.UpdateUI();

            turnOrder.Add(newStats);
            playerParty.Add(newStats);
        }

        //Setup Enemy
        party = enemyObject.GetComponent<Enemy>().party;
        for (int i = 0; i < party.Count; i++) { 
            newStats = Instantiate(enemyPrefab,enemyLocation[i]);
            newStats.GetComponent<CombatStats>().stats = party[i];
            newStats.GetComponent<CombatStats>().UpdateStats();
            turnOrder.Add(newStats);
            enemyParty.Add(newStats);
        }
        SetTarget(enemyParty[0]);

        yield return new WaitForSeconds(2f);

        SortTurn();
    }
    
    IEnumerator PlayerAttack(GameObject attacker, GameObject target)
    {
        attacker.transform.Translate(new(0f,0.1f,0f));
        state = BattleState.WAIT;
        bool Crit = false;
        float damage = target.GetComponent<CombatStats>().TakeDamage(attacker.GetComponent<CombatStats>().damage, ref Crit);
        StartCoroutine(ShowDamage(damage,Crit,target.transform));

        if (target.GetComponent<CombatStats>().IsDead())
        {
            Kill(target);
        }

        yield return new WaitForSeconds(0.2f);

        attacker.transform.Translate(new(0f,-0.1f,0f));

        yield return new WaitForSeconds(1f);

        CheckBattle();
        
        attacker.GetComponent<CombatStats>().av += 1000 / attacker.GetComponent<CombatStats>().stats.speed;
        SortTurn();
    }
    void Kill(GameObject target)
    {
        if (target.GetComponent<CombatStats>().stats.isPlayer)
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

    IEnumerator EnemyAttack(GameObject attacker, GameObject target)
    {
        attacker.transform.Translate(new(0f,0.1f,0f));
        state = BattleState.WAIT;
        bool Crit = false;
        float damage = target.GetComponent<CombatStats>().TakeDamage(attacker.GetComponent<CombatStats>().damage,ref Crit);
        StartCoroutine(ShowDamage(damage,Crit,target.transform));

        if (target.GetComponent<CombatStats>().IsDead())
        {
            Kill(target);
        }

        yield return new WaitForSeconds(0.2f);

        attacker.transform.Translate(new(0f,-0.1f,0f));

        yield return new WaitForSeconds(1f);
    
        CheckBattle();
        
        attacker.GetComponent<CombatStats>().av += 1000 / attacker.GetComponent<CombatStats>().stats.speed;
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
            GameObject key = turnOrder[i];
            int j = i - 1;

            while (j >= 0 && turnOrder[j].GetComponent<CombatStats>().av > key.GetComponent<CombatStats>().av)
            {
                turnOrder[j+1] = turnOrder[j];
                j--;
            }
            turnOrder[j+1] = key;
        }
        for (int i = size - 1; i >= 0; i--)
        {
            turnOrder[i].GetComponent<CombatStats>().av -= turnOrder[0].GetComponent<CombatStats>().av;
        }

        if (turnOrder[0].GetComponent<CombatStats>().stats.isPlayer) {
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
        playerTarget = target;
        if (Crosshair != null)
        {
            Destroy(Crosshair);
        }
        if (enemyParty.Count <= 0)
            return;
        Vector3 offset = new(0f,0.5f,-0.1f);
        Crosshair = Instantiate(CrosshairPrefab,target.transform.position + offset,target.transform.rotation);
    }

    IEnumerator ShowDamage(float damage, bool crit, Transform target)
    {
        GameObject popup = Instantiate(textPrefab,target.position + new Vector3(0,1.0f,0.1f),target.rotation);
        if (crit) {
            popup.transform.Translate(new Vector3(0,0.5f,0));
            TextMeshPro content = popup.GetComponent<TextMeshPro>();
            content.text = "-" + damage.ToString() + "\nCritical\nDamage";
        }
        else
        {
            popup.GetComponent<TextMeshPro>().text = "-" + damage.ToString();
        }
        yield return new WaitForSeconds(1f);
        Destroy(popup);
    }
}
