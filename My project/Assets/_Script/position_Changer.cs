using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class position_Changer : MonoBehaviour
{
    public Transform teleportTarget; 
    public Transform player;
    public bool isTeleporting; 

   

    public Player_Movement playerMovement; 

    private float teleportTimer = 0.5f; // Duration for teleportation timer
    private float currentTimer; // Timer countdown

    private void Update()
    {
        if (isTeleporting)
        {
            // Teleport the player to the target position and rotation
            player.position = teleportTarget.position;
            player.localRotation = teleportTarget.localRotation;

            // Disable the player's movement while teleporting
            playerMovement.enabled = false;

            // Decrease the timer
            currentTimer -= Time.deltaTime;

            // When timer reaches zero, stop teleporting and enable movement
            if (currentTimer <= 0)
            {
                isTeleporting = false;
                playerMovement.enabled = true; // Re-enable movement after teleportation
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Start teleportation and reset timer
            isTeleporting = true;
            currentTimer = teleportTimer; // Reset the timer to its initial value
        }
    }

}
