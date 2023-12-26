using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifier{
    public readonly int order;
    public readonly float value;
    public readonly StatModType type;
    public readonly object source;

    public StatModifier(float v, StatModType t, int o, object s)
    {
        value = v;
        type = t;
        order = o;
        source = s;
    }
    public StatModifier(float value, StatModType type) : this(value, type, (int)type, null) { }
    public StatModifier(float value, StatModType type, int order) : this(value, type, order, null) { }
    public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }
}

public enum StatModType
{
    Flat = 100,
    PercentAdd = 200,
    PercentMult = 300,
}
