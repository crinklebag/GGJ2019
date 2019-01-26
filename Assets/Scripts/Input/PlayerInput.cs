using System;
using UnityEngine;

[Serializable]
public class PlayerInput
{
    public PlayerInput()
    {
        CurrentInput = Vector3.zero;
        IsSelecting = false;
    }

    public Vector3 CurrentInput;
    public bool IsSelecting;
}