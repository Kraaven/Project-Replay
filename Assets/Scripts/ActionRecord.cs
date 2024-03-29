using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

//The Class that holds the data
public class ActionRecord
{
    public Vector3 Position;
    public Quaternion Rotation;

    public ActionRecord(Transform obj)
    {
        Rotation = obj.rotation;
        Position = obj.position;
    }
}
