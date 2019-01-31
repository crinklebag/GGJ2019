using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGhost : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator _animator;

    [Header("Anim Timing")]
    [SerializeField] private float _minWait = 0.0f;
    [SerializeField] private float _maxWait = 0.0f;

    public void StartMenuRoutine()
    {
        _animator.SetTrigger("Start");
    }

    public void RevealText()
    {
        MenuManager.Instance.StartCoroutine(MenuManager.RevealStartText());
    }

    public void OnScreenTraverseFinish()
    {
        StartCoroutine(StartMovementAnim());

    }

    private IEnumerator StartMovementAnim()
    {
        var time = Random.Range(_minWait, _maxWait);
        yield return new WaitForSeconds(time);

        _animator.SetTrigger("Switch");

        yield return null;
    }
}
