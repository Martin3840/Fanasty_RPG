using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Position : MonoBehaviour
{
    public Character character = null;
    public List<PositionType> positionType;
    public SpriteRenderer _spriteRenderer;

    public void SpawnCharacter(Character character)
    {
        this.character = character;
        this.character.OnDeath += UnOccupied;

        _spriteRenderer.sprite = this.character.sprite;
    }
    void OnDisable()
    {
        if (character != null)
            character.OnDeath -= UnOccupied;
    }

    void UnOccupied()
    {
        character.OnDeath -= UnOccupied;
        character = null;
    }

    void OnMouseDown()
    {
        if (BattleEvent.state != BattleState.PLAYERTURN) return;

        if (positionType.Contains(TargetSystem.currentPositionType))
        {
            TargetSystem.Instance.SetTarget(character);
            Debug.Log("Clicked");
        }
    }
}

public enum PositionType
{
    Front,
    Back
}
