using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPP_Camera_Script : MonoBehaviour
{
    public Transform target; 
    public float distance ; 
    public float height; 
    public float rotationSpeed; 

    private float currentX = 0.0f;
    private float currentY = 0.0f;

    void LateUpdate()
    {
        // Get mouse inputs
        currentX += Input.GetAxis("Mouse X") * rotationSpeed;
        currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        currentY = Mathf.Clamp(currentY, -20f, 80f); // Limit vertical rotation

        // Calculate the new position and rotation for the camera
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 direction = new Vector3(0, 0, -distance);
        Vector3 position = target.position + rotation * direction + Vector3.up * height;

        // Set the camera's position and rotation
        transform.position = position;
        transform.LookAt(target.position + Vector3.up * height);
    }
}
