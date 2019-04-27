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
    BombsUnderYou,
	BombsUnderYouWithGravity,
    TrapsDontKillYou,
    PlatformsAreDeadly,
    SpikesAreSafe,
    PlatformsInvisible,
    PlatformsDisappearing,
    HardMode,
}

public class WinController : MonoBehaviour
{
    private class GameModes
    {
        public WinConditions winCondition;
        public EnvironmentMods environmentMods;
        public GameModes(WinConditions winCondition, EnvironmentMods environmentMods)
        {
            this.winCondition = winCondition;
            this.environmentMods = environmentMods;
        }
    }

    private EnvironmentMods environmentMod;
    public WinConditions winCondition { get; private set; }
    public static WinController instance = null;
    public GameObject[] playerRefs;
    public PlatformScript[] platforms;
    public int amountOfPlayersHitWin;

    public GameObject hardModeHolder;

    public Action<Color> WinActions;
    public PickupFlag Flag;

    private GameModes[] gameModes = {
        new GameModes(WinConditions.OneReachGoal, EnvironmentMods.Standard),
        new GameModes(WinConditions.CaptureTheFlag, EnvironmentMods.Standard),
        new GameModes(WinConditions.OneReachGoal, EnvironmentMods.HardMode),
        new GameModes(WinConditions.TouchAllPlatforms, EnvironmentMods.Standard),
        new GameModes(WinConditions.OneReachGoal, EnvironmentMods.BombsUnderYou),
    };
    private int gamesModesIterator = 0;

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
        GameController.instance.RestartAction += PickNextGameMode;
        GameController.instance.RestartAction += SetEnvironmentMods;
        PickNextGameMode();
        SetEnvironmentMods();
        InitializeWinCondition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEnvironmentMods()
    {
        switch(environmentMod)
        {
            case EnvironmentMods.HardMode:
                hardModeHolder.SetActive(true);
                break;
			case EnvironmentMods.BombsUnderYou:
				BombSpawner.SpawnBombs = true;
				BombSpawner.GravityBombs = false;
				break;
			case EnvironmentMods.BombsUnderYouWithGravity:
				BombSpawner.SpawnBombs = true;
				BombSpawner.GravityBombs = true;
				break;
            default:
                hardModeHolder.SetActive(false);
                break;
        }
    }

    public void PickNextGameMode()
    {
        winCondition = gameModes[gamesModesIterator].winCondition;
        environmentMod = gameModes[gamesModesIterator].environmentMods;
        gamesModesIterator++;
        if (gamesModesIterator > gameModes.Length - 1)
        {
            gamesModesIterator = 0;
        }
    }

    public void InitializeWinCondition()
    {
        amountOfPlayersHitWin = 0;
        if (winCondition == WinConditions.CaptureTheFlag)
            Flag.StartCTF();
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
                if (platforms.Where(x => x.isActiveAndEnabled).All(x => x.hasBeenTouched))
                {
                    Win(winCondition);
                }
                break;
            case WinConditions.LastManStanding:

                break;
            case WinConditions.CaptureTheFlag:
                Win(playerGo, winCondition);
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
