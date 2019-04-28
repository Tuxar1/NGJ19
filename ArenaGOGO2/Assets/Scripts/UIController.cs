using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text WinText;
	public PlayerScore[] PlayerScores;
	public GameObject Panel;
	public GameObject StartText;
	private bool showing = false;

    // Start is called before the first frame update
    void Start()
    {
        WinController.instance.WinActions += HandlePlayerWon;
        GameController.instance.RestartAction += HandleRestart;
        HandleRestart();
    }

	private void Update()
	{
		if (showing)
		{
			if(PlayerSetup.GetAnyPressedStart())
			{
				GameController.instance.HandleRestart();
			}
		}
	}

	private void GameEnded()
	{

	}

    public void HandlePlayerWon(Color winnerColor)
    {
		PlayerSetup.AllowInput = false;

		var playersAndColors = PlayerSetup.GetPlayerColors();
		foreach (var player in playersAndColors)
		{
			foreach (var playerScore in PlayerScores)
			{
				if (playerScore.PlayerID == player.Item1)
				{
					playerScore.Setup(player.Item2, ScoreSystem.GetScoreFromPlayerID(player.Item1).ToString());
					playerScore.gameObject.SetActive(true);
				}
			}
		}
		WinText.color = winnerColor;
		if (winnerColor == Color.white)
		{
			WinText.text = "Everyone won!";
		}
		else
		{
			WinText.text = $"{GetColorNameFromColor(winnerColor)} wins this round!";
		}
		Panel.SetActive(true);
		StartText.SetActive(true);
		showing = true;
    }

    public void HandleRestart()
    {
		foreach (var playerScore in PlayerScores)
		{
			playerScore.gameObject.SetActive(false);
		}
		Panel.SetActive(false);
		StartText.SetActive(false);
		showing = false;
    }

	private string GetColorNameFromColor(Color color)
	{
		if (color == Color.red)
		{
			return "Red";
		} 
		if (color == Color.blue)
		{
			return "Blue";
		}
		if (color == Color.green)
		{
			return "Green";
		}
		if (color == Color.yellow)
		{
			return "Yellow";
		}
		if (color == Color.magenta)
		{
			return "Magenta";
		}
		if (color == Color.cyan)
		{
			return "Cyan";
		}
		if (color == new Color(0.5f,0,1f))
		{
			return "Purple";
		}
		if (color == new Color(1f,0.5f,0))
		{
			return "Orange";
		}

		Debug.LogError("I do not know the name of that color");
		return "I do not know the name of that color";
	}

	public void GameModeName(string name)
	{
		WinText.color = Color.black;
		WinText.text = name;
	}
}
