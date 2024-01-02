using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeam : MonoBehaviour
{
    public static int total;
    public static event Action<GameObject> OnEnemySpawned;
    [System.NonSerialized] public static List<GameObject> combatRoster = new();
    public static int Dead;
    public List<GameObject> party = new();
    void Awake()
    {
        total = party.Count;
        BattleEvent.OnBattleStart += SpawnCharacters;
    }
    
    void SpawnCharacters()
    {
        foreach (GameObject gameObject in party)
        {
            Debug.Log("Enemy Spawned!");
            GameObject newGameObject = Instantiate(gameObject);
            combatRoster.Add(newGameObject);
            OnEnemySpawned?.Invoke(newGameObject);
        }
    }

    public static GameObject GetLeftEnemy(GameObject target)
    {
        int location = combatRoster.IndexOf(target);
        if (location + 1 < combatRoster.Count)
            return combatRoster[location+1];
        else
            return null;
    }

    public static GameObject GetRightEnemy(GameObject target)
    {
        int location = combatRoster.IndexOf(target);
        if (location > 0)
            return combatRoster[location-1];
        else
            return null;
    }
}
