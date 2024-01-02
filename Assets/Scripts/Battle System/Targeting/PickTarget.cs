using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickTarget : MonoBehaviour
{
    void OnMouseDown()
    {
        TargetSystem.Instance.UndisplayCrosshair();
        Debug.Log("Clicked");
        if (BattleEvent.state != BattleState.PLAYERTURN) return;
        TargetSystem.SetTarget(this.gameObject);
        TargetSystem.Instance.DisplayCrosshair(true);
    }
}
