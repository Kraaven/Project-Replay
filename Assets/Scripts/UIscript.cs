using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIscript : MonoBehaviour
{
    //Just some simple functions for the UI
    public TMP_Text gameState;
    public TMP_Text ActionMsg;

    public void SetView(String ViewState)
    {
        gameState.text = ViewState;
    }

    //For the text in the bottom
    public void SeeMSG(String Msg)
    {
        ActionMsg.text = Msg;
        Invoke("Deletetext", 2);
    }

    private void Deletetext()
    {
        ActionMsg.text = "";
    }
}
