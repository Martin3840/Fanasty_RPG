using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nausea : Status
{
    private StatModifier mod;
    public float value;
    Character holder;

    public override void Apply(Character target)
    {
        holder = target;
        mod = new StatModifier(value, StatModType.PercentAdd,this);
        target.stats["accuracy"].AddModifier(mod);
        base.Apply(target);
    }

    public override void End()
    {
        base.End();
        holder.stats["accuracy"].RemoveModifier(mod);
    }
}
