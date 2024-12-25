using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endless_player_movement : MonoBehaviour
{
    // Speed for the forward movement
    public float forwardSpeed = 5f;

    // Speed for the left and right movement
    public float lateralSpeed = 5f;

    // Bounds for lateral movement
    public float minX = -5f; // Left boundary
    public float maxX = 5f;  // Right boundary

    // Reference to the Rigidbody component
    private Rigidbody rb;

    private void Start()
    {
        // Get and store the Rigidbody component attached to the player
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Move the player forward automatically
        Vector3 forwardMove = transform.forward * forwardSpeed * Time.fixedDeltaTime;

        // Get input for lateral movement (left and right)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate desired lateral movement and add it to the current position
        Vector3 lateralMove = transform.right * horizontalInput * lateralSpeed * Time.fixedDeltaTime;
        Vector3 targetPosition = rb.position + forwardMove + lateralMove;

        // Clamp the x position to keep the player within bounds
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);

        // Move the Rigidbody to the target position
        rb.MovePosition(targetPosition);
    }
}
