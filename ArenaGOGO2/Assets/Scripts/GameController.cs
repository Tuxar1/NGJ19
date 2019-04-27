using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
	public GameObject PlayerPrefab;
    public SpikeAction spikeAction;
    public static GameController instance = null;
    private SpawnPoint[] Spawners;
    private Dictionary<GameObject, Vector3> playerSpawnPos = new Dictionary<GameObject, Vector3>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

		PlayerSetup.SetUpDefaultIfNoData();
		SetupSpawners();
		SpawnPlayers();
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

	private void SetupSpawners()
	{
		var spawnersGO = GameObject.FindGameObjectsWithTag("Spawner");
		Spawners = new SpawnPoint[spawnersGO.Length];
		for (int i = 0; i < spawnersGO.Length; i++)
		{
			Spawners[i] = spawnersGO[i].GetComponent<SpawnPoint>();
		}
	}

	private void SpawnPlayers()
	{
		var playerData = PlayerSetup.GetPlayerColors();
		foreach (var player in playerData)
		{
			foreach (var spawner in Spawners)
			{
				if (player.Item1 == spawner.PlayerID)
				{
					SpawnPlayer(player.Item1, player.Item2, spawner);
				}
			}
		}
	}

	private void SpawnPlayer(int playerID, Color color, SpawnPoint spawn)
	{
		var go = Instantiate(PlayerPrefab, spawn.transform.position, Quaternion.identity);
		go.GetComponent<PlayerInput>().Setup(playerID, color);
		playerSpawnPos[go] = spawn.transform.position;
	}
}

