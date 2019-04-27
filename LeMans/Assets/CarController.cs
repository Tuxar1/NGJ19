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
    private float damp = 0.005f;
    private float acceleration = 0.05f;
    private float brake = 0.05f;
    private float maxSpeed = 0.25f;

    private bool jumpPressed;

    public GameObject CarModel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var rigidbody = this.GetComponent<Rigidbody>();

        // Forward speed
        var target = this.transform.position;
        if (Input.GetKey(KeyCode.UpArrow) && forwardVelocity < maxSpeed && !jumpPressed)
        {
            forwardVelocity += acceleration;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && forwardVelocity > 0 && !jumpPressed)
        {
            forwardVelocity -= brake;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && forwardVelocity <= 0 && forwardVelocity > -(maxSpeed / 2) && !jumpPressed)
        {
            forwardVelocity -= brake;
        }
        else if (forwardVelocity > 0 && !jumpPressed)
        {
            forwardVelocity -= damp;
        }
        else if (forwardVelocity < 0 && !jumpPressed)
        {
            forwardVelocity += damp;
        }
        else if (!jumpPressed)
        {
            forwardVelocity = 0;
        }
        target += this.transform.forward * forwardVelocity;


        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && !jumpPressed)
        {
            jumpPressed = true;
            rigidbody.AddForce(Vector3.up * 6f, ForceMode.VelocityChange);
        }

        if (Input.GetKey(KeyCode.RightArrow) && forwardVelocity != 0 && !jumpPressed)
        {
            rigidbody.MoveRotation(Quaternion.Euler(rigidbody.rotation.eulerAngles.x, rigidbody.rotation.eulerAngles.y + (forwardVelocity > 0 ? rotationSpeed : -rotationSpeed), rigidbody.rotation.eulerAngles.x));
        }
        if (Input.GetKey(KeyCode.LeftArrow) && forwardVelocity != 0 && !jumpPressed)
        {
            rigidbody.MoveRotation(Quaternion.Euler(rigidbody.rotation.eulerAngles.x, rigidbody.rotation.eulerAngles.y + -(forwardVelocity > 0 ? rotationSpeed : -rotationSpeed), rigidbody.rotation.eulerAngles.x));
        }

        rigidbody.MovePosition(Vector3.MoveTowards(this.transform.position, target, 1f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        jumpPressed = false;
    }
}
