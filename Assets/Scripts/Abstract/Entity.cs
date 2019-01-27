using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] protected Movement _movement;
    [SerializeField] protected Rigidbody _rb;

    [Header("Rand Values")]
    [SerializeField] protected float _groundOffset;
    [SerializeField] protected float _pickupSpeed;


    public abstract IEnumerator EnterState();
    public abstract IEnumerator ExitState();


    protected abstract void HandleMovement(Vector3 input);
    protected abstract Entity HandleSelect(bool isSelecting);


    public abstract Entity HandleInput();

    protected IEnumerator MoveOffGround()
    {
        var start = transform.position;
        var target = new Vector3(transform.position.x, transform.position.y + _groundOffset, transform.position.z);

        var totalTime = Vector3.Distance(start, target) / _pickupSpeed;
        var elapsedTime = 0.0f;

        while (elapsedTime < totalTime)
        {
            transform.position = Vector3.Lerp(start, target, elapsedTime / totalTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = target;
        yield return null;
    }
}