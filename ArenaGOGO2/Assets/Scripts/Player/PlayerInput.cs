﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	public int PlayerID = 0;

	public Action<bool> JumpChanged;
	public Action<float> XAxis;
	public Action<bool> AttackClicked;

	void Start()
	{
		GetComponent<SpriteRenderer>().color = PlayerSetup.GetColorFromPlayerID(PlayerID);
	}

	// Update is called once per frame
	void Update()
    {
        if (PlayerSetup.GetPlayerJumpedForPlayerID(PlayerID))
		{
			JumpChanged(true);
		}
		XAxis(PlayerSetup.GetPlayerAxisForPlayerID(PlayerID));
		if (PlayerSetup.GetPlayerAttackedForPlayerID(PlayerID))
		{
			AttackClicked(true);
		}
	}
}
