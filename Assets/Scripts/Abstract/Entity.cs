using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] protected Movement _movement;

    public abstract IEnumerator EnterState();
    public abstract IEnumerator ExitState();


    protected abstract void HandleMovement(Vector3 input);
    protected abstract Entity HandleSelect(bool isSelecting);


    public abstract Entity HandleInput();
}