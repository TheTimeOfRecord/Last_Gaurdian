﻿using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public PlayerStats playerStats;
    [HideInInspector] public Vector2 moveDirection;
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool isMove = false;
    [HideInInspector] public bool isJumpPressed = false;
    [HideInInspector] public bool canJump = true;
    [HideInInspector] public bool isHeartWater = false;

    [Header("Look")]
    public Transform cameraContainer;
    public PlayerLookConfig playerLookConfig;
    [HideInInspector] public float camCurXRot;
    [HideInInspector] public Vector2 mouseDelta;
    [HideInInspector] public bool canLook = true;

    [Header("PlayerSensor")]
    public LayerMask waterLayer;


    private Rigidbody rigidbody;

    float lastMoveTime;
    float lastJumpTime;
    private void Start()
    {
        rigidbody = PlayerManager.Instance.Player.GetComponent<Rigidbody>();

        SetCursor();

        moveSpeed = playerStats.walkSpeed;
        lastMoveTime = Time.time;
        lastJumpTime = Time.time;

        // 심장의 Water 감지 센서
        PlayerManager.Instance.Player.playerHeartSensor.onHeartWaterEvent += SetIsHeartWater;

        // Subscribe to input events
        var playerController = GetComponent<PlayerController>();
        playerController.onMoveInput += HandleMoveInput;
        playerController.onJumpInput += HandleJumpInput;
        playerController.onRunInput += HandleRunInput;
        playerController.onLookInput += HandleLookInput;
    }

    private void Update()
    {
        canJump = CanJump();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            Look();
        }
    }

    private void HandleMoveInput(Vector2 direction)
    {
        moveDirection = direction;
    }

    private void HandleJumpInput(bool isPressed)
    {
        isJumpPressed = isPressed;
    }

    private void HandleRunInput(bool isRunning)
    {
        moveSpeed = isRunning ? playerStats.runSpeed : playerStats.walkSpeed;
    }

    private void HandleLookInput(Vector2 delta)
    {
        mouseDelta = delta;
    }



    private void Look()
    {
        CameraLook();
    }

    private void CameraLook()
    {
        camCurXRot += mouseDelta.y * playerLookConfig.lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, playerLookConfig.minXRotLook, playerLookConfig.maxXRotLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * playerLookConfig.lookSensitivity, 0);
    }

    private void Move()
    {
        if ((Time.time - lastMoveTime) > playerStats.forceDelay && (canJump || isHeartWater) && canMove)
        {
            Vector3 moveDir = transform.forward * moveDirection.y + transform.right * moveDirection.x; ;
            rigidbody.AddForce(moveDir * moveSpeed, playerStats.forceMode);
            lastMoveTime = Time.time;
        }
    }

    private void Jump()
    {
        if (isJumpPressed && canJump && canMove)
        {
            rigidbody.AddForce(transform.up * playerStats.jumpImpulse, ForceMode.Impulse);
            lastJumpTime = Time.time;
        }
    }

    private bool CanJump()
    {
        if ((Time.time - lastJumpTime > playerStats.waterJumpDelay) && isHeartWater)//물에서의 점프 딜레이, 심장위치에 물이있으면 점프가능
        {
            return true;
        }

        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.5f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.5f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.5f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.5f) + (transform.up * 0.1f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            // TODO : 점프가능한 레이어 마스크 설정
            if (Physics.Raycast(rays[i], 0.1f, playerStats.canJumpLayer))//
            {
                return true;
            }
        }
        return false;
    }

    public void SetIsHeartWater(bool inputIsHeartWater)
    {
        isHeartWater = inputIsHeartWater;
    }

    public void SetCursor()
    {
        if (canLook)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}