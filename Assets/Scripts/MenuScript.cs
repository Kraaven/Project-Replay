using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    //Main Menu
    public void Startgame()
    {
        SceneManager.LoadScene(1);
    }
}
