using System;
using System.Collections;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[Serializable]
public class ShakeMovement : Movement
{
    [SerializeField] private Transform _modelTransform;
    [SerializeField] private float _hoverRange;
    [SerializeField] private float _hoverSpeed;

    [SerializeField] private float _rotationRange;
    [SerializeField] private Vector2 _rotationSpeeds;

    private Vector3 _startPosition;
    private Vector3 _startRotation;


    public void Awake()
    {
        _startPosition = _modelTransform.localPosition;
        _startRotation = _modelTransform.localEulerAngles;
    }

    public override void Move(Vector3 input)
    {
        transform.localPosition += input * _speed * Time.deltaTime;

        if (_modelTransform == null)
            return;

        // Move child object up and down
        _modelTransform.localPosition = new Vector3(
            _modelTransform.localPosition.x,
            _startPosition.y + (Mathf.Sin(Time.time * _hoverSpeed) * _hoverRange),
            _modelTransform.localPosition.z);

        _modelTransform.eulerAngles = new Vector3(
            _startRotation.x + (Mathf.Sin(Time.time * _rotationSpeeds[0]) * _rotationRange),
            _modelTransform.localEulerAngles.y,
            _startRotation.z + (Mathf.Sin(Time.time * _rotationSpeeds[1]) * _rotationRange));
    }

    public override IEnumerator FinishMovement()
    {
        if (_modelTransform == null)
            yield break;

        // TODO: move back to start position and rotation
        _modelTransform.localPosition = _startPosition;
        _modelTransform.eulerAngles = _startRotation;

        yield return null;
    }
}