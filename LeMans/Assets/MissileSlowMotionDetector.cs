using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSlowMotionDetector : MonoBehaviour
{
    public void OnTriggerEnter (Collider other)
    {
        isClose = true;
    }
 
    public void OnTriggerStay(Collider other)
    {
        isClose = true;
    }
 
    public void OnTriggerExit(Collider other)
    {
        isClose = false;
    }
    
    public GameObject Missile;
    public PlayerKeysScript keysScript;
    private const int maxShots = 2;
    private int curShots = maxShots;
    private const int reloadCD_Reset = 50;
    private int reloadCD = reloadCD_Reset;

    private const float slowMotionSpeedSlowed = 0.5f;
    private const float slowMotionSpeedNormal = 1.0f;
    private const float slowMotionSpeedChangeSpeed = 0.2f;
    private bool isClose;

    public void Update() 
    {
       /*  if (isClose)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, slowMotionSpeedSlowed, slowMotionSpeedChangeSpeed);
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }
        else
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, slowMotionSpeedNormal, slowMotionSpeedChangeSpeed);
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }*/
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
