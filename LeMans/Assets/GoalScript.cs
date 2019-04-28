using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    public RunningScoreScript runningScoreScript;

    private void OnTriggerEnter(Collider other)
    {
        Application.LoadLevel(Application.loadedLevel);

        UpdatePlayerPoints(other);
    }

    private void UpdatePlayerPoints(Collider other)
    {
        PlayerKeysScript keyScript = other.gameObject.transform.parent.GetComponent<PlayerKeysScript>();
        
        runningScoreScript = FindObjectOfType<RunningScoreScript>();

        if (keyScript != null)
        {
            if (keyScript.PlayerID == PlayerID.Player1)
            {
                runningScoreScript.givePlayer1Points();
            }
            else if (keyScript.PlayerID == PlayerID.Player2)
            {   
                runningScoreScript.givePlayer2Points();
            }
        }
    }
}
