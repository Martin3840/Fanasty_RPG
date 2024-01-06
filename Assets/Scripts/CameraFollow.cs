using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float smoothTime;
    Vector3 velocity = Vector3.zero;
    public Vector3 offset = new(0, 2, -5);
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newLocation = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newLocation, ref velocity, smoothTime);

        transform.LookAt(player);
    }
}
