using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public Character character = null;

    void Awake()
    {
        Character.OnStaticDeath += UnOccupied;
    }
    void OnDisable()
    {
        Character.OnStaticDeath -= UnOccupied;
    }

    void UnOccupied(Character character)
    {
        if (character == this.character)
            character = null;
    }
}
