using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChasePlayer : MonoBehaviour
{
    public Transform target;
    [SerializeField]
    private float speed = 2.0f;
    [SerializeField]
    private float lookDistance = 5.0f;


    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "Player") {
            SceneManager.LoadScene("Combat", LoadSceneMode.Additive);
        }
    }
    void Update()
    {
        var step = speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, target.position) < lookDistance) {
            transform.position = Vector3.MoveTowards(transform.position,target.position,step);
        }
    }
}
