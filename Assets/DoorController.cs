using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    [SerializeField] GameObject openPos;
    [SerializeField] GameObject closePos;
    [SerializeField] GameObject door;

    [SerializeField] float speed = 5;

    private float startTime = 0;
    private float journeyLength = 0;

    bool openDoor = false;
    bool closeDoor = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (openDoor)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;

            door.transform.localPosition = Vector3.Lerp(closePos.transform.localPosition, openPos.transform.localPosition, fracJourney);
        }
        else if (closeDoor) {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;

            door.transform.localPosition = Vector3.Lerp(openPos.transform.localPosition, closePos.transform.localPosition, fracJourney);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != this.gameObject) {
            //Open the Door
            startTime = Time.time;
            journeyLength = Vector3.Distance(openPos.transform.localPosition, closePos.transform.localPosition);
            openDoor = true;
            closeDoor = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != this.gameObject) {
            // Close the Door
            startTime = Time.time;
            journeyLength = Vector3.Distance(closePos.transform.localPosition, openPos.transform.localPosition);
            closeDoor = true;
            openDoor = false;
        }
    }
}
