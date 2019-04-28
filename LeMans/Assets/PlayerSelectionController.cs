using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class PlayerSelectionController : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;

    public GameObject Player1UI;
    public GameObject Player2UI;
    public GameObject Player3UI;
    public GameObject Player4UI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump_1"))
        {
            Player1.transform.position = new Vector3(Player1.transform.position.x, 0, Player1.transform.position.z);
            Player1.SetActive(!Player1.activeSelf);
            GameManager.Instance.Player2Active = Player2.activeSelf;
            SoundManager.Instance.PlayJump();
        }

        if (Input.GetButtonDown("Jump_2"))
        {
            Player2.transform.position = new Vector3(Player2.transform.position.x, 0, Player2.transform.position.z);
            Player2.SetActive(!Player2.activeSelf);
            GameManager.Instance.Player2Active = Player2.activeSelf;
        }
    }
}
