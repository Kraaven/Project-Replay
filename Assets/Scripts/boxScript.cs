using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxScript : MonoBehaviour
{
    public Vector3 initpos;
    private void Start()
    {
        initpos = transform.position;
        FindObjectOfType<CameraInteract>().Boxes.Add(this);
    }

    public void resetPos()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(-90,0,0);
        transform.position = initpos;
        
        
    }
}
