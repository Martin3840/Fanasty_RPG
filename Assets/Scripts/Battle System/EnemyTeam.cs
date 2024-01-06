using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeam : MonoBehaviour
{
    public static int total;
    public static event Action<Character> OnEnemySpawned;
    public List<GameObject> party = new();
    void Awake()
    {
        total = party.Count;
        BattleEvent.OnBattleStart += SpawnCharacters;
    }
    
    void SpawnCharacters()
    {
        foreach (GameObject enemy in party)
        {
            Debug.Log("Enemy Spawned!");
            OnEnemySpawned?.Invoke(enemy.GetComponent<Character>());
        }
    }

}
