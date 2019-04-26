using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public GameObject Follow;
    public Rigidbody Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = Follow.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Follow.transform.position;
        var target = Quaternion.LookRotation((Follow.transform.position + Rigidbody.velocity) - this.transform.position);
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, target, 12f);
    }
}
