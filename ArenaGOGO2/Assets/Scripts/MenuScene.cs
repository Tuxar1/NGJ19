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

	public Color[] SelectableColors =
	{
		Color.red,
		Color.blue,
		Color.green,
		Color.yellow,
	};

	public MenuPlayer[] menuPlayers;
	public GameObject StartText;

	private const int MaxPlayers = 4;
	private const int JoyStickInputSetup = 4;
	private const string BaseJoyName = "Joy";
	private const string HorizontalName = "X";
	private const string JumpName = "Jump";
	private const string AttackName = "Attack";

	private const int KeyboardInputCount = 2;

	private JoinedPlayer[] joinedPlayers;

	private bool started = false;

	private enum InputType {
		Keyboard,
		Joystick
	}

	// Start is called before the first frame update
	void Start()
    {
		joinedPlayers = new JoinedPlayer[MaxPlayers];
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
		for (int i = 1; i < JoyStickInputSetup + 1; i++)
		{
			if (Input.GetButtonDown(BaseJoyName + i + JumpName))
			{
				AddRemovePlayer(i, InputType.Joystick);
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
			else
			{
				var jumpKey = player.InputID == 1 ? KeyCode.UpArrow : KeyCode.W;
				var attackKey = player.InputID == 1 ? KeyCode.RightControl : KeyCode.LeftControl;
				PlayerSetup.SetUpKeyboardPlayer(player.InputID, i, "Key" + player.InputID + HorizontalName, jumpKey, attackKey, player.SelectedColor);
			}
		}
		SceneManager.LoadScene(1);
	}
}
