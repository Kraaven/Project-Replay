using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Storage;
using UnityEngine;
using UnityEngine.Windows;

public class firebaseConnect : MonoBehaviour
{
    public FirebaseStorage storage;

    private StorageReference buck;

    private string path;
    // Start is called before the first frame update
    void Start()
    {
        storage = FirebaseStorage.GetInstance("gs://project-replay-df7ea.appspot.com");
        buck = storage.RootReference;
        path = Application.dataPath + "/Saved_Loaded/SavedFile.json";

    }

    public void Store()
    {
        StorageReference saveRef = buck.Child("Saves/SavedFile.json");
        
        saveRef.PutFileAsync(path)
            .ContinueWith((Task<StorageMetadata> task) => {
                if (task.IsFaulted || task.IsCanceled) {
                    Debug.Log(task.Exception.ToString());
                }
                else {
                    Debug.Log("Finished uploading...");
                }
            });
    }

    public void GetSave(GameManagement manager)
    {
        buck.Child("Saves/SavedFile.json").GetFileAsync(path).ContinueWith(task => {
            if (!task.IsFaulted && !task.IsCanceled) {
                Debug.Log("File downloaded successfully.");
                manager.LOADSAVE();
            }
        });
    }
}
