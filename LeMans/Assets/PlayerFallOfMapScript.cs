using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallOfMapScript : MonoBehaviour
{
    public PlayerKeysScript playerKeysScript;
    private Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Debug.Log(this.transform.position.y);
        if (this.transform.position.y < -5)
        {
            transform.position = playerKeysScript.spawnPoint;
        }
    }
}
