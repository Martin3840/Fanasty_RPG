using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CharacterName", menuName = "RPGfloats")] 
public class RPGStats : ScriptableObject
{
    public new string name;
    public int level;
    public Sprite sprite;
    public Image profilePic;
    public float health;
    public float physical_attack;
    public float physical_defense;
    public float magic_attack;
    public float magic_defense;
    public float speed;
    public float mana;
    public float accuracy;
    public bool isPlayer;
    private readonly Dictionary<string, Stat> statList;

    public Dictionary<string, Stat> GetAllStats()
    {
        statList.Add("maxHealth", new Stat(health));
        statList.Add("physical_attack", new Stat(physical_attack));
        statList.Add("physical_defense", new Stat(physical_defense));
        statList.Add("magic_attack", new Stat(magic_attack));
        statList.Add("magic_defense", new Stat(magic_defense));
        statList.Add("speed", new Stat(speed));
        statList.Add("mana", new Stat(mana));
        statList.Add("accuracy", new Stat(accuracy));
        return statList;
    }
}
