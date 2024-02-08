using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public List<ActionObject> Objects;
    public bool Recording;
    public bool viewing;

    public int frame = 0;
    private void Awake()
    {
        Objects = new List<ActionObject>();
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !viewing)
        {
            if (!Recording)
            {
                foreach (var obj in Objects)
                {
                    obj.StartRecording();
                }

                Recording = true;
            }
            else
            {
                foreach (var obj in Objects)
                {
                    obj.Record = false;
                }
                Recording = false;
                
            }
        }

        if (Input.GetKeyDown(KeyCode.V) && !Recording)
        {
            if (!viewing)
            {
                viewing = true;
                frame = 0;
            }
            else
            {
                viewing = false;
            }
            
        }

        
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && viewing && frame > 0)
        {
            frame -= 1;
        }

        if (Input.GetKey(KeyCode.RightArrow) && viewing && frame < Objects[0].ActionHistory.Count-1)
        {
            frame += 1;
        }
        
        if (viewing)
        {
            foreach (var obj in Objects)
            {
                obj.loadFrame(frame);
            }
        }
    }
}
