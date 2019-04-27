using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text WinText;

    // Start is called before the first frame update
    void Start()
    {
        WinController.instance.WinActions += HandlePlayerWon;
        GameController.instance.RestartAction += HandleRestart;
        HandleRestart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandlePlayerWon(string playerName)
    {
        string winText = "Good job everyone is a winner!";
        if (!string.IsNullOrWhiteSpace(playerName))
        {
            winText = "Player " + playerName + " won!";
        }
        WinText.text = winText;
    }

    public void HandleRestart()
    {
        WinText.text = "";
    }
}
