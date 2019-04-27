using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileShooterScript : MonoBehaviour
{
    public GameObject Missile;
    public PlayerKeysScript keysScript;

    public void FixedUpdate() 
    {
        // Shoot
        if (Input.GetKeyDown(keysScript.ShootKey))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Instantiate(Missile, this.transform.position + this.transform.forward * 3, this.transform.rotation);
    }
}