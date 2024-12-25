using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switching_TO_magnifine_Glass : MonoBehaviour
{
    [SerializeField]
    Player_Movement playermovementsc;
    [SerializeField]
    TPP_Camera_Script tppcamerascript;
    [SerializeField]
    magnifine_Glass mg;
    [SerializeField]
    private bool canswitch;
    [SerializeField]
    private GameObject canvasmagnifinrendrer;

    private void Awake()
    {
        canvasmagnifinrendrer.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(canswitch)
            {
                playermovementsc.enabled = false;
                tppcamerascript.enabled = false;
                mg.enabled = true;
                canvasmagnifinrendrer.SetActive(true);

            }
            else
            {
                playermovementsc.enabled = true;
                tppcamerascript.enabled = true;
                mg.enabled = false;
                canvasmagnifinrendrer.SetActive(false);
            }
           
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canswitch = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canswitch = false;
        }
    }

}
