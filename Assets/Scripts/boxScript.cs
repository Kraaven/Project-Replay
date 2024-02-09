using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxScript : MonoBehaviour
{
    //Cache Initial position for reset [Not Forces, so that we have variation between resets]
    public Vector3 initpos;
    private void Start()
    {
        initpos = transform.position;
        //Add this box to the possible objects to be tinted
        FindObjectOfType<CameraInteract>().Boxes.Add(this);
    }

    //Function to Reset the position 
    public void resetPos()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(-90,0,0);
        transform.position = initpos;
        
        
    }
}
