using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float speed = 5f;
    private float rotationSpeed = 1f;

    public GameObject FrontLeft;
    public GameObject FrontRight;
    public GameObject BackLeft;
    public GameObject BackRight;
    private float hoverHeight = 1.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        float terrainHeight = Terrain.activeTerrain.SampleHeight(pos);
        transform.position = new Vector3(pos.x,
                                         terrainHeight + hoverHeight,
                                         pos.z);

        // Rotate to align with terrain
        RaycastHit hit;
        Physics.Raycast(this.transform.position, Vector3.down, out hit);
        transform.up -= (transform.up - hit.normal) * 0.1f;

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

        this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
    }
}
