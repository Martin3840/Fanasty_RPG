using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYER1, PLAYER2, PLAYER3, PLAYER4, ENEMY1, ENEMY2, ENEMY3, ENEMY4, ENEMY5, WON, LOST }
public class BattleSystem : MonoBehaviour
{
    public GameObject player1;
    public GameObject enemy1;

    public Transform player1Loc;
    public Transform enemy1Loc;

    RPGStats player1Stats;
    RPGStats enemy1Stats;

    public BattleState state;
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(Setup());
    }
    IEnumerator Setup()
    {

        GameObject enemyGO = Instantiate(enemy1,enemy1Loc);
        enemy1Stats = enemyGO.GetComponent<RPGStats>();

        yield return new WaitForSeconds(2f);
        


    }
    void Player1Turn()
    {
        Debug.Log("Your Turns");
    }
}
