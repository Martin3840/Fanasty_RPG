using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Position : MonoBehaviour
{
    public Character character;
    public List<PositionType> positionType;
    public SpriteRenderer _spriteRenderer;

    public void BindCharacter(Character newcharacter)
    {
        this.character = newcharacter;
        this.character.OnDeath += UnOccupied;
        
        _spriteRenderer.sprite = this.character.sprite;

        character.transform.parent = this.transform;
        character.transform.position = this.transform.position;
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
            TargetSystem.Instance.SetTarget(this.gameObject);
            Debug.Log("Clicked");
        }
    }
}

public enum PositionType
{
    Front,
    Back
}
