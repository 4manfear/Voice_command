using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropping_object : MonoBehaviour
{
    AudioSource audiosource;

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 1f)
        {
            audiosource.Play();
            //Debug.Log("Collision Velocity: " + collision.relativeVelocity.magnitude);
        }
    }
}
