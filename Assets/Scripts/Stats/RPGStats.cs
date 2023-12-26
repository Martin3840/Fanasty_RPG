using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Build.Pipeline.Interfaces;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CharacterName", menuName = "RPGfloats")] 
public class RPGStats : ScriptableObject
{
    public float health;
    public float physical_attack;
    public float physical_defense;
    public float magic_attack;
    public float magic_defense;
    public float speed;
    public float mana;
    public float accuracy;
    private readonly Dictionary<string, Stat> statList;

    public Dictionary<string, Stat> GetAllStats(int level)
    {
        statList.Add("health", new Stat(health));
        statList.Add("physical_attack", new Stat(physical_attack));
        statList.Add("physical_defense", new Stat(physical_defense));
        statList.Add("magic_attack", new Stat(magic_attack));
        statList.Add("magic_defense", new Stat(magic_defense));
        statList.Add("mana", new Stat(mana));
        statList.Add("speed", new Stat(speed));
        statList.Add("accuracy", new Stat(accuracy));
        
        StatModifier levelModifier = new StatModifier((float)Math.Pow(1.2, level), StatModType.PercentMult, 0);
        statList["health"].AddModifier(levelModifier);
        statList["physical_attack"].AddModifier(levelModifier);
        statList["physical_defense"].AddModifier(levelModifier);
        statList["magic_attack"].AddModifier(levelModifier);
        statList["magic_defense"].AddModifier(levelModifier);
        statList["mana"].AddModifier(levelModifier);
        
        return statList;
    }
}
