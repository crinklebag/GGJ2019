using System;
using UnityEngine;

[Serializable]
public class PlayerMovement : Movement
{
    public override void Move(Vector3 input)
    {
        PlayerController.PlayerTransform.localPosition += input * _speed * Time.deltaTime;
    }
}