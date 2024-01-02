using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static event Action<Character> OnBattleJoined;
    public static event Action<Character> OnTurnStart;
    public static event Action<Character> OnTurnEnded;
    public static event Action<Character> OnStaticDeath;
    public event Action OnDeath;
    public event Action<int> OnDamaged;
    [System.NonSerialized] public float actionValue;
    public RPGStats baseStats;
    public Sprite profilePicture;
    public new string name;
    public int level;
    public float currentHp;
    public float currentMana;
    public bool isPlayer;

    public Skill passive;
    public Skill attack;
    public Skill skill1;
    public Skill skill2;
    public Skill skill3;
    public Skill ultimate;

    public Dictionary<Stat, StatValue> stats = new();
    public HashSet<Status> statusEffects = new();
    
    public virtual void Awake()
    {
        UpdateStats();
        OnBattleJoined?.Invoke(this);
    }

    public void UpdateStats()
    {
        stats = baseStats.GetAllStats(level);
    }

    public void TurnStart()
    {
        Debug.Log(name + "'s turn started!");
        foreach (Status effect in statusEffects)
            effect.Tick();
        Character.OnTurnStart += skill1.Cast;
        OnTurnStart?.Invoke(this);
    }
    public void TurnEnd()
    {
        actionValue += 1000 / stats[Stat.speed].Value;
        OnTurnEnded?.Invoke(this);
    }

    public int TakeDamage(float damage)
    {
        float damageReduce = 100 / (100 + stats[Stat.physical_defense].Value);
        float finalDamage = damage * damageReduce;
        
        currentHp -= finalDamage;

        if (currentHp < 0)
        {
            currentHp = 0;
            Despawn();
        }

        int finalDamageInt = (int) finalDamage;

        OnDamaged(finalDamageInt);

        return finalDamageInt;
    }

    public void Despawn()
    {
        OnStaticDeath?.Invoke(this);
        OnDeath?.Invoke();
        Destroy(this.gameObject);
    }

    public void ReceiveStatus(Status status)
    {
        statusEffects.Add(status);
    }
}
