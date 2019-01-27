using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringTv : Entity
{
    [SerializeField] protected ParticleSystem _idleSystem;

    [SerializeField] private List<GameObject> _screens;
    private IEnumerator flickerRountine = null;

    [SerializeField] private float _minScreenTime;
    [SerializeField] private float _maxScreenTime;


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
        yield return null;
    }

    public override IEnumerator ExitState()
    {
        _particleSystem.Stop();
        _idleSystem.Play();

        yield return null;
    }

    public override Entity HandleInput()
    {
        HandleMovement(InputManager.PlayerInput.CurrentInput);
        return HandleSelect(InputManager.PlayerInput.IsSelecting);
    }

    protected override Entity HandleSelect(bool isSelecting)
    {
        //Debug.Log("Handle Select Possessable Entity");

        return isSelecting ? PlayerController.PlayerEntity : null;
    }

    protected override void HandleMovement(Vector3 input)
    {
        if (flickerRountine != null)
            return;

        flickerRountine = Flicker(input);
        StartCoroutine(flickerRountine);
    }

    public IEnumerator Flicker(Vector3 input)
    {
        yield return new WaitForSeconds(0.1f);

        Debug.Log("Start Flicker");

        var x = Mathf.Abs(input.x);
        var z = Mathf.Abs(input.z);

        if (x <= 0 && z <= 0)
        {
            flickerRountine = null;
            yield break;
        }

        // choose random screen to turn on
        var index = Random.Range(0, _screens.Count);

        _screens[index].SetActive(true);

        yield return new WaitForSeconds(Random.Range(_minScreenTime, _maxScreenTime));

        _screens[index].SetActive(false);

        flickerRountine = null;

        yield return null;
    }
}