using System;
using System.Collections;
using UnityEngine;

public class DefaultEntity : Entity
{
    [SerializeField] protected SpriteRenderer _sprite;
    [SerializeField] protected Animator _animator;

    [SerializeField] private bool canInteract = false;
    private Entity _interactEntity;

    private bool _inUse = false;

    [SerializeField] private float _fadeSpeed;
    private IEnumerator _fadeRoutine = null;


    public override IEnumerator EnterState()
    {
        _sprite.enabled = true;
        _interactEntity = null;
        _inUse = true;

        if(_fadeRoutine != null)
            StopCoroutine(_fadeRoutine);

        _fadeRoutine = FadeIn();
        yield return StartCoroutine(_fadeRoutine);
    }

    public override IEnumerator ExitState()
    {
        _interactEntity = null;
        _inUse = false;

        yield return _movement.StartCoroutine(_movement.FinishMovement());

        if (_fadeRoutine != null)
            StopCoroutine(_fadeRoutine);

        _fadeRoutine = FadeOut();
        yield return StartCoroutine(_fadeRoutine);

        _sprite.enabled = false;
    }

    private IEnumerator FadeIn()
    {
        var alpha = _sprite.color.a;
        var color = _sprite.color;
        var elapsedTime = 0.0f;
        var totalTime = Mathf.Abs(alpha - 1.0f) / _fadeSpeed;

        while (elapsedTime < totalTime)
        {
            _sprite.color = new Color(color.r, color.g, color.b,
                Mathf.Lerp(alpha, 1.0f, elapsedTime / totalTime));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _sprite.color = new Color(color.r, color.g, color.b, 1.0f);

        yield return null;
    }

    private IEnumerator FadeOut()
    {
        var alpha = _sprite.color.a;
        var color = _sprite.color;
        var elapsedTime = 0.0f;
        var totalTime = alpha / _fadeSpeed;

        while (elapsedTime < totalTime)
        {
            _sprite.color = new Color(color.r, color.g, color.b,
                Mathf.Lerp(alpha, 0.0f, elapsedTime / totalTime));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _sprite.color = new Color(color.r, color.g, color.b, 0.0f);

        yield return null;
    }

    public override Entity HandleInput()
    {
        HandleMovement(InputManager.PlayerInput.CurrentInput);
        return HandleSelect(InputManager.PlayerInput.IsSelecting);
    }
    
    protected override void HandleMovement(Vector3 input)
    {
        var x = Mathf.Abs(input.x);
        var z = Mathf.Abs(input.z);

        //Debug.Log("Handle Movement Player Entity: " + input);
        _animator.SetBool("IsMoving", x <= Mathf.Epsilon && z <= Mathf.Epsilon ? false : true);

        _movement.Move(input);
        _animator.SetFloat("Horizontal", input.x);
        _animator.SetFloat("Vertical", input.z);
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
        _interactEntity = col.transform.GetComponent<Entity>();
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.tag != "Possessable") return;

        if (!_inUse) return;

        Debug.Log("Can't Posses");
        canInteract = false;
        _interactEntity = null;
    }


    public void SwapSprites()
    {
        _sprite.flipX = InputManager.PlayerInput.CurrentInput.x > 0;
    }
}