using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CombatCamera : FollowPlayer
{
    public GameObject pointerPrefab;
    public Vector3 pointerOffset;
    readonly GameObject currentPointer;

    public GameObject spotLight;
    public Vector3 spotLightOffset;
    
    void OnEnable()
    {
        Character.OnTurnStart += ChangeTarget;
    }

    void OnDisable()
    {
        Character.OnTurnStart -= ChangeTarget;
    }
    public void ChangeTarget(Character newTarget)
    {
        if (currentPointer)
            Destroy(currentPointer);

        target = newTarget.transform;

        spotLight.transform.Translate(newTarget.transform.position + spotLightOffset);
        
        Instantiate(pointerPrefab, target.position + pointerOffset, target.transform.rotation);
    }
}
