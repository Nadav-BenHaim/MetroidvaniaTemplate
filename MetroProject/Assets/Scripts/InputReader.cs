using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;

[CreateAssetMenu(fileName = "Metro Input Reader", menuName = "Input System/Metro Input Reader")]
public class InputReader : ScriptableObject, InputSystem_Actions.IMetroPlayerActions
{
    public event Action<Vector2> MoveEvent = delegate { };
    public event Action JumpEvent = delegate { };
    public event Action AttackEvent = delegate { };
    public event Action InteractEvent = delegate { };

    private InputSystem_Actions input;

    private void OnEnable()
    {
        if (input == null)
        {
            input = new InputSystem_Actions();
            input.MetroPlayer.SetCallbacks(this);
        }
        input.MetroPlayer.Enable();
    }
    private void OnDisable()
    {
        input?.MetroPlayer.Disable();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            AttackEvent();//.Invoke()
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            InteractEvent();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            JumpEvent();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent(context.ReadValue<Vector2>());
    }
}
