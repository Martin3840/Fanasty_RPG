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
    public StatModifier(float v, StatModType t) : this(v, t, (int)t, null) { }
    public StatModifier(float v, StatModType t, int o) : this(v, t, o, null) { }
    public StatModifier(float v, StatModType t, object s) : this(v,t,(int)t,s) { }
}

public enum StatModType
{
    Flat = 100,
    PercentAdd = 200,
    PercentMult = 300,
}
