using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarControlsScript_1 : MonoBehaviour
{
    private float rotationSpeed = 2f;
    private float forwardVelocity = 0f;
    private float dampForce = 0.005f;
    private float accelerationForce = 0.05f;
    private float brakeForce = 0.05f;
    private float maxSpeed = 0.25f;
    private bool jumpPressed;
    private bool glidePressed;

    public GameObject CarModel;
    public PlayerKeysScript keysScript;
    public ControllerInput controllerScript;
    private Rigidbody rigidBody;
    public GameObject playAuidoAndDestroy;
    public AudioClip sfxBoing;
    private float countDown = 5f;


    private Text text;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        text = GameObject.Find("Text").GetComponent<Text>();
        Camera.main.rect = new Rect(Camera.main.rect.x, keysScript.CameraPos, Camera.main.rect.width, Camera.main.rect.height);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (countDown > 1)
        {
            countDown -= Time.deltaTime;
            int seconds = (int)countDown % 60;
            text.text = seconds.ToString();
            return;
        }
        text.text = "";

        // Forward speed
        var target = this.transform.position;
        if ((Input.GetKey(keysScript.AccelerationKey) || Input.GetButton(controllerScript.AccelerationKey)) && forwardVelocity < maxSpeed && !jumpPressed)
        {
            forwardVelocity += accelerationForce;
        }
        else if ((Input.GetKey(keysScript.BrakeKey) || Input.GetButton(controllerScript.BrakeKey)) && forwardVelocity > 0 && !jumpPressed)
        {
            forwardVelocity -= brakeForce;
        }
        else if ((Input.GetKey(keysScript.BrakeKey) || Input.GetButton(controllerScript.BrakeKey)) && forwardVelocity <= 0 && forwardVelocity > -(maxSpeed / 2) && !jumpPressed)
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

        if ((Input.GetKey(keysScript.RightKey) || Input.GetAxis(controllerScript.Axis) > 0.3) && !jumpPressed)
        {
            rigidBody.MoveRotation(Quaternion.Euler(rigidBody.rotation.eulerAngles.x, rigidBody.rotation.eulerAngles.y + (forwardVelocity < -0.05 ? -rotationSpeed : rotationSpeed), rigidBody.rotation.eulerAngles.x));
        }
        else if ((Input.GetKey(keysScript.LeftKey) || Input.GetAxis(controllerScript.Axis) < -0.3) && !jumpPressed)
        {
            rigidBody.MoveRotation(Quaternion.Euler(rigidBody.rotation.eulerAngles.x, rigidBody.rotation.eulerAngles.y + -(forwardVelocity < -0.05 ? -rotationSpeed : rotationSpeed), rigidBody.rotation.eulerAngles.x));
        }

        rigidBody.MovePosition(Vector3.MoveTowards(this.transform.position, target, 1f));
    }

    public void Update()
    {
        if (keysScript.isFlaggedForReset)
        {
            keysScript.isFlaggedForReset = false;

            rigidBody.velocity = Vector3.zero;
            forwardVelocity = 0f;
        }

        // Jump
        if ((Input.GetKeyDown(keysScript.JumpKey) || Input.GetButtonDown(controllerScript.JumpKey)) && !jumpPressed)
        {
            jumpPressed = true;
            rigidBody.AddForce(Vector3.up * 6f, ForceMode.VelocityChange);

            // PLAY SOUND
            GameObject gObbj = Instantiate(playAuidoAndDestroy) as GameObject;
            gObbj.GetComponent<PlayAudioAndDestroy>().PlayClip(sfxBoing, true, 1);
        }
        //Glide
        else if ((Input.GetKey(keysScript.JumpKey) || Input.GetButtonDown(controllerScript.JumpKey)) && jumpPressed)
        {
            rigidBody.AddForce(Vector3.up * 3f, ForceMode.Acceleration);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        jumpPressed = false;
    }
}
