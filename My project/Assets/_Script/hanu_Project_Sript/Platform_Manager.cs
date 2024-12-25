using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Manager : MonoBehaviour
{
    public GameObject platformPrefab; // The platform prefab to spawn
    public Transform playerTransform; // Reference to the player's position
    public float platformLength ; // Length of each platform
    private Vector3 nextSpawnPosition; // Position for the next platform spawn
    private Queue<GameObject> platforms = new Queue<GameObject>(); // Queue to store active platforms

    private void Start()
    {
        // Initialize the first platform
        nextSpawnPosition = Vector3.zero;
        SpawnPlatform(); // Spawn the first platform
    }

    private void Update()
    {
        // Check if the player has moved past the halfway point of the current platform
        if (playerTransform.position.z > nextSpawnPosition.z - platformLength)
        {
            SpawnPlatform(); // Spawn a new platform ahead
            RemoveOldPlatform(); // Remove the previous platform
        }
    }

    private void SpawnPlatform()
    {
        // Instantiate the platform at the next spawn position
        GameObject newPlatform = Instantiate(platformPrefab, nextSpawnPosition, Quaternion.identity);

        // Add the new platform to the queue
        platforms.Enqueue(newPlatform);

        // Update the spawn position for the next platform
        nextSpawnPosition.z += platformLength/2;
    }

    private void RemoveOldPlatform()
    {
        // Remove the oldest platform if more than one platform exists
        if (platforms.Count > 2)
        {
            GameObject oldPlatform = platforms.Dequeue();
            Destroy(oldPlatform);
        }
    }
}
