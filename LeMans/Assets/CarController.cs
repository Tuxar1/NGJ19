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
    private KeyCode ShootKey;

    public PlayerID PlayerID = PlayerID.Player1;
    public GameObject Missile;

    private Queue positionList;
    private Queue rotationList;

    // Start is called before the first frame update
    void Start()
    {
        positionList = new Queue();
        rotationList = new Queue();
        switch (PlayerID)
        {
            case PlayerID.Player1:
                AccelerationKey = KeyCode.UpArrow;
                BrakeKey = KeyCode.DownArrow;
                LeftKey = KeyCode.LeftArrow;
                RightKey = KeyCode.RightArrow;
                JumpKey = KeyCode.Minus;
                ShootKey = KeyCode.Period;
                break;

            case PlayerID.Player2:
                AccelerationKey = KeyCode.W;
                BrakeKey = KeyCode.S;
                LeftKey = KeyCode.A;
                RightKey = KeyCode.D;
                JumpKey = KeyCode.F;
                ShootKey = KeyCode.G;
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

        // Jump
        if (Input.GetKeyDown(ShootKey))
        {
            Shoot();
        }

        if (Input.GetKey(RightKey) && !jumpPressed)
        {
            rigidbody.MoveRotation(Quaternion.Euler(rigidbody.rotation.eulerAngles.x, rigidbody.rotation.eulerAngles.y + (forwardVelocity > 0 ? rotationSpeed : -rotationSpeed), rigidbody.rotation.eulerAngles.x));
        }
        if (Input.GetKey(LeftKey) && !jumpPressed)
        {
            rigidbody.MoveRotation(Quaternion.Euler(rigidbody.rotation.eulerAngles.x, rigidbody.rotation.eulerAngles.y + -(forwardVelocity > 0 ? rotationSpeed : -rotationSpeed), rigidbody.rotation.eulerAngles.x));
        }

        rigidbody.MovePosition(Vector3.MoveTowards(this.transform.position, target, 1f));

        if (positionList.Count > 100)
        {
            positionList.Dequeue();
        }
        positionList.Enqueue(this.transform.position);

        if (rotationList.Count > 100)
        {
            rotationList.Dequeue();
        }

        RaycastHit hit;
        if(Physics.Raycast(new Ray(this.transform.position + Vector3.down, Vector3.down), out hit))
        {
            if(hit.collider.gameObject.tag == "Road")
            {
                rotationList.Enqueue(this.transform.position);
            }
        }

        if (this.transform.position.y < -1)
        {
            jumpPressed = true;
            rigidbody.position = (Vector3)positionList.Dequeue();
            rigidbody.position += Vector3.up*5;
            positionList.Clear();
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.rotation = Quaternion.identity;
            rotationList.Clear();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        jumpPressed = false;
    }

    private void Shoot()
    {
        Instantiate(Missile, this.transform.position + this.transform.forward * 3, this.transform.rotation);
    }
}

public enum PlayerID
{
    Player1 = 1,
    Player2 = 2,
}