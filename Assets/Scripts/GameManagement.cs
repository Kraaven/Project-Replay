using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Application = UnityEngine.Application;
using Random = UnityEngine.Random;

public class GameManagement : MonoBehaviour
{
    public List<ActionObject> Objects;
    public bool Recording;
    public bool viewing;

    public int frame = 0;
    public String filepath;
    private void Awake()
    {
        Objects = new List<ActionObject>();
        filepath = Application.dataPath + "/Saved_Loaded";

        if (!Directory.Exists(filepath))
        {
            Directory.CreateDirectory(filepath);
        }
        

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
                foreach (var VARIABLE in Objects)
                {
                    VARIABLE.StopViewing();
                }

                viewing = false;
            }
            
        }

        if (Input.GetKeyDown(KeyCode.R) && !viewing && !Recording && Objects[0].ActionHistory.Count > 0 )
        {
            ReplayData SAVE = new ReplayData(Objects);
            
            
            File.WriteAllText(filepath+"/SavedFile.json",JsonHelper.ToJson(SAVE.ObjectRecords.ToArray()));
        }

        if (Input.GetKeyDown(KeyCode.P) && !viewing && !Recording)
        {
            //Use a Combination of JsonConvert and FromJson to replace all the ActionObjects in the ActionHistory of each object from the loaded data
            Debug.Log("Hello Load");
            
            List<String> LOAD = JsonHelper.FromJson<String>(File.ReadAllText(filepath+"/SavedFile.json")).ToList();

            for (int i = 0; i < Objects.Count; i++)
            {
                Objects[i].SetActionData(LOAD[i]);
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

[Serializable]
public class ReplayData
{
    public List<String> ObjectRecords;

    public ReplayData(List<ActionObject> records)
    {
        ObjectRecords = new List<string>();
        foreach (var obj in records)
        {
            ObjectRecords.Add(obj.GetActionData());
        }
    }
    
}
