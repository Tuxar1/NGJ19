using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunningScoreScript : MonoBehaviour
{
    public static int player1Score = 0;
    public static int player2Score = 0;

    public Text textPlayer1Score;
    public Text textPlayer2Score;

    public void Start()
    {
        textPlayer1Score.text = "" + player1Score;
        textPlayer2Score.text = "" + player2Score;
    }

    public void givePlayer1Points()
    {
        player1Score++;
    }

     public void givePlayer2Points()
    {
        player2Score++;
    }
}
