using UnityEngine;
using UnityEngine.InputSystem;

public class Script_Player_Inputs : MonoBehaviour
{
    PlayerInput InputAction;

    [HideInInspector] public bool actionPressed = false;
    [HideInInspector] public Vector2 movement = Vector2.zero;

    public void Movement(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void Action(InputAction.CallbackContext context)
    {
        actionPressed = context.ReadValue<float>() > 0 ? true : false;
    }
}