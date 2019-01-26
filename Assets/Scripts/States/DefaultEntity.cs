using System.Collections;
using UnityEngine;

public class DefaultEntity : Entity
{
    [SerializeField] private bool canInteract = false;
    private Entity _interactEntity;


    public override IEnumerator EnterState()
    {
        // Turn on sprite
        
        yield return null;
    }

    public override IEnumerator ExitState()
    {
        // Turn off sprite

        yield return null;
    }


    public override Entity HandleInput()
    {
        HandleMovement(InputManager.PlayerInput.CurrentInput);
        return HandleSelect(InputManager.PlayerInput.IsSelecting);
    }


    protected override void HandleMovement(Vector3 input)
    {
        //Debug.Log("Handle Movement Player Entity: " + input);
        PlayerController.PlayerTransform.localPosition += input * _moveSpeed * Time.deltaTime;
    }

    protected override Entity HandleSelect(bool isSelecting)
    {
        if (isSelecting && canInteract)
        {
            canInteract = false;
            Debug.Log("Handle Select Player Entity");
            // Return selected entity's script
            return _interactEntity;
        }

        return null;
    }


    public void OnTriggerEnter(Collider col)
    {
        if (col.tag != "Possessable") return;

        Debug.Log("Can Posses");
        canInteract = true;
        _interactEntity = col.GetComponent<Entity>();
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.tag != "Possessable") return;

        Debug.Log("Can't Posses");
        canInteract = false;
        _interactEntity = null;
    }
}