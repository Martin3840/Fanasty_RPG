using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackStab : Skill
{
    public override void Cast(Character caster)
    {
        Debug.Log(caster.name + " Castef BackStab");
    }
}
