using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerFloat : MonoBehaviour
{
    public float upperLimit;
    public float lowerLimit;
    public float speed;

    Vector3 originalPosition;
    Vector3 floatDirection = Vector3.down;

    void Awake()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > originalPosition.y + upperLimit)
            floatDirection = Vector3.down;
        else if (transform.position.y < originalPosition.y - lowerLimit)
            floatDirection = Vector3.up;

        transform.Translate(speed * Time.deltaTime * floatDirection);
    }
}
