using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterName", menuName = "RPGStats")] 
public class RPGStats : ScriptableObject
{
    public new string name;
    public int level;
    public Sprite sprite;
    public float health;
    public float attack;
    public float defense;
    public float speed;
    public bool isPlayer;
}
