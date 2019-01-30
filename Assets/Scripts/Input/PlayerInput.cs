using System;
using UnityEngine;

[Serializable]
public class PlayerInput
{
    public PlayerInput()
    {
        CurrentInput = Vector3.zero;
        IsSelecting = false;
        FloatValue = 0.0f;
    }

    public Vector3 CurrentInput;
    public bool IsSelecting;
    public float FloatValue;
}