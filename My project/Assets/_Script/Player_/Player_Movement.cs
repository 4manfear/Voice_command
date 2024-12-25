using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float moveSpeed ;
    public float normal_speed;
    public float sprint;
    //public float jumpForce ; 
    //public bool isGrounded;
    private Transform cameraTransform;

    public Animator anim;

    private Rigidbody rb;
    float moveHorizontal;
    float moveVertical;

    public Vector3 moveDirection;

    void Start()
    {
       
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
       
        HandleMovement();
        pistolmechaninc();

    }

    void HandleMovement()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = sprint;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = normal_speed;
        }

        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");


        // Calculate movement direction based on camera orientation
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f; // Ensure we only move in XZ plane
        right.y = 0f;

        moveDirection = (forward * moveVertical + right * moveHorizontal).normalized;

        // Move the player
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);

        // Rotate the player to face the movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.deltaTime);
        }

        if (moveVertical > 0 || moveHorizontal >0)
        {
           anim.SetFloat("walkingfwd", 0); // Forward movement
            anim.SetFloat("walkingback", 1);
        }
        else if (moveVertical < 0 || moveHorizontal < 0)
        {
            anim.SetFloat("walkingfwd", 0);
            anim.SetFloat("walkingback", 1); // Backward movement
        }
        else
        {
          //  anim.SetFloat("walkingfwd", 0);
            anim.SetFloat("walkingback", 0);
        }

        //if (Input.GetButtonDown("Jump") && isGrounded)
        //{
        //    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        //    isGrounded = false; // Set to false until the player lands back
        //}
    }

    void pistolmechaninc()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            anim.SetBool("draw_pistol",true);
            StartCoroutine(pistolsettingfalse());
        }
    }
    IEnumerator pistolsettingfalse()
    {
        
        yield return new WaitForSeconds(0.7f);
        anim.SetBool("draw_pistol", false);
    }


//    private void OnCollisionEnter(Collision collision)
//    {
//        if (collision.gameObject.CompareTag("Ground"))
//        {
//            isGrounded = true;
//        }
//    }
}
