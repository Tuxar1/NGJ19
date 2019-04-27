using System;
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
    HardMode,
}

public class WinController : MonoBehaviour
{
    public EnvironmentMods environmentMods;
    public WinConditions winCondition;
    public static WinController instance = null;
    public GameObject[] playerRefs;
    public PlatformScript[] platforms;
    public int amountOfPlayersHitWin;

    public GameObject hardModeHolder;

    public Action<Color> WinActions;

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
        GameController.instance.RestartAction = SetEnvironmentMods;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEnvironmentMods()
    {
        switch(environmentMods)
        {
            case EnvironmentMods.HardMode:
                hardModeHolder.SetActive(true);
                break;
            default:
                hardModeHolder.SetActive(false);
                break;
        }
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
                    Win(playerGo, winCondition);
                }
                break;
            case WinConditions.AllReachGoal:
                if (asker.GetComponent<WinDoor>() != null)
                    amountOfPlayersHitWin++;
                if (amountOfPlayersHitWin >= playerRefs.Length)
                {
                    Win(winCondition);
                }
                break;
            case WinConditions.TouchAllPlatforms:
                if (platforms.All(x => x.hasBeenTouched))
                {
                    Win(winCondition);
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

    public void Win(WinConditions winCondition)
    {
		foreach (var player in playerRefs)
		{
			ScoreSystem.AwardPoints(player.GetComponent<PlayerInput>().PlayerID, winCondition);
		}
		WinActions(Color.white);
	}

	public void Win(GameObject player, WinConditions winCondition)
    {
		var playerID = player.GetComponent<PlayerInput>().PlayerID;
		ScoreSystem.AwardPoints(playerID, winCondition);
		var playerColor = PlayerSetup.GetColorFromPlayerID(playerID);
		WinActions(playerColor);
	}

}
