using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController>
{
    public static Transform PlayerTransform { get => Instance._transform; }
    [SerializeField] protected Transform _transform;

    [SerializeField] private DefaultEntity _defaultEntity;
    private Entity _currentEntity;


    public void Awake()
    {
        _currentEntity = GetComponent<DefaultEntity>();
    }

    public void Update()
    {
        // TODO: Handle button press
        HandleMovement(InputManager.CurrentInput);
    }

    public void HandleMovement(Vector3 input)
    {
        _currentEntity.HandleMovement(input);
    }
}