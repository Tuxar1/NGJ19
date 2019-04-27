using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.transform.forward * 0.5f;
    }
}
