using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerID
{
    Player1 = 1,
    Player2 = 2,
}

public class PlayerKeysScript : MonoBehaviour
{
    public PlayerID PlayerID = PlayerID.Player1;
    public List<GameObject> carControlScriptGameObjects;
    [HideInInspector] public KeyCode AccelerationKey;
    [HideInInspector] public KeyCode BrakeKey;
    [HideInInspector] public KeyCode LeftKey;
    [HideInInspector] public KeyCode RightKey;
    [HideInInspector] public KeyCode JumpKey;
    [HideInInspector] public KeyCode ShootKey;

    void Start()
    {
        switch (PlayerID)
        {
            case PlayerID.Player1:
                AccelerationKey = KeyCode.UpArrow;
                BrakeKey = KeyCode.DownArrow;
                LeftKey = KeyCode.LeftArrow;
                RightKey = KeyCode.RightArrow;
                JumpKey = KeyCode.Minus;
                ShootKey = KeyCode.Period;
                carControlScriptGameObjects[0].SetActive(true);
                break;

            case PlayerID.Player2:
                AccelerationKey = KeyCode.W;
                BrakeKey = KeyCode.S;
                LeftKey = KeyCode.A;
                RightKey = KeyCode.D;
                JumpKey = KeyCode.F;
                ShootKey = KeyCode.G;
                carControlScriptGameObjects[1].SetActive(true);
                break;
        }
    }
}