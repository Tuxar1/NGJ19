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

	void Start()
	{
		GetComponent<SpriteRenderer>().color = PlayerSetup.GetColorFromPlayerID(PlayerID);
	}

	// Update is called once per frame
	void Update()
    {
        if (Input.GetKeyDown(PlayerSetup.GetJumpKeyForPlayer(PlayerID)))
		{
			JumpChanged(true);
		}
		XAxis(Input.GetAxis(PlayerSetup.GetAxisNameForPlayer(PlayerID)));
		if (Input.GetKeyDown(PlayerSetup.GetAttackKeyForPlayer(PlayerID)))
		{
			AttackClicked(true);
		}
	}
}
