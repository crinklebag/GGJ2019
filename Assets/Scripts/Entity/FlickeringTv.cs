using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringTv : Entity
{
    [SerializeField] private List<GameObject> _screens;
    private IEnumerator flickerRountine = null;

    [SerializeField] private float _minScreenTime;
    [SerializeField] private float _maxScreenTime;


    public override IEnumerator EnterState()
    {

        yield return null;
    }

    public override IEnumerator ExitState()
    {
        if (_movement != null)
            yield return _movement.StartCoroutine(_movement.FinishMovement());

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