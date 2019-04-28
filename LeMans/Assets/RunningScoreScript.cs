using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunningScoreScript : MonoBehaviour
{
    public static int player1Score = 0;
    public static int player2Score = 0;
    public static int winScore = 5;

    public Text textPlayer1Score;
    public Text textPlayer2Score;
    public Text winTitle;
    public GameObject winTitleGameObject;

    public void Start()
    {
        RefreshScores();
    }

    private void RefreshScores()
    {
        // REFRESH SCORES
        textPlayer1Score.text = "" + player1Score;
        textPlayer2Score.text = "" + player2Score;
    }

    public void givePlayer1Points()
    {
        player1Score++;
        RefreshScores();
        CheckWinner();
    }

    public void givePlayer2Points()
    {
        player2Score++;
        RefreshScores();
        CheckWinner();
    }

    private void CheckWinner()
    {
        // SHOW WINNER
        if (player1Score == winScore ||
            player2Score == winScore)
        {
            winTitleGameObject.SetActive(true);

            if (player1Score == winScore) winTitle.text = "Bottom Won";
            if (player2Score == winScore) winTitle.text = "Top Won";

            StartCoroutine(disableWinTitle());
        }
        else
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    IEnumerator disableWinTitle()
    {
        var fireworks = FindObjectOfType<FireworksStart>();
        fireworks.SetGameWon();

        yield return new WaitForSeconds(5.0f);
        winTitleGameObject.SetActive(false);
        Application.LoadLevel(0);
    }
}
