using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueMaster : MonoBehaviour
{

    [Header("NPC Line Buckets")]
    [SerializeField] string[] idleLinesArray;
    [SerializeField] string[] scaredLinesArray;
    int idleLinesArrayIndex;
    int scaredLinesArrayIndex;

    [Header("NPC Wins Lines")]
    [SerializeField] string NPCWinsLevel1;
    [SerializeField] string NPCWinsLevel2;
    [SerializeField] string NPCWinsLevel3;

    [Header("NPC Loses Lines")]
    [SerializeField] string NPCLosesLevel1;
    [SerializeField] string NPCLosesLevel2;
    [SerializeField] string NPCLosesLevel3;


    void Start()
    {
        idleLinesArrayIndex = 0;
        scaredLinesArrayIndex = 0;

        ShuffleStringArray(idleLinesArray);
        ShuffleStringArray(scaredLinesArray);
    }

    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log(GetNextIdleLine());
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(GetNextScaredLine());
        }
    }

    public string GetNextIdleLine ()
    {

        string nextIdleLine = idleLinesArray[idleLinesArrayIndex];

        idleLinesArrayIndex++;
        idleLinesArrayIndex %= idleLinesArray.Length;

        return nextIdleLine;
    }

    public string GetNextScaredLine ()
    {
        string nextScaredLine = scaredLinesArray[scaredLinesArrayIndex];

        scaredLinesArrayIndex++;
        scaredLinesArrayIndex %= idleLinesArray.Length;

        return nextScaredLine;
    }

    private void ShuffleStringArray(string[] stringArray)
    {
        for (int t = 0; t < stringArray.Length; t++)
        {
            string tmp = stringArray[t];
            int r = Random.Range(t, stringArray.Length);
            stringArray[t] = stringArray[r];
            stringArray[r] = tmp;
        }
    }
}
