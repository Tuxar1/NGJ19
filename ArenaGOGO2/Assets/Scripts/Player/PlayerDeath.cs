﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public ParticleSystem ps;

    public Action PlayerDeathActions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDie()
    {
        ps.gameObject.transform.SetParent(null, true);
        ps.Play();
        PlayerDeathActions();
        gameObject.SetActive(false);
		GameController.instance.PlayerDied(gameObject);
    }

    public void ReclaimPSAndResetIt()
    {
        ps.Stop();
        ps.transform.SetParent(gameObject.transform);
        ps.transform.localPosition = new Vector3(0, 0, 0);
    }
}
