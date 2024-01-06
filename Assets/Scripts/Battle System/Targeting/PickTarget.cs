using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickTarget : MonoBehaviour
{
    public static event Action<GameObject> OnClicked;
    public bool isEnabled;

    void OnMouseDown()
    {
        if (isEnabled)
        {
            OnClicked(this.gameObject);
        }
    }
}
