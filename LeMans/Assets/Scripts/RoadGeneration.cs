using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyRoads3Dv3;

public class RoadGeneration : MonoBehaviour
{
    private ERRoadNetwork roadNetwork;
    private 
    // Start is called before the first frame update
    void Start()
    {
        roadNetwork = new ERRoadNetwork();

        Vector3[] markers = { Vector3.zero};
        
        roadNetwork.AddInititialMarkers(new ERRoad(), markers);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
