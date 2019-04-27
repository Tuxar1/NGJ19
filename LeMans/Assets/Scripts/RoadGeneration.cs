using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGeneration : MonoBehaviour
{
    public GameObject CurvePrefab;
    public GameObject LinePrefab;

    public int Scale = 10;

    private static int sizeX = 100;
    private static int sizeY = 100;
    private bool[,] map = new bool[sizeX, sizeY];

    public Vector2 StartPosition = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                map[x, y] = false;
            }
        }
        StartPosition = new Vector2(sizeX / 2, sizeX / 2);
        int currentDirection = 1;
        int lastDirection = currentDirection;
        Vector2 currentPostion = StartPosition;
        Vector2 nextPosition = Vector2.zero;
        map[(int)StartPosition.x, (int)StartPosition.y] = true;
        for (int i = 0; i < 100; i++)
        {
            switch (currentDirection)
            {
                //Up
                case 1:
                    nextPosition = new Vector2((int)currentPostion.x, (int)currentPostion.y + Scale);
                    break;
                //Left
                case 2:
                    nextPosition = new Vector2((int)currentPostion.x- Scale, (int)currentPostion.y);
                    break;
                //Down
                case 3:
                    nextPosition = new Vector2((int)currentPostion.x, (int)currentPostion.y - Scale);
                    break;
                //Right
                case 4:
                    nextPosition = new Vector2((int)currentPostion.x+ Scale, (int)currentPostion.y);
                    break;
                default:
                    break;
            }

            lastDirection = currentDirection;

            currentDirection = Random.Range(1,5);

            try
            {
                if (map[(int)nextPosition.x, (int)nextPosition.y] == false)
                {
                    map[(int)nextPosition.x, (int)nextPosition.y] = true;
                    currentPostion = nextPosition;
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.localScale = new Vector3(Scale, Scale, Scale);
                    cube.transform.position = new Vector3(currentPostion.x, 0, currentPostion.y);
                }
            }
            catch (System.Exception)
            {
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
