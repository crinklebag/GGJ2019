using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularNPCDialogue : MonoBehaviour
{

    [Header("NPC Line Buckets")]
    [SerializeField, Space] string[] idleLinesArray;
    [SerializeField, Space] string[] scaredLinesArray;
    int idleLinesArrayIndex;
    int scaredLinesArrayIndex;

    [Header("Player Wins Lines")]
    [SerializeField] string playerWinsLevel1;
    [SerializeField] string playerWinsLevel2;
    [SerializeField] string playerWinsLevel3;

    [Header("Player Loses Lines")]
    [SerializeField] string playerLosesLevel1;
    [SerializeField] string playerLosesLevel2;
    [SerializeField] string playerLosesLevel3;


    void Start()
    {
        idleLinesArrayIndex = 0;
        scaredLinesArrayIndex = 0;

        ShuffleStringArray(idleLinesArray);
        ShuffleStringArray(scaredLinesArray);
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

    public string GetNextIdleLine ()
    {

        string nextIdleLine = idleLinesArray[idleLinesArrayIndex];

        idleLinesArrayIndex++;
        idleLinesArrayIndex %= idleLinesArray.Length;
        if(idleLinesArrayIndex == 0) { ShuffleStringArray(idleLinesArray); }

        return nextIdleLine;
    }

    public string GetNextScaredLine ()
    {
        string nextScaredLine = scaredLinesArray[scaredLinesArrayIndex];

        scaredLinesArrayIndex++;
        scaredLinesArrayIndex %= idleLinesArray.Length;
        if(scaredLinesArrayIndex == 0) { ShuffleStringArray(scaredLinesArray); }

        return nextScaredLine;
    }

    public string GetPlayerWinsLine(int level)
    {
        string playerWinsLine = "";

        switch (level)
        {
            case 1:
                playerWinsLine = playerWinsLevel1;
                break;
            case 2:
                playerWinsLine = playerWinsLevel2;
                break;
            case 3:
                playerWinsLine = playerWinsLevel3;
                break;
        }

        return playerWinsLine;
    }

    public string GetPlayerLosesLine(int level)
    {
        string playerLosesLine = "";

        switch (level)
        {
            case 1:
                playerLosesLine = playerLosesLevel1;
                break;
            case 2:
                playerLosesLine = playerLosesLevel2;
                break;
            case 3:
                playerLosesLine = playerLosesLevel3;
                break;
            default:
                break;
        }

        return playerLosesLine;
    }
}
