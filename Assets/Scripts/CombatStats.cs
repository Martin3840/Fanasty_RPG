using System;
using UnityEngine;

public class CombatStats : MonoBehaviour
{
    public RPGStats stats;
    public CharacterUI characterUI;
    public float damage;
    public float maxHP;
    public float currentHP;
    public float currMana;
    public float maxMana;
    public float critChance;
    public float critDamage;
    public float av;
    public void UpdateStats()
    {
        damage = 0.1f * (float)Math.Pow((double)stats.level,2) + 20 * stats.level + stats.attack;
        maxHP = currentHP = 0.1f * (float)Math.Pow((double)stats.level,2) + 20 * stats.level + stats.health;
        currMana = maxMana = 0.1f * (float)Math.Pow((double)stats.level,2) + 20 * stats.level + stats.mana;
        av = 1000 / stats.speed;
        critChance = 50;
        critDamage = 150;
        GetComponent<SpriteRenderer>().sprite = stats.sprite;
    }

    public void UpdateUI()
    {
        characterUI.Setup(currentHP, maxHP, currMana, maxMana, stats.profilePic);
    }

    public int TakeDamage(float damage, ref bool Crit)
    {
        float damageReduce = 100 / (100 + stats.defense);
        float finalDamage = damage * damageReduce;

        int chance = UnityEngine.Random.Range(1,100);
        if (chance < critChance)
        {
            finalDamage *= critDamage / 100;
            Crit = true;
        }
        
        currentHP -= finalDamage;

        int finalInt = (int) finalDamage;
        if (stats.isPlayer)
        {
            characterUI.SetHpValue(finalInt);
        }
        return finalInt;
    }

    public bool IsDead()
    {
        return currentHP <= 0;
    }

}
