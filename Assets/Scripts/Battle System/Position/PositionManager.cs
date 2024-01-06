using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PositionManager : MonoBehaviour
{
    public List<Position> enemyPosition = new();
    public List<Position> playerPosition = new();
    void OnEnable()
    {
        EnemyTeam.OnEnemySpawned += SetCharacterPosition;
        PlayerTeam.OnCharacterSpawned += SetCharacterPosition;
    }
    void OnDisable()
    {
        EnemyTeam.OnEnemySpawned -= SetCharacterPosition;
        PlayerTeam.OnCharacterSpawned -= SetCharacterPosition;
    }
    void SetCharacterPosition(Character character)
    {
        List<Position> positions;
        if (character.isPlayer)
            positions = playerPosition;
        else
            positions = enemyPosition;
        foreach (Position pos in positions)
        {
            if (pos.character == null)
            {
                pos.BindCharacter(character);
                break;
            }
        }
    }
}
