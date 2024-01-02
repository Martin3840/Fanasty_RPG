using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTeam : MonoBehaviour
{
    public static int total;
    public static GameObject currentPlayerTarget;
    public static event Action<GameObject> OnCharacterSpawned;
    [System.NonSerialized] public static List<GameObject> combatRoster = new();
    public int Alive
    {
        get
        {
            return party.Count - Dead;
        }
    }
    [System.NonSerialized] public int Dead;
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
            Debug.Log("Player Spawned!");
            GameObject newGameObject = Instantiate(gameObject);
            OnCharacterSpawned?.Invoke(newGameObject);
        }
    }
}
