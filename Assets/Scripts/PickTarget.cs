using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickTarget : MonoBehaviour
{
    void OnMouseDown()
    {
        GameObject.FindWithTag("BattleSystem").GetComponent<BattleSystem>().SetTarget(this.gameObject);
    }
}
