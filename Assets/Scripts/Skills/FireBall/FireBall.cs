using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class FireBall : Skill
{
    public GameObject crosshairPrefab;
    public BattleSystem battleSystem;
    bool Crit;
    GameObject target;
    void Awake()
    {
        Debug.Log("SkillButton Summoned!");
        /*
        target = GetTarget();
        Vector3 offset = new(0f,0.5f,-0.1f);
        Crosshair = Instantiate(CrosshairPrefab,target.transform.position + offset,target.transform.rotation);
        */
    }
    void Cast()
    {
        Debug.Log("Casted");
    }
}
