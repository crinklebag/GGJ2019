using UnityEngine;

public class DefaultState : State
{
    public override void HandleMovement(Vector3 input)
    {
        Debug.Log("Player Movement: " + input);
        PlayerController.PlayerTransform.localPosition += input * _moveSpeed * Time.deltaTime;
    }

    public override void HandleInput()
    {
        Debug.Log("Handle Input Default State");
    }
}