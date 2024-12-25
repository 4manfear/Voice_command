using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inspect_object : MonoBehaviour
{
    public TPP_Camera_Script tppcam;

    [SerializeField]
    private Transform inspectobject;// object to inspected
    [SerializeField]
    private Transform inspectelocation;// position to be moved the inspected object
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private GameObject cameramaintpp;

    public float caneravision_tpp;
    public float camonspectate;

    public bool specatetaing_is_True;

    public bool can_specatete;

    private void Update()
    {
        if(can_specatete)
        {
            if (Input.GetKey(KeyCode.G))
            {
                specatetaing_is_True = true;
            }
        }

        playerspectating();

    }

    void playerspectating()
    {
        if (specatetaing_is_True)
        {
            inspectobject.position = Vector3.MoveTowards(inspectobject.position, inspectelocation.position, Time.deltaTime * 2f);
            cameramaintpp.transform.LookAt(inspectelocation.position);
            cam.fieldOfView = camonspectate;
            //tppcam = GetComponent<TPP_Camera_Script>();
            tppcam.enabled = false;
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            can_specatete = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            can_specatete = false;
        }
    }

}
