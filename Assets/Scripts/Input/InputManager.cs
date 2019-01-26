using UnityEngine;

public class InputManager : MonoSingleton<InputManager>
{
    public static Vector3 CurrentInput { get => Instance._input; set => Instance._input = value; }
    private Vector3 _input;

    public void Update()
    {
        _input = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
    }
}