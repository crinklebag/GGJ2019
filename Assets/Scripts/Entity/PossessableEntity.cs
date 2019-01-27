using System.Collections;
using UnityEngine;

public class PossessableEntity : Entity
{
    public override IEnumerator EnterState()
    {
        _rb.useGravity = false;
        _rb.isKinematic = true;

        yield return MoveOffGround();

        yield return null;
    }

    public override IEnumerator ExitState()
    {
        if(_movement != null)
            yield return _movement.StartCoroutine(_movement.FinishMovement());

        _rb.useGravity = true;
        _rb.isKinematic = false;

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

    public void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "NPC")
        {
            //col.GetComponent<AIController>().MakeScared();
        }
    }
}