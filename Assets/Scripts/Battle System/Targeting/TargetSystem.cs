using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetSystem : MonoBehaviour
{
    public static TargetSystem Instance;
    public GameObject crosshairPrefab;
    public static Character currentTarget;
    public static PositionType currentPositionType;
    public List<GameObject> crosshairs;

    void Awake()
    {
        Instance = this;
    }
    public void DisplayCrosshair()
    {
        GameObject newCrosshair = Instantiate(crosshairPrefab, currentTarget.transform);
        
        foreach (GameObject crosshair in crosshairs)
            Destroy(crosshair);
        
        crosshairs.Add(newCrosshair);
    }

    public void SetTarget(Character character)
    {
        currentTarget = character;
        DisplayCrosshair();
    }
}

public enum TargetType
{
    mid,
    left,
    right
}
