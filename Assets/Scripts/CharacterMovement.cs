using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 3.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.0f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        controller.Move(Vector3.right * horizontalInput * Time.deltaTime * speed);

        float verticalInput = Input.GetAxis("Vertical");
        controller.Move(Vector3.forward * verticalInput * Time.deltaTime * speed);

        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        velocity.y += gravity * Time.deltaTime;
        if (isGrounded && (velocity.y <= 0)) {
            velocity.y = -3f;
        }
        controller.Move(velocity * Time.deltaTime);
    }
}
