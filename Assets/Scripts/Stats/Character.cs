using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.Build.Pipeline.Interfaces;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "NewCharacterName", menuName = "Character")]
public class Character : ScriptableObject
{
    [System.NonSerialized]
    public GameObject spriteObject;
    [System.NonSerialized]
    public CharacterUI characterUI;
    [System.NonSerialized]
    public float actionValue;
    BattleSystem battleSystem;
    public RPGStats baseStats;
    public Sprite sprite;
    public Sprite profile_picture;
    public new string name;
    public int level;
    private float currentHp;
    private float currentMana;
    public bool isPlayer;
    public Skill normalAttack;
    public Skill passive;
    public Skill skill1;
    public Skill skill2;
    public Skill skill3;
    public Skill ultimate;

    public Dictionary<string,Stat> stats;
    public HashSet<Status> statusEffects;

    private int oldLevel;
    public void UpdateStats()
    {
        if (level == oldLevel) return;
        stats = baseStats.GetAllStats(level);
    }

    public void TurnStart()
    {
        foreach (Status effect in statusEffects)
            effect.Tick();
    }
    public void TurnEnd()
    {
        actionValue += 1000 / stats["speed"].Value;
        battleSystem.SortTurn();
    }
    public void EnterCombat(BattleSystem system, GameObject prefab, Transform location)
    {
        battleSystem = system;
        spriteObject = Instantiate(prefab, location);
        spriteObject.GetComponent<SpriteRenderer>().sprite = profile_picture;
        UpdateStats();
    }

    public virtual void UpdateUI()
    {
        characterUI.Setup(currentHp, stats["health"].Value, currentMana, stats["mana"].Value, profile_picture);
    }

    public virtual int TakeDamage(float damage)
    {
        float damageReduce = 100 / (100 + stats["physical_defense"].Value);
        float finalDamage = damage * damageReduce;
        
        currentHp -= finalDamage;

        int finalInt = (int) finalDamage;
        if (isPlayer)
        {
            characterUI.SetHpValue(finalInt);
        }
        return finalInt;
    }

    public virtual bool IsDead()
    {
        return currentHp <= 0;
    }

    public void Despawn()
    {
        Destroy(spriteObject);
    }
}
