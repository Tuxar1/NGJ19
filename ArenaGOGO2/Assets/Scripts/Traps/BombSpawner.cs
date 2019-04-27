using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
	public static bool SpawnBombs = false;
	public static bool GravityBombs = true;
	public GameObject BombPrefab;
	public float TimeBetweenBombs = 5f;

	private Dictionary<GameObject, float> timeToBombForPlayer = new Dictionary<GameObject, float>();
	private GameObject[] players;

    // Update is called once per frame
    void Update()
    {
		if (!(!SpawnBombs && GameController.GameHasStarted && PlayerSetup.AllowInput))
		{
			return;
		}
		if (players == null)
		{
			players = GameController.instance.GetPlayerGameObjects();
			foreach (var player in players)
			{
				timeToBombForPlayer[player] = TimeBetweenBombs;
			}
		}
		foreach (var player in players)
		{
			if (player.activeSelf)
			{
				timeToBombForPlayer[player] -= Time.deltaTime;
				if (timeToBombForPlayer[player] < 0)
				{
					timeToBombForPlayer[player] = TimeBetweenBombs;
					SpawnBombOnPlayer(player);
				}
			}
		}
    }

	private void SpawnBombOnPlayer(GameObject player)
	{
		var go = Instantiate(BombPrefab, player.transform.position, Quaternion.identity);
		if (!GravityBombs)
		{
			go.GetComponent<Rigidbody2D>().isKinematic = true;
		}
	}
}
