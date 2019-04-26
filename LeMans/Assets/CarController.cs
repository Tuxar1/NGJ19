using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float speed = 5f;
    private float rotationSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var target = this.transform.position;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            target += this.transform.forward;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, rotationSpeed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up, -rotationSpeed);
        }

        this.transform.position = Vector3.MoveTowards(this.transform.position, target, Time.deltaTime * speed);
    }
}
