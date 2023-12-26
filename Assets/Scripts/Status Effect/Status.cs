using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public abstract class Status : ScriptableObject
{
    private Character target;
    public int duration;
    public List<string> type;

    public virtual void Tick()
    {
        duration--;
        if (duration == 0) End();
    }

    public virtual void Apply(Character target)
    {
        target.statusEffects.Add(this);
    }

    public virtual void End()
    {
        target.statusEffects.Remove(this);
    }
}
