using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public event Action<Vector2> onMoveInput;
    public event Action<bool> onJumpInput;
    public event Action<bool> onRunInput;
    public event Action<Vector2> onLookInput;
    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            onMoveInput?.Invoke(context.ReadValue<Vector2>().normalized);
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            onMoveInput?.Invoke(Vector2.zero);
        }
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onRunInput?.Invoke(true);
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            onRunInput?.Invoke(false);
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            onJumpInput?.Invoke(true);
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            onJumpInput?.Invoke(false);
        }
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        onLookInput?.Invoke(context.ReadValue<Vector2>());
    }
}
