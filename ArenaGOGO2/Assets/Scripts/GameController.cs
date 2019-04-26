using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
    public SpikeAction spikeAction;
    public static GameController instance = null;
    private GameObject[] players;
    private Dictionary<GameObject, Vector3> playerSpawnPos;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        players = GameObject.FindGameObjectsWithTag("Player");
        playerSpawnPos = players.ToDictionary(k => k, v => new Vector3(v.transform.position.x, v.transform.position.y, v.transform.position.z));
    }



    //Update is called every frame.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            HandleRestart();
        }
    }

    public void HandleRestart()
    {
        foreach (var player in playerSpawnPos.Keys)
        {
            player.transform.position = playerSpawnPos[player];
            player.SetActive(true);
        }
    }
}

