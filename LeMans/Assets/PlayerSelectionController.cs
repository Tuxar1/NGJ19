using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectionController : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Player1.SetActive(!Player1.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Player2.SetActive(!Player2.activeSelf);
        }
    }
}
