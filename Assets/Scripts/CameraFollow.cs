using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public float smoothTime;
    Vector3 velocity = Vector3.zero;
    public Vector3 targetOffset = new(0, 0, 0);
    public Vector3 cameraOffset = new(0, 1.5f, -4);
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newLocation = target.position + cameraOffset;
        transform.position = Vector3.SmoothDamp(transform.position, newLocation, ref velocity, smoothTime);

        transform.LookAt(target.position + targetOffset);
    }
}
