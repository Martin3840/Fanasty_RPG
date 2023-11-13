using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class RPGStats : ScriptableObject
{
    public new string name;
    public Sprite sprite;
    public int level;
    public int health;
    public int attack;
    public int defense;
    public int speed;
    public int magicAttack;
    public int magicDefense;
}
