using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraInteract : MonoBehaviour
{
    //Cached data and references
    private boxScript Tbox;
    public GameObject Ball;
    public List<boxScript> Boxes;

    private Vector3 BallInitPos;
    private Quaternion BallInitRot;

    private void Awake()
    {
        Boxes = new List<boxScript>();
    }

    
    private void Start()
    {
        //Caching references and ball data
        Tbox = FindObjectOfType<boxScript>();
        BallInitPos = Ball.transform.position;
        BallInitRot = Ball.transform.rotation;
    }

    void Update()
    {
        //Setting the Color tint on the Boxes. 
        RaycastHit hit;
        Ray R = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(R, out hit, 150))
        {
            if (hit.transform.TryGetComponent(out boxScript box))
            {
                if (Tbox != box)
                {
                    // Reset color of the previous box
                    if (Tbox != null)
                    {
                        Tbox.GetComponent<MeshRenderer>().material.color = Color.white;
                    }

                    Tbox = box;
                    Tbox.GetComponent<MeshRenderer>().material.color = Color.red;

                    
                    
                }
            }
        }
        else
        {
            // Reset color when not hovering over any box
            if (Tbox != null)
            {
                Tbox.GetComponent<MeshRenderer>().material.color = Color.white;
                Tbox = null;
            }
        }
        
        //The control that adds force to the ball. Could be in Fixed Update
        if (Input.GetMouseButton(0))
        {

            if (Tbox != null)
            {
                Vector3 forceDirection = hit.point  - Ball.transform.position;
                Ball.GetComponent<Rigidbody>().AddForce(forceDirection.normalized * 2, ForceMode.Impulse);
            }
        }

        
        //To stop the Ball's forces, gives the user control
        if (Input.GetMouseButton(1))
        {
            if (Tbox != null)
            {
                Ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

        //
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Resets all the objects in the scene. Made like this so that resets can be recorded too
            foreach (var box in Boxes)
            {
                box.resetPos();
            }
            Ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Ball.transform.position = BallInitPos;
            Ball.transform.rotation = BallInitRot;
            
        }
    }
}
