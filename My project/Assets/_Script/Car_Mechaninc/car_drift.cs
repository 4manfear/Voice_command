using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car_drift : MonoBehaviour
{

    public bool ismoving;

    public Transform backLeftWheel;  // Reference to the back-left wheel
    public Transform backRightWheel; // Reference to the back-right wheel
    public Transform frontLeftWheel;    // Reference to the front-left wheel
    public Transform frontRightWheel;   // Reference to the front-right wheel



    public float acceleration ;    // Speed of the car
    public float maxSpeed ;         // Maximum speed of the car
    public float reverseSpeed ;     // Speed when reversing
    public float turnSpeed ;       // Steering angle speed

    public float maxSteerAngle;  // Maximum steering angle of the front wheels



    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }

    private void FixedUpdate()
    {
        // Get input for forward/backward movement and steering
        float moveInput = Input.GetAxis("Vertical");   // W/S or Up/Down arrow for forward and backward
        float turnInput = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow for turning

        // Forward or backward movement by applying force to both rear wheels
        if (moveInput != 0)
        {
            Vector3 forwardForce = transform.forward * moveInput * acceleration;

            // Apply force to the back wheels
            rb.AddForceAtPosition(forwardForce, backLeftWheel.position);
            rb.AddForceAtPosition(forwardForce, backRightWheel.position);

            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed); // Clamp the car's speed
        }

        // Steering logic
        if (turnInput != 0 && moveInput!=0)
        {
            float steerAngle = turnInput * maxSteerAngle;

            // Rotate the front wheels visually
            frontLeftWheel.localRotation = Quaternion.Euler(0f, steerAngle, 0f);
            frontRightWheel.localRotation = Quaternion.Euler(0f, steerAngle, 0f);
            if(moveInput <0)
            {
                // Turn the car by rotating its Rigidbody based on the steer angle
                rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, -steerAngle * Time.fixedDeltaTime, 0f));
            }
            if(moveInput >0)
            {
                // Turn the car by rotating its Rigidbody based on the steer angle
                rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, steerAngle * Time.fixedDeltaTime, 0f));
            }
           
        }
    }
}




