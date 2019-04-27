using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGeneration : MonoBehaviour
{
    public GameObject Finish;
    public int Scale = 10;

    private static int sizeX = 100;
    private static int sizeY = 100;
    private bool[,] map = new bool[sizeX, sizeY];

    public Vector2 StartPosition = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        GameObject creator = new GameObject();
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
        map[(int)StartPosition.x, (int)StartPosition.y] = true;
        for (int i = 0; i < 100; i++)
        {

            try
            {
                if (map[(int)creator.transform.position.x, (int)creator.transform.position.z] == false)
                {
                    map[(int)creator.transform.position.x, (int)creator.transform.position.z] = true;
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.localScale = new Vector3(Scale, Scale, Scale);
                    cube.transform.position = new Vector3(creator.transform.position.x, -Scale / 2, creator.transform.position.z);
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

                currentDirection = Random.Range(0, 10);
            }
            catch (System.Exception)
            {
                var fin = Instantiate(Finish, new Vector3(creator.transform.position.x, 0, creator.transform.position.z),Quaternion.identity);
                fin.transform.rotation = creator.transform.rotation;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
