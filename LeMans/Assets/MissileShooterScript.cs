using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileShooterScript : MonoBehaviour
{
    public GameObject Missile;
    public PlayerKeysScript keysScript;
    public ControllerInput controllerScript;
    private const int maxShots = 2;
    private int curShots = maxShots;
    private const int reloadCD_Reset = 50;
    private int reloadCD = reloadCD_Reset;

    public void Update() 
    {
        // Shoot
        if ((Input.GetKeyDown(keysScript.ShootKey) || Input.GetButtonDown(controllerScript.ShootKey)))
        {
            if (curShots > 0)
            {
                Shoot();
            }
        }
    }

    public void FixedUpdate() 
    {
        if (curShots < maxShots)
        {
            if (reloadCD > 0)
            {
                reloadCD--;
            }
            else
            {
                curShots++;
                reloadCD = reloadCD_Reset;
            }
        }
    }

    private void Shoot()
    {
        curShots--;
        Instantiate(Missile, this.transform.position + this.transform.forward * 3f, this.transform.rotation);
    }
}