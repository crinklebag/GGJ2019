using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    // Start is called before the first frame update

    // list of way points
    // enum for room type 
    // lights on when they enter 

    public enum RoomType { LIVING_ROOM = 0, KITCHEN, BEDROOM };
    
    [SerializeField] List<GameObject> waypoints = new List<GameObject>();
    [SerializeField] List<GameObject> endpoints = new List<GameObject>();
    [SerializeField] Light roomLight;
    [SerializeField] GameObject roomTrigger;
    [SerializeField] RoomType roomType;

    [SerializeField] int maxLightIntensity = 4;

    private int occupantCounter = 0;


    // Update is called once per frame
    void Update()
    {
        if (occupantCounter <= 0) {
            ToggleLight(false);
        }
        else {
            ToggleLight(true);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "NPC") {
            occupantCounter++;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "NPC") {
            occupantCounter--;
        }
    }

    void ToggleLight(bool isOn) {
        if (isOn) {
            // Turn on the light
            roomLight.intensity = maxLightIntensity;
        } else {
            // Turn off the light 
            roomLight.intensity = 0;
        }
    }

    public RoomType GetRoomType(){
        return roomType;
    }

    public List<GameObject> GetWaypoints() {
        return waypoints;
    }

    public List<GameObject> GetEndPoints ()
    {
        return endpoints;
    }
}
