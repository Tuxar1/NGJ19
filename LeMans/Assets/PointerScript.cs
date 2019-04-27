using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerScript : MonoBehaviour
{
    public Transform Goal;

    // Start is called before the first frame update
    void Start()
    {
        Goal = FindObjectOfType<GoalScript>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(Goal);
    }
}
