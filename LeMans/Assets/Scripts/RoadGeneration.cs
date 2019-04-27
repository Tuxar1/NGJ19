using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGeneration : MonoBehaviour
{
    public GameObject Finish;
    public PlayerKeysScript Player1;
    public PlayerKeysScript Player2;
    public int Scale = 10;

    private static int sizeX = 1000;
    private static int sizeY = 1000;
    private bool[,] map = new bool[sizeX, sizeY];

    public Vector2 StartPosition = Vector2.zero;
    private GameObject creator;

    void Start()
    {
        SetupMap();
        addPlayers();
    }

    // Start is called before the first frame update
    void SetupMap()
    {
        creator = new GameObject();
        StartPosition = new Vector2(sizeX / 2, sizeX / 2);
        creator.transform.position = new Vector3(StartPosition.x, 0, StartPosition.y);

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                map[x, y] = false;
            }
        }
        int currentDirection = 1;
        try
        {
            for (int i = 0; i < 100; i++)
            {
            
                if (map[(int)creator.transform.position.x, (int)creator.transform.position.z] == false)
                {
                    map[(int)creator.transform.position.x, (int)creator.transform.position.z] = true;
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.localScale = new Vector3(Scale, 1, Scale);
                    cube.gameObject.tag = "Road";
                    cube.transform.position = new Vector3(creator.transform.position.x, 0, creator.transform.position.z);
                }

                //Up
                if (currentDirection < 2)
                {
                    creator.transform.Rotate(Vector3.up, -90);
                    creator.transform.position += creator.transform.forward * Scale;
                }
                else if (currentDirection > 7)
                {
                    creator.transform.Rotate(Vector3.up, 90);
                    creator.transform.position += creator.transform.forward * Scale;
                }
                else
                {
                    creator.transform.position = creator.transform.position + creator.transform.forward * Scale;
                }

                currentDirection = UnityEngine.Random.Range(0, 10);
            }
        }
        catch (System.Exception)
        {
        }
    }

    private void addPlayers()
    {
        var fin = Instantiate(Finish, new Vector3(creator.transform.position.x, 0, creator.transform.position.z), Quaternion.identity);
            fin.transform.rotation = creator.transform.rotation;

        var player1 = Instantiate(Player1, new Vector3(StartPosition.x, 1, StartPosition.y), Quaternion.identity);
            player1.transform.LookAt(fin.transform.position);
            player1.transform.position -= player1.transform.right;
            player1.PlayerID = PlayerID.Player1;
            player1.spawnPoint = player1.transform.position;

        var player2 = Instantiate(Player2, new Vector3(StartPosition.x, 1, StartPosition.y), Quaternion.identity);
            player2.transform.LookAt(fin.transform.position);
            player2.transform.position += player2.transform.right * 0.1f;
            player2.PlayerID = PlayerID.Player2;
            player2.spawnPoint = player2.transform.position;
    }
}
