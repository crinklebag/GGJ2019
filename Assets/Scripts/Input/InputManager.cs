using UnityEngine;

public class InputManager : MonoSingleton<InputManager>
{
    public static PlayerInput PlayerInput { get => Instance._playerInput; }
    [SerializeField] private PlayerInput _playerInput;

    public void Awake()
    {
        _playerInput = new PlayerInput();
    }

    public void Update()
    {
        _playerInput.CurrentInput = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        _playerInput.IsSelecting = Input.GetButtonDown("Select");
    }
}