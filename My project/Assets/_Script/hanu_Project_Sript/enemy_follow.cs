using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_follow : MonoBehaviour
{
    private Rigidbody rb;
    private Transform player;   // Reference to the player's position
    public float speed = 5f;    // Speed at which the enemy moves toward the player
    private float lifetime = 10f; // Time (in seconds) before the enemy is destroyed

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
       

        
    }

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assumes the player has a "Player" tag
        Vector3.MoveTowards(transform.position,player.position,speed*Time.deltaTime);

        timerstarted();

    }

    void timerstarted()
    {
        lifetime-= Time.deltaTime;

        if(lifetime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("bullet"))
        {
            Destroy(this.gameObject);   
        }
    }

}
