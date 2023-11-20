using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CombatStats : MonoBehaviour
{
    public RPGStats stats;
    public float damage;
    public float maxHP;
    public float currentHP;
    public float mana;
    public float av;
    public void UpdateStats()
    {
        damage = 0.1f * (float)Math.Pow((double)stats.level,2) + 20 * stats.level + stats.attack;
        maxHP = currentHP = 0.1f * (float)Math.Pow((double)stats.level,2) + 20 * stats.level + stats.health;
        av = 1000 / stats.speed;
        GetComponent<SpriteRenderer>().sprite = stats.sprite;
    }

    public bool TakeDamage(float damage) {
        float damageReduce = 100 / (100 + stats.defense);
        currentHP -= damage * damageReduce;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }
}
