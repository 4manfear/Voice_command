using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostmode_powerUP : MonoBehaviour
{
    public LayerMask enemyLayer;  // Assign the Enemy layer in the Inspector
    public LayerMask groundLayer; // Assign the Ground layer in the Inspector
    private bool canPassThroughEnemy = false;
    private float passThroughDuration = 5f;
    private float passThroughTimer = 0f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // If the player can pass through enemies
        if (canPassThroughEnemy)
        {
            passThroughTimer += Time.deltaTime;

            // After 5 seconds, revert back to normal collision
            if (passThroughTimer >= passThroughDuration)
            {
                Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
                canPassThroughEnemy = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // this function will getactive when the player gets the powerup
        if (other.CompareTag("PowerUp")) // Replace with appropriate trigger
        {
            ActivatePassThroughEnemy();
        }
    }

    void ActivatePassThroughEnemy()
    {
        // Ignore collision between Player and Enemy
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        canPassThroughEnemy = true;
        passThroughTimer = 0f;
    }

    void FixedUpdate()
    {
        // Continuously apply gravity
        rb.AddForce(Physics.gravity * rb.mass);
    }
}
