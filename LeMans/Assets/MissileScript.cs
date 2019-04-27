using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    void Update()
    {
        this.transform.position += this.transform.forward * 0.5f;
    }
}
