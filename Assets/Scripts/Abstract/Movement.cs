using System.Collections;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] protected float _speed;

    public abstract void Move(Vector3 input);

    public abstract IEnumerator FinishMovement();

}