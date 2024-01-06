using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTeam : MonoBehaviour
{
    public static int total;
    public static GameObject currentPlayerTarget;
    public static event Action<Character> OnCharacterSpawned;
    public List<GameObject> party = new();
    void Awake()
    {
        total = party.Count;
        BattleEvent.OnBattleStart += SpawnCharacters;
    }
    void SpawnCharacters()
    {
        foreach (GameObject player in party)
        {
            Debug.Log("Player Spawned!");
            GameObject newPlayer = Instantiate(player);
            OnCharacterSpawned?.Invoke(newPlayer.GetComponent<Character>());
        }
    }
}
