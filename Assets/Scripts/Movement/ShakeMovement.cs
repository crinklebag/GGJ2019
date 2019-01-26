using System;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

[Serializable]
public class ShakeMovement : Movement
{
    [SerializeField] private Transform _modelTransform;
    [SerializeField] private float _hoverRange;
    [SerializeField] private float _hoverSpeed;
    
    private Vector3 _startPosition;



    public void Awake()
    {
        _startPosition = _modelTransform.localPosition;
    }

    public override void Move(Vector3 input)
    {
        transform.localPosition += input * _speed * Time.deltaTime;

        // Move child object up and down
        _modelTransform.localPosition = new Vector3(
            _modelTransform.localPosition.x,
            _startPosition.x + (Mathf.Sin(Time.time * _hoverSpeed) * _hoverRange),
            _modelTransform.localPosition.z);
    }
}