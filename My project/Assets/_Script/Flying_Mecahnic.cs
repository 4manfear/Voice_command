using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying_Mecahnic : MonoBehaviour
{
    [SerializeField]
    private float gainHeightForce;
    [SerializeField]
    private float decreaseHeightForce;

    private Rigidbody rb;
    private bool gainingHeight;
    private bool decreasingHeight;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true; 
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        ApplyFlyingMechanics();
    }

    private void HandleInput()
    {
     
        if (Input.GetKeyDown(KeyCode.R))
        {
            rb.useGravity = !rb.useGravity;
        }

        
        if (Input.GetKeyDown(KeyCode.E))
        {
            gainingHeight = true;
        }

        
        if (Input.GetKeyUp(KeyCode.E))
        {
            gainingHeight = false;
        }

        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            decreasingHeight = true;
        }

        
        if (Input.GetKeyUp(KeyCode.Q))
        {
            decreasingHeight = false;
        }
    }

    private void ApplyFlyingMechanics()
    {
        
        if (gainingHeight)
        {
            rb.AddForce(Vector3.up * gainHeightForce, ForceMode.Force);
        }

        
        if (decreasingHeight)
        {
            rb.AddForce(Vector3.down * decreaseHeightForce, ForceMode.Force);
        }
    }

}
