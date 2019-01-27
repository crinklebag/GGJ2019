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

    Animator animator;
    AISpawner aiSpawner;

    private void Start()
    {
        animator = this.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (isScared)
        {
            animator.SetBool("isMoving", false);
            RemoveFromMasterList();
            Destroy(this.gameObject);
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
        if (this.GetComponent<CapsuleCollider>())
        {
            RoomController roomController = other.GetComponent<RoomController>();

            waypoints.Clear();
            waypoints = roomController.GetWaypoints();
            ShuffleWaypoints();
            if(roomController.GetStartpoints().Count > 0) { waypoints.InsertRange(0, roomController.GetStartpoints()); }
            waypoints.AddRange(roomController.GetEndPoints());
            currentWaypointIndex = 0;
            currentWaypoint = waypoints[currentWaypointIndex];
        }
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
        Vector3 destinationPosition = new Vector3(currentWaypoint.transform.position.x, this.transform.position.y, currentWaypoint.transform.position.z);
        float distance = Vector3.Distance(this.transform.position, destinationPosition);
        Vector3 moveDirection = (destinationPosition - this.transform.position).normalized;

        if(!nearWaypoint)
        {
            if (distance > 0.5f)
            {
                animator.SetBool("isMoving", true);
                this.GetComponent<Rigidbody>().MovePosition(this.transform.position + moveSpeed * moveDirection);

                // Flip sprite
                if(moveDirection.x < 0 && this.transform.localScale.x < 0)
                {
                    this.transform.localScale = Vector3.one;
                }
                else if(moveDirection.x > 0 && this.transform.localScale.x > 0)
                {
                    this.transform.localScale = new Vector3(-1, 1, 1);
                }
            }
            else
            {
                nearWaypoint = true;
                animator.SetBool("isMoving", false);
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

    void RemoveFromMasterList ()
    {
        aiSpawner.RemoveNPC(this.gameObject);
    }

    public void MakeScared ()
    {
        isScared = true;
    }

    public void SetSpawnerReference (AISpawner spawner)
    {
        aiSpawner = spawner;
    }

    public void AddStartPoint(GameObject startPoint)
    {
        waypoints.Insert(0, startPoint);
        Debug.Log("Start point added");
        Debug.Break();
    }

    // Go through the list and walk to each point
    // Wait at point for n seconds then continue 
    // If the AI get to the end of the list they go into the next room
    // Maybe make a "doorway" series of connectors to get the AI into the next room?
    // Maybe make a house controller that has refence to all the room connections that the AI can look to 
}
