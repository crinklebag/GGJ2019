using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected float _moveSpeed;

    public abstract void HandleMovement(Vector3 input);
    public abstract void HandleInput();
}