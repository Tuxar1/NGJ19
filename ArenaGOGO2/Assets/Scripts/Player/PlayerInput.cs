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

	public void Setup(int playerID, Color color)
	{
		PlayerID = playerID;
		GetComponent<SpriteRenderer>().color = color;
	}

	// Update is called once per frame
	void Update()
    {
		JumpChanged(PlayerSetup.GetPlayerJumpedForPlayerID(PlayerID));
		XAxis(PlayerSetup.GetPlayerAxisForPlayerID(PlayerID));
		if (PlayerSetup.GetPlayerAttackedForPlayerID(PlayerID))
		{
			AttackClicked(true);
		}
	}
}
