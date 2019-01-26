using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    List<GameObject> waypoints = new List<GameObject>(); 

    // Start is called before the first frame update
    void Start()
    {
        // Move towards house entrance
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        // When the AI hits a new trigger region clear the old list and make a new one 
        // When the list is filled start the AI walking through the room
    }

    // Go through the list and walk to each point
    // Wait at point for n seconds then continue 
    // If the AI get to the end of the list they go into the next room
    // Maybe make a "doorway" series of connectors to get the AI into the next room?
    //Maybe make a house controller that has refence to all the room connections that the AI can look to 
}
