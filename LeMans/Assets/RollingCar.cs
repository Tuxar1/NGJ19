using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingCar : MonoBehaviour
{
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var rigidbody = this.GetComponent<Rigidbody>();

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rigidbody.AddTorque(Camera.main.transform.right * speed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rigidbody.velocity = Vector3.MoveTowards(rigidbody.velocity, Vector3.zero, 0.15f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody.AddTorque(-Camera.main.transform.up * speed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody.AddTorque(Camera.main.transform.up * speed);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }
}
