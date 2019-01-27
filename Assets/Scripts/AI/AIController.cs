using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    // AI States
    bool isScared = false;

    // Movement
    [SerializeField] float moveSpeed;
    List<GameObject> waypoints = new List<GameObject>();
    GameObject currentWaypoint;
    int currentWaypointIndex = 0;
    bool nearWaypoint = false;

    void Update()
    {
        if (isScared)
        {
            // Fade away???
        }
        else
        {
            MoveCharacter();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RoomController>())
        {
            GetNewRoomWaypoints(other);
        }
    }

    void GetNewRoomWaypoints (Collider other)
    {
        waypoints.Clear();
        waypoints = other.GetComponent<RoomController>().GetWaypoints();
        ShuffleWaypoints();
        waypoints.AddRange(other.GetComponent<RoomController>().GetEndPoints());
        currentWaypointIndex = 0;
        currentWaypoint = waypoints[currentWaypointIndex];
    }

    void ShuffleWaypoints ()
    {
        for (int t = 0; t < waypoints.Count; t++)
        {
            GameObject tmp = waypoints[t];
            int r = Random.Range(t, waypoints.Count);
            waypoints[t] = waypoints[r];
            waypoints[r] = tmp;
        }
    }

    void MoveCharacter ()
    {
        float distance = Vector3.Distance(this.transform.position, currentWaypoint.transform.position);
        Vector3 moveDirection = (currentWaypoint.transform.position - this.transform.position).normalized;

        if(!nearWaypoint)
        {
            if (distance > 0.5f)
            {
                this.GetComponent<Rigidbody>().MovePosition(this.transform.position + moveSpeed * moveDirection);
            }
            else
            {
                nearWaypoint = true;
                StartCoroutine(WaitAtWayPoint());
            }
        }
    }

    IEnumerator WaitAtWayPoint ()
    {
        yield return new WaitForSeconds(Random.Range(2f, 4f));
        currentWaypointIndex++;
        currentWaypoint = waypoints[currentWaypointIndex];
        nearWaypoint = false;
    }

    // Go through the list and walk to each point
    // Wait at point for n seconds then continue 
    // If the AI get to the end of the list they go into the next room
    // Maybe make a "doorway" series of connectors to get the AI into the next room?
    // Maybe make a house controller that has refence to all the room connections that the AI can look to 
}
