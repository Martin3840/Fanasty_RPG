using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;
using System.Collections.ObjectModel;

[System.Serializable]
public class StatValue
{
    protected bool modified = true;
    protected float _value;
    public float baseValue;
    protected float lastBaseValue = float.MinValue;
    protected readonly List<StatModifier> statModifiers;
    float percentAddSum = 0;
    public readonly ReadOnlyCollection<StatModifier> StatModifiers;
    public StatValue()
    {
        statModifiers = new List<StatModifier>();
        StatModifiers = statModifiers.AsReadOnly();
    }
    public StatValue(float b) : this()
    {
        baseValue = b;
    }
    public float GetValue()
    {
        return baseValue;
    }
    
    public virtual float Value {
        get
        {
            if (modified || lastBaseValue != baseValue)
            {
                lastBaseValue = baseValue;
                _value = Calculate();
                modified = false;
            }
            return _value;
        }
    }
    protected virtual int Order(StatModifier a, StatModifier b)
    {
        if (a.order < b.order)
            return -1;
        else if (a.order > b.order)
            return 1;
        return 0;
    }

    public virtual void AddModifier(StatModifier mod)
    {
        modified = true;
        statModifiers.Add(mod);
        statModifiers.Sort(Order);
    }

    public virtual bool RemoveModifier(StatModifier mod)
    {
        if (statModifiers.Remove(mod))
        {
            modified = true;
            return true;
        }
        return false;
    }

    public virtual bool RemoveAllModFromSource(object source)
    {
        bool removed = false;
        foreach (StatModifier mod in statModifiers)
        {
            if (mod.source == source)
            {
                modified = true;
                removed = true;
                statModifiers.Remove(mod);
            }
        }
        return removed;
    }

    protected virtual float Calculate()
    {
        float finalValue = baseValue;
        StatModifier last = statModifiers.Last();
        foreach (StatModifier mod in statModifiers)
        {
            if (mod.type == StatModType.Flat)
                finalValue += mod.value;
            else if (mod.type == StatModType.PercentAdd)
            {
                percentAddSum += mod.value;
            }
            else if (mod.type == StatModType.PercentMult)
                finalValue *= 1 + mod.value;
            if (mod == last)
            {
                finalValue *= 1 + percentAddSum;
                percentAddSum = 0;
            }
        }
        return (float)Math.Round(finalValue,4);
    }
}