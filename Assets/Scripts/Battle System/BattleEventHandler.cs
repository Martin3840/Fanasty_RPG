using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class BattleEvent : MonoBehaviour
{
    public static event Action OnBattleStart;
    public static event Action<BattleState> OnBattleEnded;

    private List<Character> turnOrder = new();
    public static BattleState state;

    void Awake()
    {
        Character.OnTurnEnded += NextTurn;
        PlayerTeam.OnCharacterSpawned += AddCharacterToTurnOrder;
        EnemyTeam.OnEnemySpawned += AddCharacterToTurnOrder;
        Character.OnStaticDeath += RemoveCharacterFromTurnOrder;
        StartCoroutine(DelayStart());
    }
    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(1.0f);
        UpdateState(BattleState.START);
    }
    void NextTurn(Character character)
    {
        UpdateState(BattleState.WAIT);
        SortTurn();
    }

    void OnDestroy()
    {
        Character.OnTurnEnded -= NextTurn;
        PlayerTeam.OnCharacterSpawned -= AddCharacterToTurnOrder;
        EnemyTeam.OnEnemySpawned -= AddCharacterToTurnOrder;
        Character.OnStaticDeath -= RemoveCharacterFromTurnOrder;
    }

    void UpdateState(BattleState newState)
    {
        state = newState;

        switch (state)
        {
            case BattleState.START:
                OnBattleStart?.Invoke();
                break;
            case BattleState.VICTORY:
                OnBattleEnded?.Invoke(state);
                break;
            case BattleState.LOSE:
                OnBattleEnded?.Invoke(state);
                break;
        }
    }

    void AddCharacterToTurnOrder(Character character)
    {
        Debug.Log("Character Added");
        turnOrder.Add(character);
        if (turnOrder.Count == (EnemyTeam.total + PlayerTeam.total))
        {
            SortTurn();
            Debug.Log("Player Size:" + PlayerTeam.total);
            Debug.Log("Enemy Size:" + EnemyTeam.total);
        }
    }

    void RemoveCharacterFromTurnOrder(Character character)
    {
        turnOrder.Remove(character);
    }

    public void SortTurn()
    {
        int size = turnOrder.Count;
        for (int i = 1; i < size; i++)
        {
            Character key = turnOrder[i];
            int j = i - 1;

            while (j >= 0 && turnOrder[j].actionValue > key.actionValue)
            {
                turnOrder[j+1] = turnOrder[j];
                j--;
            }
            turnOrder[j+1] = key;
        }
        for (int i = size - 1; i >= 0; i--)
        {
            turnOrder[i].actionValue -= turnOrder[0].actionValue;
        }

        if (turnOrder[0].isPlayer)
            UpdateState(BattleState.PLAYERTURN);
        else
            UpdateState(BattleState.ENEMYTURN);
        
        turnOrder[0].TurnStart();
    }
}
public enum BattleState {
    START,
    ENEMYTURN,
    PLAYERTURN,
    WAIT,
    VICTORY,
    LOSE
}
