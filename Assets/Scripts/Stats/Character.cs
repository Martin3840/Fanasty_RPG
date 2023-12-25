using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [System.NonSerialized]
    public GameObject spriteObject;
    [System.NonSerialized]
    public CharacterUI characterUI;
    public RPGStats baseStats;
    private float currentHp;
    private float currentMana;
    public float av;
    public bool isPlayer;


    public Dictionary<string,Stat> stats;
    public HashSet<Status> status;

    public void UpdateBaseStats()
    {
        stats = baseStats.GetAllStats();
    }

    public void TurnEnd()
    {
        av += 1000 / stats["speed"].Value;
    }

    public virtual void UpdateUI()
    {
        characterUI.Setup(currentHp, stats["health"].Value, currentMana, stats["mana"].Value, baseStats.profilePic);
    }

    public virtual int TakeDamage(float damage)
    {
        float damageReduce = 100 / (100 + stats["physical_defense"].Value);
        float finalDamage = damage * damageReduce;
        
        currentHp -= finalDamage;

        int finalInt = (int) finalDamage;
        if (baseStats.isPlayer)
        {
            characterUI.SetHpValue(finalInt);
        }
        return finalInt;
    }

    public virtual bool IsDead()
    {
        return currentHp <= 0;
    }

    public void SpawnPrefabAt(GameObject prefab, Transform location)
    {
        spriteObject = Instantiate(prefab,location);
    }

    public void Despawn()
    {
        Destroy(spriteObject);
    }
}
