using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController>
{
    public static Transform PlayerTransform { get => Instance._transform; }
    [SerializeField] protected Transform _transform;

    [SerializeField] private DefaultState _defaultState;
    private State _currentState;


    public void Awake()
    {
        _currentState = GetComponent<DefaultState>();
    }

    public void Update()
    {
        HandleMovement(InputManager.CurrentInput);
    }

    public void HandleMovement(Vector3 input)
    {
        _currentState.HandleMovement(input);
    }
}