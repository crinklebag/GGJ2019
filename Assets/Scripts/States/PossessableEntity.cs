using System.Collections;
using UnityEngine;

public class PossessableEntity : Entity
{
    public override IEnumerator EnterState()
    {
        
        yield return null;
    }

    public override IEnumerator ExitState()
    {

        yield return null;
    }

    public override Entity HandleInput()
    {
        HandleMovement(InputManager.PlayerInput.CurrentInput);
        return HandleSelect(InputManager.PlayerInput.IsSelecting);
    }

    protected override void HandleMovement(Vector3 input)
    {
        //Debug.Log("Handle Movement Possessable Entity:: " + input);
        transform.localPosition += input * _moveSpeed * Time.deltaTime;
    }

    protected override Entity HandleSelect(bool isSelecting)
    {
        //Debug.Log("Handle Select Possessable Entity");

        return isSelecting ? PlayerController.PlayerEntity : null;
    }
}