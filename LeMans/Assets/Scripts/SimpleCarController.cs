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

public class SimpleCarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    private Rigidbody rigidbody;

    private float MAX_SPEED = 1f;
    private float SPEED = 0f;
    private float LERP_SPEED = 0.2f;
    private float rotationSpeed = 1;
    private Boolean isGrounded = true;

    private Vector3 carGrvity = new Vector3(0, -50, 0);

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

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        checkControls();
    }

    void OnCollisionEnter (Collision col)
    {
        isGrounded = (col.gameObject.tag == GameTags.Tags.Ground.ToString());
    }

    private void checkControls()
    {
        // JUMP
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidbody.AddForce(Vector3.up * 10000f, ForceMode.Impulse);
            isGrounded = false;
        }

        // FORWARD & BACKWARDS
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            // SET SPEED FORWARD
            SPEED = Mathf.Lerp(SPEED, MAX_SPEED, LERP_SPEED);
            applySpeed(SPEED);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.UpArrow))
        {
            // SET SPEED BACKWARDS
            SPEED = Mathf.Lerp(SPEED, -MAX_SPEED, LERP_SPEED);
            applySpeed(SPEED);
        }
        
        // ROTATE LEFT & RIGHT
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up, -rotationSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, rotationSpeed);
        }
    }

    private void applySpeed(float speed)
    {
        if (speed < MAX_SPEED && speed > -MAX_SPEED)
        {
            rigidbody.AddForce(rigidbody.transform.forward * 200f * speed, ForceMode.Impulse);
        }
    }

    public void FixedUpdate()
    {
        updateGravity();
        updateSteering();
    }

    private void updateGravity()
    { 
        Vector3 gravity = (carGrvity - Physics.gravity);
        Debug.Log( gravity  + ", " +rigidbody.mass);
        rigidbody.AddForce( gravity * rigidbody.mass);
    }

    private void updateSteering()
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