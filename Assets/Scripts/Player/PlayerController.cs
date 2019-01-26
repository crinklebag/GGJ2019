using System.Collections;
using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController>
{
    public static Transform PlayerTransform { get => Instance._transform; }
    [SerializeField] protected Transform _transform;

    public static DefaultEntity PlayerEntity { get => Instance._defaultEntity; }
    [SerializeField] private DefaultEntity _defaultEntity;

    [SerializeField] private Entity _currentEntity;


    public void Awake()
    {
        _transform = GameObject.FindGameObjectWithTag("Player").transform;
        _defaultEntity = _transform.GetComponent<DefaultEntity>();
        _currentEntity = _defaultEntity;
    }

    public IEnumerator Start()
    {
        StartCoroutine(HandleInput());

        yield return null;
    }

    public IEnumerator HandleInput()
    {
        while (Application.isPlaying)
        {
            var entity = _currentEntity.HandleInput();

            if (entity != null)
            {
                yield return _currentEntity.StartCoroutine(_currentEntity.ExitState());
                _currentEntity = entity;
                yield return _currentEntity.StartCoroutine(_currentEntity.EnterState());
            }

            yield return null;
        }

        yield return null;
    }
}