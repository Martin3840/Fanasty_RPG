using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public enum Category
{
    Attack,
    Defense,
}
public class Skill : ScriptableObject
{
    public string skillName;
    public Sprite icon;
    public float manaCost;
    public Category skillType;
}
