using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public abstract class Status : ScriptableObject
{
    public int duration;

    public List<string> type;

    public virtual void Turn()
    {
        duration--;
        if (duration == 0) End();
    }

    public abstract void End();

}
