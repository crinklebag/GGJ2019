using UnityEngine;

public class InputManager : MonoSingleton<InputManager>
{
    private Vector2 _input;

    public void Update()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Debug.Log("Input: " + _input);
    }
}