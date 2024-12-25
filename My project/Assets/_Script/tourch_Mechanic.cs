using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tourch_Mechanic : MonoBehaviour
{
    public GameObject tourch;
    public Animator anim;
    public bool light_Is_on;

    // Start is called before the first frame update
    void Start()
    {
        tourch.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if(light_Is_on == false) 
            {
                light_Is_on = true;
                anim.SetBool("tourch_In_hand", true);
                tourch.SetActive(true);
            }
           
        }
    }
}
