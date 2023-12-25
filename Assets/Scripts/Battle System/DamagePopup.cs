using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    public float floatTimeInSeconds = 5;
    IEnumerator Start()
    {
        for (int i = (int)floatTimeInSeconds; i > 0; i--)
        {
            yield return new WaitForSeconds(1.0f);
            this.gameObject.transform.Translate(Vector3.up * i);
        }
        Destroy(this.gameObject);
    }
}
