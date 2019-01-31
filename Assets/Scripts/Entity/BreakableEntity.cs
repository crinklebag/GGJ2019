using System.Collections;
using UnityEngine;

public class BreakableEntity : Entity
{
    [SerializeField] private PropShatter _propShatter;
    [SerializeField] private SfxPlayer _sfxPlayer;

    [SerializeField] protected ParticleSystem _idleSystem;


    public void Start()
    {
        if (!_idleSystem)
            return;

        _idleSystem.Play();
    }

    public override IEnumerator EnterState()
    {
        _particleSystem.Play();
        _idleSystem.Stop();
        yield return MoveOffGround();

        yield return null;
    }

    public override IEnumerator ExitState()
    {
        _particleSystem.Stop();
        _idleSystem.Play();
        _sfxPlayer.Play();
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
        //Debug.Log("Handle Select Breakable Entity");

        if (isSelecting)
        {
            //Debug.Log("Return player");
            _propShatter.ShatterObject();
            return PlayerController.PlayerEntity;
        }
        else
        {
            return null;
        }
    }
}