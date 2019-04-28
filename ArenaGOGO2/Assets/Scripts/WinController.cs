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
	LowGravity,
	HighGravity,
	LerpingGravity,
	HeavyWinds,
    DeadlyIntervalPlatforms,
}

public class WinController : MonoBehaviour
{
    private class GameModes
    {
        public WinConditions winCondition;
        public EnvironmentMods environmentMods;
		public string name;
        public GameModes(WinConditions winCondition, EnvironmentMods environmentMods, string name)
        {
            this.winCondition = winCondition;
            this.environmentMods = environmentMods;
			this.name = name;
        }
    }

    private EnvironmentMods environmentMod;
    public WinConditions winCondition { get; private set; }
    public static WinController instance = null;
    public GameObject[] playerRefs;
    public PlatformScript[] platforms;
    public int amountOfPlayersHitWin;

    public GameObject hardModeHolder;

	public UIController UIController;

    public Action<Color> WinActions;
    public PickupFlag Flag;

    private bool isDeadlyPlatforms = false;
    public int maxDeadlyPlatforms = 8;
    public float deadlyPlatformInterval = 2f;
    public float startDeadlyPlatformsInterval = 1f;
    private Coroutine deadlyPlatformRoutine;

    private GameModes[] gameModes = {
        new GameModes(WinConditions.OneReachGoal, EnvironmentMods.Standard, "You cannot walk through doors"),
        new GameModes(WinConditions.CaptureTheFlag, EnvironmentMods.Standard, "... And back again"),
        new GameModes(WinConditions.TouchAllPlatforms, EnvironmentMods.Standard, ""),
        new GameModes(WinConditions.OneReachGoal, EnvironmentMods.BombsUnderYou, "Watch out!"),
		new GameModes(WinConditions.CaptureTheFlag, EnvironmentMods.HeavyWinds, "???"),
		new GameModes(WinConditions.OneReachGoal, EnvironmentMods.LowGravity, "The moon"),
        new GameModes(WinConditions.CaptureTheFlag, EnvironmentMods.DeadlyIntervalPlatforms, "What do the colors signify?"),
		new GameModes(WinConditions.OneReachGoal, EnvironmentMods.HardMode, "Are you tough enough?"),
		new GameModes(WinConditions.CaptureTheFlag, EnvironmentMods.LerpingGravity, "???"),
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
		GameController.instance.AfterRestartAction += InitializeWinCondition;
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
		ResetEnvironmentMods();
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
			case EnvironmentMods.LowGravity:
				GravityController.mode = GravityController.GravityMode.Low;
				break;
			case EnvironmentMods.HighGravity:
				GravityController.mode = GravityController.GravityMode.High;
				break;
			case EnvironmentMods.LerpingGravity:
				GravityController.mode = GravityController.GravityMode.Pulsating;
				break;
			case EnvironmentMods.HeavyWinds:
				GravityController.mode = GravityController.GravityMode.HeavyWinds;
				break;
            case EnvironmentMods.DeadlyIntervalPlatforms:
                isDeadlyPlatforms = true;
                StartCoroutine(HandleChangingDeadlyPlatforms());
                break;
			default:
                break;
        }
    }

	public void ResetEnvironmentMods()
	{
		hardModeHolder.SetActive(false);
		BombSpawner.SpawnBombs = false;
		BombSpawner.GravityBombs = false;
		GravityController.mode = GravityController.GravityMode.Default;
		Flag.ResetState();
        ResetPlatforms();
	}

	public void PickNextGameMode()
    {
        winCondition = gameModes[gamesModesIterator].winCondition;
        environmentMod = gameModes[gamesModesIterator].environmentMods;
		UIController.GameModeName(gameModes[gamesModesIterator].name);
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
				if (asker.tag == "Flag")
				{
					Win(playerGo, winCondition);
				}
				break;
        }
    }

    public Action GetEnvironmentAction()
    {
        return null;
    }

    public void Win(WinConditions winCondition)
    {
		GameController.GameHasStarted = false;
		foreach (var player in playerRefs)
		{
			ScoreSystem.AwardPoints(player.GetComponent<PlayerInput>().PlayerID, winCondition);
		}
		WinActions(Color.white);
	}

	public void Win(GameObject player, WinConditions winCondition)
    {
		GameController.GameHasStarted = false;
		var playerID = player.GetComponent<PlayerInput>().PlayerID;
		ScoreSystem.AwardPoints(playerID, winCondition);
		var playerColor = PlayerSetup.GetColorFromPlayerID(playerID);
		WinActions(playerColor);
	}

    private IEnumerator HandleChangingDeadlyPlatforms()
    {
        isDeadlyPlatforms = true;
        var activePlatforms = platforms.Where(p => p.isActiveAndEnabled);
        while(isDeadlyPlatforms)
        {
            if(activePlatforms.Count(p => p.IsInDeadlySequence) < 8)
            {
                var availablePlatforms = activePlatforms.Where(p => !p.IsInDeadlySequence).ToArray();
                int notStartedCount = availablePlatforms.Count();
                var randomNumber = UnityEngine.Random.Range(0, notStartedCount);
                availablePlatforms[randomNumber].InitiateDeadlyPlatform(deadlyPlatformInterval);
                var secondRandomNumber = UnityEngine.Random.Range(0, notStartedCount);
                int iterator = 0, maxTries = 50;
                while (secondRandomNumber == randomNumber && iterator < maxTries)
                {
                    secondRandomNumber = UnityEngine.Random.Range(0, notStartedCount);
                    iterator++;
                }
                availablePlatforms[secondRandomNumber].InitiateDeadlyPlatform(deadlyPlatformInterval);
            }
            yield return new WaitForSeconds(startDeadlyPlatformsInterval);
        }
    }

    public void ResetPlatforms()
    {
        isDeadlyPlatforms = false;
        foreach(var platform in platforms)
        {
            platform.ResetPlatform();
        }
    }

}
