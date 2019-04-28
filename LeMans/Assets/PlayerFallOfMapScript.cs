using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallOfMapScript : MonoBehaviour
{
    public PlayerKeysScript playerKeysScript;
    public AudioClip audioClip_Fallover;
    private Rigidbody rigidBody;
    public GameObject playAuidoAndDestroy;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (this.transform.position.y < -5)
        {
            transform.position = playerKeysScript.spawnPoint;
            transform.rotation = Quaternion.identity;

            playerKeysScript.isFlaggedForReset = true;

            playSound_Fallover();
        }
    }

    private void playSound_Fallover()
    {
        // PLAY SOUND
        GameObject gObbj = Instantiate(playAuidoAndDestroy) as GameObject;
        gObbj.GetComponent<PlayAudioAndDestroy>().PlayClip(audioClip_Fallover, true, 0.3f);
    }
}
