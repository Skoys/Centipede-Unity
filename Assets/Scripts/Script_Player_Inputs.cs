using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Script_Player_Inputs : MonoBehaviour
{
    PlayerInput InputAction;
    public static Script_Player_Inputs instance;

    [HideInInspector] public bool actionPressed = false;
    [HideInInspector] public bool slowPressed = false;
    [HideInInspector] public Vector2 movement = Vector2.zero;

    [SerializeField] private List<float[]> rumbleList = new List<float[]>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Rumble();
    }

    void Rumble()
    {
        if (rumbleList.Count == 0) { Gamepad.current.SetMotorSpeeds(0, 0); return; }
        while (rumbleList[0][2] < Time.realtimeSinceStartup)
        {
            rumbleList.RemoveAt(0);
            if(rumbleList.Count == 0 ) { return; }
        }
        if (rumbleList.Count > 0)
        {
            Gamepad.current.SetMotorSpeeds(rumbleList[0][0], rumbleList[0][1]);
        }
    }

    public void AddRumble(Vector2 rumble, float time)
    {
        float[] list = { rumble.x, rumble.y, time + Time.realtimeSinceStartup };
        rumbleList.Add(list);
    }

    public void Movement(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void Action(InputAction.CallbackContext context)
    {
        actionPressed = context.ReadValue<float>() > 0 ? true : false;
    }

    public void Slow(InputAction.CallbackContext context)
    {
        slowPressed = context.ReadValue<float>() > 0 ? true : false;
    }
}