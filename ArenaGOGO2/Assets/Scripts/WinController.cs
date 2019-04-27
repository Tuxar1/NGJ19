using System;
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
    public WinConditions winPossibilities;
    public static WinController instance = null;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckWinCondition(GameObject asker, GameObject playerGo)
    {
        switch (winPossibilities)
        {
            case WinConditions.OneReachGoal:
                if (asker.GetComponent<WinDoor>() != null)
                {
                    //
                }
                break;
            case WinConditions.AllReachGoal:
                break;
            case WinConditions.TouchAllPlatforms:
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

    }

}
