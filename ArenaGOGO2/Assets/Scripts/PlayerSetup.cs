using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSetup
{
    private struct PlayerData
	{
		public KeyCode JumpKey;
		public string JumpButton;
		public string HorizontalAxisName;
		public KeyCode AttackKey;
		public string AttackButton;
		public Color PlayerColor;
		public int InputID;
		public bool Joystick;
	}

	private static Dictionary<int, PlayerData> playerID2InputData = new Dictionary<int, PlayerData>();

	public static void ResetPlayers()
	{
		playerID2InputData = new Dictionary<int, PlayerData>();
	}

	public static void SetUpJoystickPlayer(int inputID, int playerID, string horizontalName, string jumpButtonName, string attackButtonName, Color color)
	{
		playerID2InputData[playerID] = new PlayerData
		{
			AttackButton = attackButtonName,
			HorizontalAxisName = horizontalName,
			JumpButton = jumpButtonName,
			InputID = inputID,
			PlayerColor = color,
			Joystick = true,
		};
	}

	public static void SetUpKeyboardPlayer(int inputID, int playerID, string horizontalName, KeyCode jumpButtonCode, KeyCode attackButtonCode, Color color)
	{
		playerID2InputData[playerID] = new PlayerData
		{
			AttackKey = attackButtonCode,
			HorizontalAxisName = horizontalName,
			JumpKey = jumpButtonCode,
			InputID = inputID,
			PlayerColor = color,
			Joystick = false,
		};
	}

	public static void SetUpDefaultIfNoData()
	{
		if (playerID2InputData.Any())
		{
			return;
		}
		var player1Input = new PlayerData
		{
			JumpKey = KeyCode.UpArrow,
			HorizontalAxisName = "Key1X",
			AttackKey = KeyCode.RightControl,
			PlayerColor = Color.blue,
		};
		var player2Input = new PlayerData
		{
			JumpKey = KeyCode.W,
			HorizontalAxisName = "Key2X",
			AttackKey = KeyCode.LeftControl,
			PlayerColor = Color.red,
		};
		playerID2InputData[0] = player1Input;
		playerID2InputData[1] = player2Input;
	}

	public static Color GetColorFromPlayerID(int playerID)
	{
		return playerID2InputData[playerID].PlayerColor;
	}

	public static bool GetPlayerJumpedForPlayerID(int playerID)
	{
		var pd = playerID2InputData[playerID];
		if (pd.Joystick)
		{
			return Input.GetButton(pd.JumpButton);
		}
		else
		{
			return Input.GetKey(pd.JumpKey);
		}
	}

	public static bool GetPlayerAttackedForPlayerID(int playerID)
	{
		var pd = playerID2InputData[playerID];
		if (pd.Joystick)
		{
			return Input.GetButtonDown(pd.AttackButton);
		}
		else
		{
			return Input.GetKeyDown(pd.AttackKey);
		}
	}

	public static float GetPlayerAxisForPlayerID(int playerID)
	{
		var pd = playerID2InputData[playerID];
		return Input.GetAxis(pd.HorizontalAxisName);
	}

	public static Tuple<int, Color>[] GetPlayerColors()
	{
		return playerID2InputData.Select(pd => new Tuple<int, Color>(pd.Key, pd.Value.PlayerColor)).ToArray();
	}

	public static int[] GetPlayerIDs()
	{
		return playerID2InputData.Keys.ToArray();
	}
}
