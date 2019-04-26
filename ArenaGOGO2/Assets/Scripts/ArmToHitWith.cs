﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmToHitWith : MonoBehaviour
{
    public Rigidbody2D ArmRigidBody;
    public Vector2 SwingForce = new Vector2(100, 0);
    public float HitDisplacement = 0.2f;
    public Transform Parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.ToLower() == "player")
        {
            // Add force to the player and damage
        }
    }

    public void DoSwing(Vector3 direction)
    {
        if (direction.x != 0)
        {
            transform.position = new Vector3(gameObject.transform.position.x + direction.x * HitDisplacement, gameObject.transform.position.y, gameObject.transform.position.z);
        }
    }

    public void EndSwing()
    {
        transform.localPosition = new Vector3(0, 0, 0);
    }
}