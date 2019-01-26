using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class PlayerMovement : Movement
{
    public override void Move(Vector3 input)
    {
        PlayerController.PlayerTransform.localPosition += input * _speed * Time.deltaTime;
    }

    public override IEnumerator FinishMovement()
    {


        yield return null;
    }
}