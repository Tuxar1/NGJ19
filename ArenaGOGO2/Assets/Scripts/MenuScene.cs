using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour
{
	private class JoinedPlayer
	{
		public InputType InputType;
		public int InputID;
		public Color SelectedColor;
	}

	private Color[] SelectableColors =
	{
		Color.red,
		Color.blue,
		Color.green,
		Color.yellow,
		Color.magenta,
		Color.cyan,
		new Color(0.5f,1f,0),
		new Color(1f,0.5f,0),
	};

	public MenuPlayer[] menuPlayers;
	public GameObject StartText;

	private const int MaxPlayers = 8;
	private const int JoyStickInputSetup = 8;
	private const string BaseJoyName = "Joy";
	private const string HorizontalName = "X";
	private const string JumpName = "Jump";
	private const string AttackName = "Attack";

	private const int KeyboardInputCount = 4;

	private JoinedPlayer[] joinedPlayers;

	private bool started = false;

	private enum InputType {
		Keyboard,
		Joystick,
		JoyCon
	}

	// Start is called before the first frame update
	void Start()
    {
		joinedPlayers = new JoinedPlayer[MaxPlayers];
		var random = new System.Random();
		SelectableColors = SelectableColors.OrderBy(x => random.Next()).ToArray();
	}

    // Update is called once per frame
    void Update()
    {
		ReadJoinLeaveInput();
		ReadStartGame();
    }

	void ReadJoinLeaveInput()
	{
		if (started)
		{
			return;
		}
		var joysticks = Input.GetJoystickNames();
		for (int i = 1; i < JoyStickInputSetup + 1; i++)
		{
			if (Input.GetButtonDown(BaseJoyName + i + JumpName))
			{
				var joystickType = InputType.Joystick;
				var joystickName = joysticks[i - 1];
				if (joystickName == "Wireless Gamepad")
				{
					joystickType = InputType.JoyCon;
				}
				AddRemovePlayer(i, joystickType);
			}
		}
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			AddRemovePlayer(1, InputType.Keyboard);
		}
		if (Input.GetKeyDown(KeyCode.W))
		{
			AddRemovePlayer(2, InputType.Keyboard);
		}
		if (Input.GetKeyDown(KeyCode.U))
		{
			AddRemovePlayer(3, InputType.Keyboard);
		}
		if (Input.GetKeyDown(KeyCode.Keypad8))
		{
			AddRemovePlayer(4, InputType.Keyboard);
		}
	}

	void ReadStartGame()
	{
		if (joinedPlayers.Any(jp => jp != null))
		{
			StartText.SetActive(true);
			if (Input.GetButtonDown("Start"))
			{
				started = true;
				StartGame();
			}
		}
		else
		{
			StartText.SetActive(false);
		}
	}

	void AddRemovePlayer(int inputID, InputType inputType)
	{
		int? canJoinID = null;
		for (int i = 0; i < joinedPlayers.Length; i++)
		{
			var player = joinedPlayers[i];
			if (player == null)
			{
				if (canJoinID == null)
				{
					canJoinID = i;
				}
				else
				{
					canJoinID = Mathf.Min(i, canJoinID.Value);
				}
				continue;
			}
			if (player.InputType == inputType && player.InputID == inputID)
			{
				joinedPlayers[i] = null;
				menuPlayers[i].DeSelected();
				return;
			}
		}
		if (canJoinID.HasValue)
		{
			joinedPlayers[canJoinID.Value] = new JoinedPlayer
			{
				InputID = inputID,
				InputType = inputType,
				SelectedColor = GetUnusedColor(),
			};
			menuPlayers[canJoinID.Value].Selected(joinedPlayers[canJoinID.Value].SelectedColor);
		}
	}

	private Color GetUnusedColor()
	{
		var noPlayers = joinedPlayers.All(jp => jp == null);
		if (noPlayers)
		{
			return SelectableColors[0];
		}
		foreach (var color in SelectableColors)
		{
			if (joinedPlayers.All(jp => jp == null || jp.SelectedColor != color))
			{
				return color;
			}
		}
		throw new System.Exception("Could not select a color :O");
	}

	private void StartGame()
	{
		PlayerSetup.ResetPlayers();
		for (int i = 0; i < joinedPlayers.Length; i++)
		{
			var player = joinedPlayers[i];
			if (player == null)
			{
				continue;
			}
			if (player.InputType == InputType.Joystick)
			{
				PlayerSetup.SetUpJoystickPlayer(player.InputID, i, BaseJoyName + player.InputID + HorizontalName, BaseJoyName + player.InputID + JumpName, BaseJoyName + player.InputID + AttackName, player.SelectedColor);
			}
			else if (player.InputType == InputType.JoyCon)
			{
				PlayerSetup.SetUpJoystickPlayer(player.InputID, i, BaseJoyName + player.InputID + HorizontalName + "A", BaseJoyName + player.InputID + JumpName, BaseJoyName + player.InputID + AttackName, player.SelectedColor);
			}
			else
			{
				var jumpKey = GetJumpKeyCodeForKeyBoardInput(player.InputID);
				var attackKey = GetAttackKeyCodeForKeyBoardInput(player.InputID);
				PlayerSetup.SetUpKeyboardPlayer(player.InputID, i, "Key" + player.InputID + HorizontalName, jumpKey, attackKey, player.SelectedColor);
			}
		}
		ScoreSystem.ResetScore();
		SceneManager.LoadScene(1);
	}

	private KeyCode GetJumpKeyCodeForKeyBoardInput(int inputID)
	{
		if (inputID == 1)
		{
			return KeyCode.UpArrow;
		}
		else if (inputID == 2)
		{
			return KeyCode.W;
		}
		else if (inputID == 3)
		{
			return KeyCode.U;
		}
		else if (inputID == 4)
		{
			return KeyCode.Keypad8;
		}
		return KeyCode.Space;
	}

	private KeyCode GetAttackKeyCodeForKeyBoardInput(int inputID)
	{
		if (inputID == 1)
		{
			return KeyCode.RightControl;
		}
		else if (inputID == 2)
		{
			return KeyCode.LeftControl;
		}
		else if (inputID == 3)
		{
			return KeyCode.Space;
		}
		else if (inputID == 4)
		{
			return KeyCode.KeypadEnter;
		}
		return KeyCode.Space;
	}
}
