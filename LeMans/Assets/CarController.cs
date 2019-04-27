using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float speed = 5f;
    private float rotationSpeed = 2f;

    private float hoverHeight = 1.0f;

    private float forwardVelocity = 0f;
    private float upVelocity = 0f;
    private float dampForce = 0.005f;
    private float accelerationForce = 0.05f;
    private float brakeForce = 0.05f;
    private float maxSpeed = 0.25f;

    private bool jumpPressed;

    public GameObject CarModel;

    private KeyCode AccelerationKey;
    private KeyCode BrakeKey;
    private KeyCode LeftKey;
    private KeyCode RightKey;
    private KeyCode JumpKey;

    public PlayerID PlayerID = PlayerID.Player1;

    private Vector3 startPos;
    private Quaternion starRotation;

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
        starRotation = this.transform.rotation;
        switch (PlayerID)
        {
            case PlayerID.Player1:
                AccelerationKey = KeyCode.UpArrow;
                BrakeKey = KeyCode.DownArrow;
                LeftKey = KeyCode.LeftArrow;
                RightKey = KeyCode.RightArrow;
                JumpKey = KeyCode.Minus;
                break;

            case PlayerID.Player2:
                AccelerationKey = KeyCode.W;
                BrakeKey = KeyCode.S;
                LeftKey = KeyCode.A;
                RightKey = KeyCode.D;
                JumpKey = KeyCode.Space;
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var rigidbody = this.GetComponent<Rigidbody>();

        // Forward speed
        var target = this.transform.position;
        if (Input.GetKey(AccelerationKey) && forwardVelocity < maxSpeed && !jumpPressed)
        {
            forwardVelocity += accelerationForce;
        }
        else if (Input.GetKey(BrakeKey) && forwardVelocity > 0 && !jumpPressed)
        {
            forwardVelocity -= brakeForce;
        }
        else if (Input.GetKey(BrakeKey) && forwardVelocity <= 0 && forwardVelocity > -(maxSpeed / 2) && !jumpPressed)
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
        if (Input.GetKeyDown(JumpKey) && !jumpPressed)
        {
            jumpPressed = true;
            rigidbody.AddForce(Vector3.up * 6f, ForceMode.VelocityChange);
        }

        if (Input.GetKey(RightKey) && forwardVelocity != 0 && !jumpPressed)
        {
            rigidbody.MoveRotation(Quaternion.Euler(rigidbody.rotation.eulerAngles.x, rigidbody.rotation.eulerAngles.y + (forwardVelocity > 0 ? rotationSpeed : -rotationSpeed), rigidbody.rotation.eulerAngles.x));
        }
        if (Input.GetKey(LeftKey) && forwardVelocity != 0 && !jumpPressed)
        {
            rigidbody.MoveRotation(Quaternion.Euler(rigidbody.rotation.eulerAngles.x, rigidbody.rotation.eulerAngles.y + -(forwardVelocity > 0 ? rotationSpeed : -rotationSpeed), rigidbody.rotation.eulerAngles.x));
        }

        rigidbody.MovePosition(Vector3.MoveTowards(this.transform.position, target, 1f));

        if(this.transform.position.y < -5)
        {
            this.transform.position = startPos;
            this.transform.rotation = starRotation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        jumpPressed = false;
    }
}

public enum PlayerID
{
    Player1 = 1,
    Player2 = 2,
}