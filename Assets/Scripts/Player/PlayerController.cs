using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    public event Action<Vector2> onMoveInput;
    public event Action<bool> onJumpInput;
    public event Action<bool> onRunInput;
    public event Action<Vector2> onLookInput;
    public event Action onBuildInput;
    public event Action onInteractInput;
    public event Action onAttackInput;
    public event Action onInventoryInput;

    private bool isInventoryOpen = false; // 인벤토리 상태를 저장하는 변수
    private Interaction interaction;

    private void Start()
    {
        SetCursor(false);
        interaction = GetComponent<Interaction>();
    }

    public bool canLook => PlayerManager.Instance.Player.playerMovement.canLook;
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
    public void OnBuild(InputAction.CallbackContext context)
    {
        onBuildInput?.Invoke();
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        onInteractInput?.Invoke();
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        onAttackInput?.Invoke();
    }
    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            // 인벤토리 상태를 토글
            isInventoryOpen = !isInventoryOpen;

            // 인벤토리 상태에 따라 커서 설정
            SetCursor(isInventoryOpen);

            // 인벤토리가 열리면 canLook을 false로 설정하여 플레이어가 보지 않도록 함
            PlayerManager.Instance.Player.playerMovement.canLook = !isInventoryOpen;

            // 인벤토리 이벤트 호출
            onInventoryInput?.Invoke();
        }
    }
    public void SetCursor(bool showCursor)
    {
        if (showCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; // 커서를 보이게 설정
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false; // 커서를 숨김
        }
    }
}
