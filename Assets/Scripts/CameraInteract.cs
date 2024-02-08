using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInteract : MonoBehaviour
{
    private boxScript Tbox;

    private void Start()
    {
        Tbox = FindObjectOfType<boxScript>();
    }

    void Update()
    {
        RaycastHit hit;
        Ray R = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(R, out hit, 40))
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
                    box.GetComponent<MeshRenderer>().material.color = Color.red;
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
    }
}
