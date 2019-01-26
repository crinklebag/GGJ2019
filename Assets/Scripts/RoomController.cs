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
    [SerializeField] Light roomLight;
    [SerializeField] GameObject roomTrigger;
    [SerializeField] RoomType roomType;

    int maxLightIntensity = 2;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)){
            ToggleLight();
        }
    }

    private void OnTriggerEnter(Collider other) {
        
    }

    void ToggleLight() {
        if (roomLight.intensity == 0) {
            // Turn on the light
            roomLight.intensity = 2;
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
}
