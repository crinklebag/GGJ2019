using System.Collections;
using UnityEngine;

public class DefaultEntity : Entity
{
    [SerializeField] private bool canInteract = false;
    private Entity _interactEntity;

    private bool _inUse = false;

    public override IEnumerator EnterState()
    {
        _sprite.enabled = true;
        _interactEntity = null;
        _inUse = true;

        yield return null;
    }

    public override IEnumerator ExitState()
    {
        _sprite.enabled = false;
        _interactEntity = null;
        _inUse = false;

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
        if (!isSelecting || !canInteract || _interactEntity == null) return null;

        Debug.Log("Handle Select Player Entity");
        canInteract = false;
        return _interactEntity;

    }


    public void OnTriggerEnter(Collider col)
    {
        if (col.tag != "Possessable") return;

        if (!_inUse) return;

        Debug.Log("Can Posses");
        canInteract = true;
        _interactEntity = col.GetComponent<Entity>();
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.tag != "Possessable") return;

        if (!_inUse) return;

        Debug.Log("Can't Posses");
        canInteract = false;
        _interactEntity = null;
    }
}