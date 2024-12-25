using UnityEngine;



public class Detecting_object : MonoBehaviour
{
    public Transform hand;

    [SerializeField]
    private GameObject[] Pickable;

    [SerializeField]
    private GameObject nearestgameobject;

    private float nerobj = Mathf.Infinity;
    float Distance;
    float distancechecker;

    public bool canpickup;
    public bool Inthehand;

    public LineRenderer trajectoryLineRenderer;  // To visualize the trajectory
    public int lineSegmentCount = 20;  // Number of segments in the trajectory
    public float throwForce = 10f;  // Force to throw the object
    private bool isThrowing = false;  // To track if the player is holding space to throw

    private void Start()
    {
        Inthehand = false;

        Pickable = GameObject.FindGameObjectsWithTag("pickable");


    }

    private void Update()
    {
        if (!Inthehand)
        {
            finding_nearest_pickables_object();
            nerobj = Mathf.Infinity;
        }




        // Calculate the distance between the player (this object) and the nearest game object
        distancechecker = Vector3.Distance(this.transform.position, nearestgameobject.transform.position);

        if (canpickup == true && Inthehand == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                canpickuptheobject();
            }
        }


        if (nearestgameobject.transform.parent == hand.transform)
        {
            Inthehand = true;

        }
        else
        {
            Inthehand = false;
        }

        if (distancechecker < 5f)
        {
            canpickup = true;


        }
        else
        {
            canpickup = false;
        }



        if (Inthehand)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                droppingtheobject();
            }
        }

        if (Inthehand)
        {
            // If space is held, visualize the trajectory
            if (Input.GetKey(KeyCode.Space))
            {
                isThrowing = true;
                ShowTrajectory();
            }
            // If space is released, throw the object
            if (Input.GetKeyUp(KeyCode.Space))
            {
                isThrowing = false;
                trajectoryLineRenderer.enabled = false;
                ThrowObject();
            }

        }
    }


    // Method to visualize the trajectory
    void ShowTrajectory()
    {
        if (nearestgameobject != null)
        {
            trajectoryLineRenderer.enabled = true;

            Vector3[] points = new Vector3[lineSegmentCount];
            Vector3 startingPosition = nearestgameobject.transform.position;
            Vector3 startingVelocity = Camera.main.transform.forward * throwForce;

            for (int i = 0; i < lineSegmentCount; i++)
            {
                float time = i * Time.fixedDeltaTime;
                Vector3 point = startingPosition + time * startingVelocity;
                point.y = startingPosition.y + startingVelocity.y * time + 0.5f * Physics.gravity.y * time * time;
                points[i] = point;
            }

            trajectoryLineRenderer.positionCount = points.Length;
            trajectoryLineRenderer.SetPositions(points);
        }
    }

    // Method to throw the object
    void ThrowObject()
    {
        if (nearestgameobject != null)
        {
            Rigidbody rb = nearestgameobject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddForce(Camera.main.transform.forward * throwForce, ForceMode.VelocityChange);
            nearestgameobject.transform.SetParent(null);  // Remove the object from the hand
            Inthehand = false;
            nearestgameobject = null;  // Clear the reference after throwing
        }
    }






    void droppingtheobject()
    {
        if (nearestgameobject != null)
        {
            nearestgameobject.transform.SetParent(null);  // Remove from the hand

            Rigidbody rb = nearestgameobject.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = false;
            Inthehand = false ;
            nearestgameobject = null;
            
              
        }
    }


    void canpickuptheobject()
    {
        if (nearestgameobject != null)
        {
            nearestgameobject.transform.SetParent(hand.transform);

            nearestgameobject.transform.localPosition = Vector3.zero;
            nearestgameobject.transform.localRotation = Quaternion.identity;
            Rigidbody rb = nearestgameobject.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;

        }
    }

    void finding_nearest_pickables_object()
    {

        foreach (GameObject pickablee in Pickable)
        {
            Distance = Vector3.Distance(this.transform.position, pickablee.transform.position);

            if (Distance < nerobj)
            {
                nerobj = Distance;
                nearestgameobject = pickablee;
                //Debug.Log(nearestgameobject.name);
            }
        }
       


    }
}
