using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}

public class CarControlsScript_2 : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    private float MAX_SPEED = 1.5f;
    private float SPEED = 0f;
    private float LERP_SPEED = 0.2f;
    private float rotationSpeed = 1;
    private Boolean isGrounded = true;
    private Vector3 carGrvity = new Vector3(0, -50, 0);
    private Rigidbody rigidBody;
    public PlayerKeysScript keysScript;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        checkControls();
    }

    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    void OnCollisionEnter (Collision col)
    {
        isGrounded = true;//(col.gameObject.tag == GameTags.Tags.Ground.ToString());
    }

    private void checkControls()
    {
        if (isGrounded)
        {
            // JUMP
            if (Input.GetKeyDown(keysScript.JumpKey) && isGrounded)
            {
                rigidBody.AddForce(Vector3.up * 10000f, ForceMode.Impulse);
                isGrounded = false;
            }

            // FORWARD & BACKWARDS
            updateSpeeding();
        }
        
        // ROTATE LEFT & RIGHT
        if (Input.GetKey(keysScript.LeftKey))
        {
            transform.Rotate(Vector3.up, -rotationSpeed);
        }
        if (Input.GetKey(keysScript.RightKey))
        {
            transform.Rotate(Vector3.up, rotationSpeed);
        }
    }

    private void updateSpeeding()
    {
        if (isGrounded)
        {
            if (isSpeedingForward())
            {
                // SET SPEED FORWARD
                SPEED = Mathf.Lerp(SPEED, MAX_SPEED, LERP_SPEED);
                applySpeed(SPEED);
            }
            else if (isSpeedingBackwards())
            {
                // SET SPEED BACKWARDS
                SPEED = Mathf.Lerp(SPEED, -MAX_SPEED, LERP_SPEED);
                applySpeed(SPEED);
            }
        }
        else if (isSpeedingForward() == false && isSpeedingBackwards() == false)
        {
            SPEED = Mathf.Lerp(SPEED, 0, LERP_SPEED);
        }
    }

    private bool isSpeedingBackwards()
    {
        return Input.GetKey(keysScript.BrakeKey);
    }

    private bool isSpeedingForward()
    {
        return Input.GetKey(keysScript.AccelerationKey);
    }

    private void applySpeed(float speed)
    {
        if (speed < MAX_SPEED && speed > -MAX_SPEED)
        {
            rigidBody.AddForce( rigidBody.transform.forward * 200f * speed, ForceMode.Impulse);
        }

        //var target = Quaternion.LookRotation((transform.position + rigidbody.velocity) - this.transform.position);
        //this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, target, 12f);
    }

    public void FixedUpdate()
    {
        updateGravity();
        updateSteering();
    }

    private void updateGravity()
    { 
        Vector3 gravity = (carGrvity - Physics.gravity);
        rigidBody.AddForce( gravity * rigidBody.mass);
    }

    private void updateSteering()
    {
        if (isGrounded)
        {
            float motor = maxMotorTorque * Input.GetAxis("Vertical");
            float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

            foreach (AxleInfo axleInfo in axleInfos)
            {
                if (axleInfo.steering)
                {
                    axleInfo.leftWheel.steerAngle = steering;
                    axleInfo.rightWheel.steerAngle = steering;
                }
                if (axleInfo.motor)
                {
                    axleInfo.leftWheel.motorTorque = motor;
                    axleInfo.rightWheel.motorTorque = motor;
                }
                ApplyLocalPositionToVisuals(axleInfo.leftWheel);
                ApplyLocalPositionToVisuals(axleInfo.rightWheel);
            }
        }
    }
}