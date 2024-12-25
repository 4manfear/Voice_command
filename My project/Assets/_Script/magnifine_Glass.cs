using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magnifine_Glass : MonoBehaviour
{
    public float moveDistance; // The distance to move in each step
    public float minZ; // Minimum Z boundary
    public float maxZ ; // Maximum Z boundary

    public float minx; 
    public float maxx;

    public float speed; // Speed of movement based on mouse input

    void Update()
    {
        // Calculate movement based on mouse X-axis movement
        float moveInputonx = Input.GetAxis("Mouse X") * speed * Time.deltaTime;
        float moveInputony = Input.GetAxis("Mouse Y") * speed * Time.deltaTime;

        // Calculate the new position, restricted to the Z-axis
        float newZPositiononx = Mathf.Clamp(transform.position.z + moveInputonx, minZ, maxZ);
        float newZPositionony = Mathf.Clamp(transform.position.x + moveInputony, minx, maxx);

        // Apply the new position, keeping X and Y unchanged
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPositiononx);

        transform.position = new Vector3(newZPositionony,transform.position.y, transform.position.z);
    }

}
