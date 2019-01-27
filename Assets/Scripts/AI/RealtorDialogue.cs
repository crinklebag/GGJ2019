using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RealtorDialogue : MonoBehaviour
{
    [Header("Dialogue Lines"), Space]
    [SerializeField] string[] enterHallwayLinesArray;
    [SerializeField] string[] livingRoomLinesArray;
    [SerializeField] string[] kitchenLinesArray;
    [SerializeField] string[] elevatorEntranceLinesArray;
    [SerializeField] string[] bedroomLinesArray;
    [SerializeField] string[] bathroomLinesArray;
    [SerializeField] string[] atticEntranceLinesArray;
    [SerializeField] string[] atticLinesArray;

    [Header("References"), Space]
    [SerializeField] GameObject speechBubbleCanvas;
    RoomController.RoomType currentRoom;


    private void Start()
    {
        speechBubbleCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<RoomController>())
        {
            currentRoom = other.GetComponent<RoomController>().GetRoomType();
            StartCoroutine(ShowDialogue());
        }
    }

    IEnumerator ShowDialogue ()
    {
        speechBubbleCanvas.gameObject.SetActive(true);
        speechBubbleCanvas.GetComponentInChildren<TextMeshProUGUI>().text = GetRoomLine(currentRoom);

        yield return new WaitForSeconds(3);

        speechBubbleCanvas.gameObject.SetActive(false);

        yield return null;
    }

    string GetRoomLine(RoomController.RoomType roomType)
    {
        string roomLine = "";

        switch (roomType)
        {
            case RoomController.RoomType.LIVING_ROOM:
                roomLine = livingRoomLinesArray[Random.Range(0, livingRoomLinesArray.Length)];
                break;
            case RoomController.RoomType.KITCHEN:
                roomLine = kitchenLinesArray[Random.Range(0, kitchenLinesArray.Length)];
                break;
            case RoomController.RoomType.BEDROOM:
                roomLine = bedroomLinesArray[Random.Range(0, bedroomLinesArray.Length)];
                break;
            case RoomController.RoomType.HALLWAY:
                roomLine = enterHallwayLinesArray[Random.Range(0, bedroomLinesArray.Length)];
                break;
            case RoomController.RoomType.BATHROOM:
                roomLine = bathroomLinesArray[Random.Range(0, bedroomLinesArray.Length)];
                break;
            case RoomController.RoomType.ATTIC:
                roomLine = atticLinesArray[Random.Range(0, bedroomLinesArray.Length)];
                break;
        }

        return roomLine;
    }
}
