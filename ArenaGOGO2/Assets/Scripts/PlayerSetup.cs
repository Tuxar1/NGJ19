using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup
{
    private struct PlayerData
	{
		public KeyCode JumpKey;
		public string HorizontalAxisName;
		public KeyCode AttackKey;
		public Color PlayerColor;
	}

	private static Dictionary<int, PlayerData> playerID2InputData = new Dictionary<int, PlayerData>();

	public static void SetUpDefaultIfNoData()
	{
		var player1Input = new PlayerData
		{
			JumpKey = KeyCode.UpArrow,
			HorizontalAxisName = "Horizontal",
			AttackKey = KeyCode.RightControl,
			PlayerColor = Color.blue,
		};
		var player2Input = new PlayerData
		{
			JumpKey = KeyCode.W,
			HorizontalAxisName = "Horizontal2",
			AttackKey = KeyCode.LeftControl,
			PlayerColor = Color.red,
		};
		playerID2InputData[0] = player1Input;
		playerID2InputData[1] = player2Input;
	}

	public static KeyCode GetJumpKeyForPlayer(int playerID)
	{
		return playerID2InputData[playerID].JumpKey;
	}

	public static string GetAxisNameForPlayer(int playerID)
	{
		return playerID2InputData[playerID].HorizontalAxisName;
	}

	public static KeyCode GetAttackKeyForPlayer(int playerID)
	{
		return playerID2InputData[playerID].AttackKey;
	}

	public static Color GetColorFromPlayerID(int playerID)
	{
		return playerID2InputData[playerID].PlayerColor;
	}
}
