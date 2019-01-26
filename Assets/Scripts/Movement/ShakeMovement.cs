using System;
using UnityEngine;

[Serializable]
public class ShakeMovement : Movement
{
    [SerializeField] private float _shakeDistance;

    public override void Move(Vector3 input)
    {

    }
}