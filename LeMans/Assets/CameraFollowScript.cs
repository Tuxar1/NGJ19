using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public Transform targetPosition;
    public Transform targetLook;

    public float smoothSpeed = 0.125f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 smoothedPosition = Vector3.Slerp(transform.position, targetPosition.position, smoothSpeed);
        Vector3 smoothedYPosition = Vector3.Slerp(transform.position, targetPosition.position, smoothSpeed / 2f);
        transform.position = new Vector3(smoothedPosition.x, smoothedYPosition.y, smoothedPosition.z);

        transform.LookAt(targetLook);
    }
}
