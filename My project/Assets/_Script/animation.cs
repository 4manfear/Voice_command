using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Player_Movement PM;
    [SerializeField]
    private float x, y;
    float idel = 0.0f;

    private void Awake()
    {
        anim= GetComponent<Animator>();
        PM = GetComponent<Player_Movement>();
    }

    private void Update()
    {
        if(PM.moveDirection.magnitude != 0)
        {
            x = Mathf.MoveTowards(x, PM.moveDirection.magnitude, Time.deltaTime);
            //y = Mathf.MoveTowards(y,PM.moveDirection.magnitude,Time.deltaTime);
        }

        if (PM.moveDirection.magnitude == 0)
        {
            x=Mathf.MoveTowards(idel,PM.moveDirection.magnitude,Time.deltaTime);
            
        }

        anim.SetFloat("flying", x);
        //anim.SetFloat("notflying", y);

    }
}
