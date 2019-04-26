using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	public int PlayerID = 0;

	public Action<bool> JumpChanged;
	public Action<float> XAxis;
	public Action<bool> AttackClicked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(getJumpKeyForPlayer(PlayerID)))
		{
			JumpChanged(true);
		}
		XAxis(Input.GetAxis(getAxisNameForPlayer(PlayerID)));
		if (Input.GetKeyDown(getAttackKeyForPlayer(PlayerID)))
		{
			AttackClicked(true);
		}
	}

	private static KeyCode getJumpKeyForPlayer(int playerID)
	{
		if (playerID == 0)
		{
			return KeyCode.UpArrow;
		}
		else if (playerID == 1)
		{
			return KeyCode.W;
		}
		return KeyCode.Space;
	}

	private static string getAxisNameForPlayer(int playerID)
	{
		if (playerID == 0)
		{
			return "Horizontal";
		}
		else if (playerID == 1)
		{
			return "Horizontal2";
		}
		return "";
	}

	private static KeyCode getAttackKeyForPlayer(int playerID)
	{
		if (playerID == 0)
		{
			return KeyCode.RightControl;
		}
		else if (playerID == 1)
		{
			return KeyCode.LeftControl;
		}
		return KeyCode.Space;
	}
}
