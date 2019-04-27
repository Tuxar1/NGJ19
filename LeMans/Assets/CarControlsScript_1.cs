using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControlsScript_1 : MonoBehaviour
{
    private float rotationSpeed = 2f;
    private float forwardVelocity = 0f;
    private float dampForce = 0.005f;
    private float accelerationForce = 0.05f;
    private float brakeForce = 0.05f;
    private float maxSpeed = 0.25f;
    private bool jumpPressed;

    public GameObject CarModel;
    private Queue positionList;
    private Queue rotationList;
    public PlayerKeysScript keysScript;
    private Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        
        positionList = new Queue();
        rotationList = new Queue();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        // Forward speed
        var target = this.transform.position;
        if (Input.GetKey(keysScript.AccelerationKey) && forwardVelocity < maxSpeed && !jumpPressed)
        {
            forwardVelocity += accelerationForce;
        }
        else if (Input.GetKey(keysScript.BrakeKey) && forwardVelocity > 0 && !jumpPressed)
        {
            forwardVelocity -= brakeForce;
        }
        else if (Input.GetKey(keysScript.BrakeKey) && forwardVelocity <= 0 && forwardVelocity > -(maxSpeed / 2) && !jumpPressed)
        {
            forwardVelocity -= brakeForce;
        }
        else if (forwardVelocity > 0 && !jumpPressed)
        {
            forwardVelocity -= dampForce;
        }
        else if (forwardVelocity < 0 && !jumpPressed)
        {
            forwardVelocity += dampForce;
        }
        else if (!jumpPressed)
        {
            forwardVelocity = 0;
        }

        target += this.transform.forward * forwardVelocity;

        // Jump
        if (Input.GetKeyDown(keysScript.JumpKey) && !jumpPressed)
        {
            jumpPressed = true;
            rigidBody.AddForce(Vector3.up * 6f, ForceMode.VelocityChange);
        }

        if (Input.GetKey(keysScript.RightKey) && !jumpPressed)
        {
            rigidBody.MoveRotation(Quaternion.Euler(rigidBody.rotation.eulerAngles.x, rigidBody.rotation.eulerAngles.y + (forwardVelocity > 0 ? rotationSpeed : -rotationSpeed), rigidBody.rotation.eulerAngles.x));
        }
        if (Input.GetKey(keysScript.LeftKey) && !jumpPressed)
        {
            rigidBody.MoveRotation(Quaternion.Euler(rigidBody.rotation.eulerAngles.x, rigidBody.rotation.eulerAngles.y + -(forwardVelocity > 0 ? rotationSpeed : -rotationSpeed), rigidBody.rotation.eulerAngles.x));
        }

        rigidBody.MovePosition(Vector3.MoveTowards(this.transform.position, target, 1f));

        if (positionList.Count > 100)
        {
            positionList.Dequeue();
        }
        positionList.Enqueue(this.transform.position);

        if (rotationList.Count > 100)
        {
            rotationList.Dequeue();
        }
        rotationList.Enqueue(this.transform.position);

        if (this.transform.position.y < -5)
        {
            positionList.Clear();
            rotationList.Clear();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        jumpPressed = false;
    }
}