using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;

public enum Category
{
    Attack,
    Defense,
}
public abstract class Skill : ScriptableObject
{
    public string skillName;
    public Sprite icon;
    public float manaCost;
    public SkillType skillType;
    public string description;
    public abstract void Cast(Character caster);
}


public enum SkillType
{
    Passive,
    Attack,
    Skill1,
    Skill2,
    Skill3,
    Ultimate,
}
