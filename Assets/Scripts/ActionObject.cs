using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActionObject : MonoBehaviour
{
    public List<ActionRecord> ActionHistory;
    public bool Record;
    public bool Viewing;
    
    public void Start()
    {
        FindObjectOfType<GameManagement>().Objects.Add(this);
        ActionHistory = new List<ActionRecord>();
    }

    public void FixedUpdate()
    {
        if (Record)
        {
            ActionHistory.Add(new ActionRecord(transform));
        }
        
        
    }

    public void StartRecording()
    {
        ActionHistory.Clear();
        Record = true;
    }

    public void startViewing()
    {
        Viewing = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void StopViewing()
    {
        Viewing = false;
        GetComponent<Rigidbody>().isKinematic = false;
    }

    public void loadFrame(int index)
    {
        transform.position = ActionHistory[index].Position;
        transform.rotation = ActionHistory[index].Rotation;
    }
}
