using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : MonoBehaviour
{
    public PlayerID PlayerID = PlayerID.Player1;
    public List<GameObject> carControlScriptGameObjects;
    public bool isFlaggedForReset;
    [HideInInspector] public string AccelerationKey;
    [HideInInspector] public string BrakeKey;
    [HideInInspector] public string Axis;
    [HideInInspector] public string JumpKey;
    [HideInInspector] public string ShootKey;
    [HideInInspector] public Vector3 spawnPoint;
    [HideInInspector] public float CameraPos;

    void Start()
    {
        switch (PlayerID)
        {
            case PlayerID.Player1:
                AccelerationKey = "Acceleration_1";
                BrakeKey = "Brake_1";
                Axis = "Horizontal_1";
                JumpKey = "Jump_1";
                ShootKey = "Shoot_1";
                carControlScriptGameObjects[0].SetActive(true);
                CameraPos = 0.5f;
                break;

            case PlayerID.Player2:
                AccelerationKey = "Acceleration_2";
                BrakeKey = "Brake_2";
                Axis = "Horizontal_2";
                JumpKey = "Jump_2";
                ShootKey = "Shoot_2";
                carControlScriptGameObjects[0].SetActive(true);
                CameraPos = 0.0f;
                break;
        }
    }
}
