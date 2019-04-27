using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem
{
	public class ScoreData
	{
		public WinConditions Condition;
		public Color Color;
		public int PointsAwarded;
	}

	public static Dictionary<int, int> playerID2Score;
	private static List<ScoreData> scoreData = new List<ScoreData>();

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
		var points = WinCondition2Points(winCondition);
		var playerColor = PlayerSetup.GetColorFromPlayerID(playerID);
		scoreData.Add(new ScoreData
		{
			Color = playerColor,
			Condition = winCondition,
			PointsAwarded = points,
		});
		playerID2Score[playerID] += points;
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
				Debug.LogError($"NYI: {winConditions}");
				return 1;
		}
	}

	public static int GetScoreFromPlayerID(int playerID)
	{
		return playerID2Score[playerID];
	}

	public static List<ScoreData> GetLatestAwardedPoints()
	{
		return scoreData;
	}

	public static void ClearScoreData()
	{
		scoreData = new List<ScoreData>();
	}
}
