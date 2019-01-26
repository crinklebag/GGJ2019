using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController>
{
    private IMovement _movement;


    public void HandleInput(Vector3 input)
    {
        _movement.Interact(input);
    }
}