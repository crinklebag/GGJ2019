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
    [SerializeField] GameObject roomLight;
    [SerializeField] GameObject roomTrigger;
    [SerializeField] RoomType roomType;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other) {
        
    }

    void TurnOnLights() {

    }

    public RoomType GetRoomType(){
        return roomType;
    }

    public List<GameObject> GetWaypoints() {
        return waypoints;
    }
}
