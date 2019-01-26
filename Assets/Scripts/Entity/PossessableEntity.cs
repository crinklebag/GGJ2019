using System.Collections;
using UnityEngine;

public class PossessableEntity : Entity
{
    [SerializeField] protected MeshRenderer _mesh;

    public override IEnumerator EnterState()
    {
        
        yield return null;
    }

    public override IEnumerator ExitState()
    {
        yield return _movement.StartCoroutine(_movement.FinishMovement());

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
        _movement.Move(input);
    }

    protected override Entity HandleSelect(bool isSelecting)
    {
        //Debug.Log("Handle Select Possessable Entity");

        return isSelecting ? PlayerController.PlayerEntity : null;
    }
}