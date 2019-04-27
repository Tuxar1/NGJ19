﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WinConditions
{
    OneReachGoal,
    AllReachGoal,
    TouchAllPlatforms,
    LastManStanding,
    CaptureTheFlag,
}

public enum EnvironmentMods
{
    Standard,
    SpikesUnderYou,
    TrapsDontKillYou,
    PlatformsAreDeadly,
    SpikesAreSafe,
    PlatformsInvisible,
    PlatformsDisappearing,
}

public class WinController : MonoBehaviour
{
    public EnvironmentMods environmentMods;
    public WinConditions winCondition;
    public static WinController instance = null;
    public GameObject[] playerRefs;
    public PlatformScript[] platforms;
    public int amountOfPlayersHitWin;

    public Action<string> WinActions;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRefs = GameObject.FindGameObjectsWithTag("Player");
		platforms = GameObject.FindObjectsOfType<PlatformScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeWinCondition()
    {
        amountOfPlayersHitWin = 0;
    }

    public void CheckWinCondition(GameObject asker, GameObject playerGo)
    {
		if (!GameController.GameHasStarted)
		{
			return;
		}
        switch (winCondition)
        {
            case WinConditions.OneReachGoal:
                if (asker.GetComponent<WinDoor>() != null)
                {
                    Win(playerGo);
                }
                break;
            case WinConditions.AllReachGoal:
                if (asker.GetComponent<WinDoor>() != null)
                    amountOfPlayersHitWin++;
                if (amountOfPlayersHitWin >= playerRefs.Length)
                {
                    Win();
                }
                break;
            case WinConditions.TouchAllPlatforms:
                if (platforms.All(x => x.hasBeenTouched))
                {
                    Win();
                }
                break;
            case WinConditions.LastManStanding:

                break;
            case WinConditions.CaptureTheFlag:
                
                break;
        }
    }

    public Action GetEnvironmentAction()
    {
        return null;
    }

    public void Win()
    {
        WinActions(null);
    }

    public void Win(GameObject player)
    {
        WinActions(player.name);
    }

}
