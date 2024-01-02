using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stat
{
    health,
    physical_attack,
    physical_defense,
    magic_attack,
    magic_defense,
    speed,
    mana,
    accuracy,
}

[CreateAssetMenu(fileName = "NewBaseStat", menuName = "BaseStat")]
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
    private readonly Dictionary<Stat, StatValue> statList = new();

    public Dictionary<Stat, StatValue> GetAllStats(int level)
    {
        statList.Clear();
        statList.Add(Stat.health, new StatValue(health));
        statList.Add(Stat.physical_attack, new StatValue(physical_attack));
        statList.Add(Stat.physical_defense, new StatValue(physical_defense));
        statList.Add(Stat.magic_attack, new StatValue(magic_attack));
        statList.Add(Stat.magic_defense, new StatValue(magic_defense));
        statList.Add(Stat.mana, new StatValue(mana));
        statList.Add(Stat.speed, new StatValue(speed));
        statList.Add(Stat.accuracy, new StatValue(accuracy));
        
        StatModifier levelModifier = new ((float)Math.Pow(1.2, level), StatModType.PercentMult, 0);
        statList[Stat.health].AddModifier(levelModifier);
        statList[Stat.physical_attack].AddModifier(levelModifier);
        statList[Stat.physical_defense].AddModifier(levelModifier);
        statList[Stat.magic_attack].AddModifier(levelModifier);
        statList[Stat.magic_defense].AddModifier(levelModifier);
        statList[Stat.mana].AddModifier(levelModifier);
        
        return statList;
    }
}
