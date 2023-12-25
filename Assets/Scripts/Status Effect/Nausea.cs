using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nausea : Status
{
    private StatModifier mod;
    public float value;
    Character holder;

    public void Apply(Character target)
    {
        holder = target;
        mod = new StatModifier(value, StatModType.PercentAdd,this);
        target.accuracy.AddModifier(mod);
        target.status.Add(this);
    }

    public override void End()
    {
        holder.accuracy.RemoveModifier(mod);
        holder.status.Remove(this);
    }
}
