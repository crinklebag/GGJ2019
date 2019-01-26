using UnityEngine;

public class DefaultEntity : Entity
{
    public override void HandleMovement(Vector3 input)
    {
        Debug.Log("Handle Movement Player Entity: " + input);
        PlayerController.PlayerTransform.localPosition += input * _moveSpeed * Time.deltaTime;
    }

    public override void HandleInput()
    {
        Debug.Log("Handle Input Player Entity");
    }
}

public class PossessableEntity : Entity
{
    public override void HandleMovement(Vector3 input)
    {
        Debug.Log("Handle Movement Possessable Entity:: " + input);
        PlayerController.PlayerTransform.localPosition += input * _moveSpeed * Time.deltaTime;
    }

    public override void HandleInput()
    {
        Debug.Log("Handle Input Possessable Entity");
    }
}