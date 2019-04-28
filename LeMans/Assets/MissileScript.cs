using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    public GameObject explosion;
    public AudioClip explosionAudioClip;
    public AudioClip shootAudioClip;
    public GameObject playAuidoAndDestroy;

    public int missileLifspan = 100;

    private void Start() 
    {
        // PLAY SOUND
        GameObject gObbj = Instantiate(playAuidoAndDestroy) as GameObject;
        gObbj.GetComponent<PlayAudioAndDestroy>().PlayClip(shootAudioClip, false, 0.2f);
    }

    void Update()
    {
        this.transform.position += this.transform.forward * 0.5f;
    }

    private void FixedUpdate() 
    {
        if (missileLifspan > 0)
        {
            missileLifspan--;   
        }
        else
        {
            // DESTROY MISSILE
            Destroy(transform.gameObject);

            // PFX: Explosion
            showExplosion( transform );
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        Rigidbody rigidBody = other.gameObject.GetComponent<Rigidbody>();
        if (rigidBody)
        {
            // APPLY FORCE UPWARDS ON TARGET
            rigidBody.AddForce( rigidBody.transform.up * 10f, ForceMode.Impulse);

            // PFX: Explosion
            showExplosion( other.gameObject.transform );

            // PLAY SOUND
            GameObject gObbj = Instantiate(playAuidoAndDestroy) as GameObject;
            gObbj.GetComponent<PlayAudioAndDestroy>().PlayClip(explosionAudioClip);

            // DESTROY MISSILE
            Destroy(transform.gameObject);
        }
    }

    private void showExplosion( Transform originTransform )
    {
        // PLAY PFX
        GameObject explo = Instantiate(explosion) as GameObject;
        explo.transform.position = originTransform.position;
    }
}
