using System.Collections;
using UnityEngine;

public class BreakableEntity : Entity
{
    [SerializeField] private PropShatter _propShatter;


    public override IEnumerator EnterState()
    {

        yield return null;
    }

    public override IEnumerator ExitState()
    {
        //yield return _movement.StartCoroutine(_movement.FinishMovement());
        Destroy(this.gameObject, 0.5f);

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
        Debug.Log("Handle Select Breakable Entity");

        if (isSelecting)
        {
            Debug.Log("Return player");
            _propShatter.ShatterObject();
            return PlayerController.PlayerEntity;
        }
        else
        {
            return null;
        }
    }
}