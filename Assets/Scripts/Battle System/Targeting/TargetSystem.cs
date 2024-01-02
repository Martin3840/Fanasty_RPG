using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetSystem : MonoBehaviour
{
    public static TargetSystem Instance;
    public GameObject crosshairPrefab;
    public static GameObject currentTarget;
    public static GameObject leftTarget;
    public static GameObject rightTarget;
    readonly HashSet<GameObject> crosshairs = new();

    void Awake()
    {
        Instance = this;
        BattleEvent.OnBattleStart += InitialTarget;
    }
    void InitialTarget()
    {
        currentTarget = EnemyTeam.combatRoster[0];
        leftTarget = EnemyTeam.GetLeftEnemy(currentTarget);
        rightTarget = EnemyTeam.GetRightEnemy(currentTarget);
        BattleEvent.OnBattleStart -= InitialTarget;
        Debug.Log("got Inital Target");
    }
    public void DisplayCrosshair(bool adjacent)
    {
        GameObject newCrosshair = Instantiate(crosshairPrefab, currentTarget.transform);
        crosshairs.Add(newCrosshair);
        if (adjacent)
        {
            if (leftTarget != null)
            {
                newCrosshair = Instantiate(crosshairPrefab, leftTarget.transform);
                crosshairs.Add(newCrosshair);
            }
            if (rightTarget != null)
            {
                newCrosshair = Instantiate(crosshairPrefab, rightTarget.transform);
                crosshairs.Add(newCrosshair);
            }
        }
    }
    public void UndisplayCrosshair()
    {
        foreach (GameObject crosshair in crosshairs)
        {
            Destroy(crosshair);
        }
    }
    public static void SetTarget(GameObject target)
    {
        currentTarget = target;
        leftTarget = EnemyTeam.GetLeftEnemy(target);
        rightTarget = EnemyTeam.GetRightEnemy(target);
    }
    public static Character GetTarget(TargetType type)
    {
        if (currentTarget != null)
            Debug.Log("The One Piece is real!");
        return currentTarget.GetComponent<Character>();
    }
}

public enum TargetType
{
    mid,
    left,
    right
}
