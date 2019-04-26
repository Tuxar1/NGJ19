using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingCar : MonoBehaviour
{
    private float speed = 5f;

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
            rigidbody.AddTorque(Camera.main.transform.right * speed * 5f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rigidbody.AddTorque(-Camera.main.transform.right * speed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody.AddTorque(-Camera.main.transform.forward * speed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody.AddTorque(Camera.main.transform.forward * speed);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }
}
