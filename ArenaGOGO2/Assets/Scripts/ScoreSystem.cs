using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem
{
	public static Dictionary<int, int> playerID2Score;



	public static void ResetScore()
	{
		playerID2Score = new Dictionary<int, int>();
		var playerIDs = PlayerSetup.GetPlayerIDs();
		foreach (var id in playerIDs)
		{
			playerID2Score[id] = 0;
		}
	}

	public static void AwardPoints(int playerID, WinConditions winCondition)
	{
		playerID2Score[playerID] += WinCondition2Points(winCondition);
	}

	private static int WinCondition2Points(WinConditions winConditions)
	{
		switch (winConditions)
		{
			case WinConditions.OneReachGoal:
				return 1;
			case WinConditions.AllReachGoal:
				return 1;
			case WinConditions.TouchAllPlatforms:
				return 1;
			case WinConditions.LastManStanding:
				return 1;
			case WinConditions.CaptureTheFlag:
				return 1;
			default:
				Debug.Log("NYI");
				return 1;
		}
	}
}
