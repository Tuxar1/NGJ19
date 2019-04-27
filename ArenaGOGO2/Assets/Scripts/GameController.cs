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
    public Action RestartAction;
    public Action AfterRestartAction;

	public static bool GameHasStarted = false;
	public float ContinuousRespawnTime = 5f;

	private CameraShaker camShake;

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
		GameHasStarted = true;
		PlayerSetup.AllowInput = true;
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
        RestartAction();
        foreach (var player in playerSpawnPos.Keys)
        {
			StartCoroutine(ResetPlayer(player, 0f));
        }
        AfterRestartAction();
		PlayerSetup.AllowInput = true;
		GameHasStarted = true;
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
					break;
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

	public void PlayerDied(GameObject player)
	{
		if (camShake == null)
		{
			camShake = Camera.main.GetComponent<CameraShaker>();
		}
		camShake.DoShake();

		StartCoroutine(ResetPlayer(player, ContinuousRespawnTime));
	}

	private IEnumerator ResetPlayer(GameObject playerGO, float time)
	{
		yield return new WaitForSeconds(time);
		playerGO.transform.position = playerSpawnPos[playerGO];
		playerGO.GetComponent<PlayerDeath>().Reset();
		playerGO.SetActive(true);
	}

	public GameObject[] GetPlayerGameObjects()
	{
		return playerSpawnPos.Keys.ToArray();
	}
}

