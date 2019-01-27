using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTeleporter : MonoBehaviour
{

    [SerializeField] Transform teleportDestinationTransform;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "NPC")
        {
            other.transform.position = teleportDestinationTransform.position;
        }
    }
}
