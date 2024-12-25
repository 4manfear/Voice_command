using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inspectrotation : MonoBehaviour
{
    [SerializeField] private Transform targetObject; // The object to spectate
    [SerializeField] private float distance ; // Distance from the target object
    [SerializeField] private float rotationSpeed ; // Speed of camera rotation
    [SerializeField] private float transitionSpeed ; // Speed of transition to the target

    private Vector3 offset; // Offset to maintain distance from the object
    private bool isSpectating = false; // Flag to determine if spectating is active

    private void Start()
    {
        // Set the initial offset based on the starting position of the camera and target object
        offset = transform.position - targetObject.position;
    }

    private void Update()
    {
        // Toggle spectate mode when the "G" key is pressed
        if (Input.GetKeyDown(KeyCode.G))
        {
            isSpectating = !isSpectating;
        }

        // If spectating is active, follow the target object and allow camera rotation
        if (isSpectating)
        {
            Spectate();
        }
    }

    private void Spectate()
    {
        // Calculate the desired position of the camera
        Vector3 desiredPosition = targetObject.position + offset.normalized * distance;
        // Smoothly move the camera to the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * transitionSpeed);

        // Get mouse input for rotation
        float horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float verticalInput = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        // Rotate the camera around the target object
        transform.RotateAround(targetObject.position, Vector3.up, horizontalInput);
        transform.RotateAround(targetObject.position, transform.right, verticalInput);

        // Update the offset after rotation to maintain the correct distance
        offset = transform.position - targetObject.position;
    }

}
