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
    public UIscript UI;

    public int frame = 0;
    public String filepath;
    private int LastFrame;
    public TMP_Text Frames;
    public firebaseConnect ServerConnection;
    private void Awake()
    {
        //Creating list of ActionObjects and set filepath
        Objects = new List<ActionObject>();
        filepath = Application.dataPath + "/Saved_Loaded";

        //Create the Directory if it does not exist
        if (!Directory.Exists(filepath))
        {
            Directory.CreateDirectory(filepath);
        }
        
        //Set state to starting
        UI.SetView("Simulation");
        Frames.gameObject.SetActive(false);

        ServerConnection = GetComponent<firebaseConnect>();
    }

    public void Update()
    {
        //Input for recording
        if (Input.GetKeyDown(KeyCode.F) && !viewing)
        {
            if (!Recording)
            {
                foreach (var obj in Objects)
                {
                    obj.StartRecording();
                }

                Recording = true;
                UI.SetView("Recording");
            }
            else
            {
                foreach (var obj in Objects)
                {
                    obj.Record = false;
                }
                Recording = false;
                UI.SetView("Simulation");
                UI.SeeMSG("Recording in RAM");
            }
        }

        
        //Input for Viewing the recording
        if (Input.GetKeyDown(KeyCode.V) && !Recording && Objects[0].ActionHistory.Count > 0)
        {
            if (!viewing)
            {
                foreach (var VARIABLE in Objects)
                {
                    VARIABLE.startViewing();
                }

                LastFrame = Objects[0].ActionHistory.Count - 1;
                UI.SetView("Viewing");
                Frames.gameObject.SetActive(true);
                viewing = true;
                frame = 0;
            }
            else
            {
                foreach (var VARIABLE in Objects)
                {
                    VARIABLE.stopViewing();
                }
                UI.SetView("Simulation");
                Frames.gameObject.SetActive(false);
                viewing = false;
            }
            
        }

        //To save the data from all the objects into 1 file
        if (Input.GetKeyDown(KeyCode.R) && !viewing && !Recording && Objects[0].ActionHistory.Count > 0 )
        {
            ReplayData SAVE = new ReplayData(Objects);
            
            
            File.WriteAllText(filepath+"/SavedFile.json",JsonHelper.ToJson(SAVE.ObjectRecords.ToArray()));
            ServerConnection.Store();
            
            UI.SeeMSG("Saved Recording from RAM to external server");
        }

        //Take the 1 file, and deconstruct it into the action data for each object
        if (Input.GetKeyDown(KeyCode.P) && !viewing && !Recording)
        {
            ServerConnection.GetSave(this);
        }
        
        
        //Quest the Application
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void LOADSAVE()
    {
        List<String> LOAD = JsonHelper.FromJson<String>(File.ReadAllText(filepath+"/SavedFile.json")).ToList();

        //set the data for each object
        for (int i = 0; i < Objects.Count; i++)
        {
            Objects[i].SetActionData(LOAD[i]);
        }
            
        UI.SeeMSG("Replaced Recording in RAM from saved external server"); 
    }
    private void FixedUpdate()
    {
        //Controls for Viewing the recording
        if (Input.GetKey(KeyCode.LeftArrow) && viewing && frame > 0)
        {
            frame -= 1;
            Frames.text = "Frame " + frame + ":" + LastFrame;
        }

        if (Input.GetKey(KeyCode.RightArrow) && viewing && frame < Objects[0].ActionHistory.Count-1)
        {
            frame += 1;
            Frames.text = "Frame " + frame + ":" + LastFrame;
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

//The Class created to hold all the data
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

