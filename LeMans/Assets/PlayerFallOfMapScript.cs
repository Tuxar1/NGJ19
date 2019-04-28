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
    private Queue positionList;
    private Queue rotationList;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        positionList = new Queue();
        rotationList = new Queue();
    }

    void Update()
    {
        RaycastHit[] hits = Physics.RaycastAll(new Ray(this.transform.position, Vector3.down));

        if (positionList.Count > 20)
        {
            positionList.Dequeue();
        }

        if (rotationList.Count > 20)
        {
            rotationList.Dequeue();
        }

        foreach (var hit in hits)
        {
            if(hit.collider.tag == "Road")
            {
                positionList.Enqueue(this.transform.position);
                rotationList.Enqueue(this.transform.rotation);
            }
        }

        if (this.transform.position.y < -5)
        {
            transform.position = (Vector3) positionList.Dequeue();
            transform.rotation = (Quaternion) rotationList.Dequeue();

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
