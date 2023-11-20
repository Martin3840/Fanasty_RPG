using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WAIT, WON, LOST }
public class BattleSystem : MonoBehaviour
{
    public GameObject titleMessage;
    public GameObject textPrefab;
    public GameObject playerObject;
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
    List<GameObject> turnOrder = new List<GameObject>();
    List<RPGStats> party = new List<RPGStats>();

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
        for (int i = 0; i < party.Count; i++) {
            newStats = Instantiate(playerPrefab,playerLocation[i]);
            newStats.GetComponent<CombatStats>().stats = party[i];
            newStats.GetComponent<CombatStats>().UpdateStats();
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

        SortTurn();

        yield return new WaitForSeconds(2f);
    }
    
    IEnumerator PlayerAttack(GameObject attacker, GameObject target)
    {
        attacker.transform.Translate(new(0f,0.1f,0f));
        state = BattleState.WAIT;
        bool isDead = target.GetComponent<CombatStats>().TakeDamage(attacker.GetComponent<CombatStats>().damage);

        yield return new WaitForSeconds(0.2f);

        StartCoroutine(ShowDamage(attacker.GetComponent<CombatStats>().damage, target.transform));

        yield return new WaitForSeconds(1f);
        attacker.transform.Translate(new(0f,-0.1f,0f));
        if (isDead)
        {
            Kill(target);
            CheckBattle();
        }
        
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
    IEnumerator ShowDamage(float damage, Transform target)
    {
        Vector3 offset = new(0,1.0f,0.1f);
        GameObject popup = Instantiate(textPrefab,target.position + offset,target.rotation);
        popup.GetComponent<TextMeshPro>().text = "-" + damage.ToString();
        yield return new WaitForSeconds(1f);
        Destroy(popup);
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
        bool isDead = target.GetComponent<CombatStats>().TakeDamage(attacker.GetComponent<CombatStats>().damage);

        yield return new WaitForSeconds(0.2f);

        attacker.transform.Translate(new(0f,-0.1f,0f));
        StartCoroutine(ShowDamage(attacker.GetComponent<CombatStats>().damage, target.transform));

        yield return new WaitForSeconds(1f);
        
        if (isDead)
        {
            Kill(target);
            CheckBattle();
        }

        attacker.GetComponent<CombatStats>().av += 1000 / attacker.GetComponent<CombatStats>().stats.speed;
        SortTurn();
    }
    void EnemyTurn()
    {
        state = BattleState.ENEMYTURN;
        Debug.Log("Enemy's Turns");
        StartCoroutine(EnemyAttack(turnOrder[0],playerParty[Random.Range(0,3)]));
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
            return;
        }
        else if (playerParty.Count <= 0)
        {
            state = BattleState.LOST;
            titleMessage.GetComponent<TextMeshProUGUI>().SetText("You Lost!");
            titleMessage.GetComponent<TextMeshProUGUI>().color = new(226,18,0);
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
        Vector3 offset = new(0f,0.5f,-0.1f);
        Crosshair = Instantiate(CrosshairPrefab,target.transform.position + offset,target.transform.rotation);
    }
}
