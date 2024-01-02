using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LocationManager : MonoBehaviour
{
    public List<Location> enemyLocation;
    public List<Location> playerLocation;
    void OnEnable()
    {
        EnemyTeam.OnEnemySpawned += SetEnemyLocation;
        PlayerTeam.OnCharacterSpawned += SetPlayerLocation;
    }
    void SetEnemyLocation(GameObject enemy)
    {
        foreach (Location location in enemyLocation)
        {
            if (location.character == null)
            {
                enemy.transform.Translate(location.transform.localPosition);
                location.character = enemy.GetComponent<Character>();
                break;
            }
        }
    }

    void SetPlayerLocation(GameObject player)
    {
        foreach (Location location in playerLocation)
        {
            if (location.character == null)
            {
                player.transform.Translate(location.transform.localPosition);
                location.character = player.GetComponent<Character>();
                break;
            }
        }
    }
}
