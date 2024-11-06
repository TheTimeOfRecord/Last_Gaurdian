using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController playerController;
    public PlayerCondition playerCondition;
    public PlayerMovement playerMovement;
    public PlayerPhysics playerPhysics;
    public PlayerHeartSensor playerHeartSensor;

    public PlayerJump playerJump;
    public PlayerAttack playerAttack;
    private PlayerAnimationManager animationManager;

    public Action addItem; // 이 델리게이트를 실행시키게 해준다.
    public ItemData itemData; // 현재 상호작용되는 아이템의 데이터를 넣는다.

    private void Awake()
    {
        PlayerManager.Instance.Player = this;
        playerController = GetComponent<PlayerController>();
        playerCondition = GetComponent<PlayerCondition>();
        playerMovement = GetComponent<PlayerMovement>();
        playerPhysics = GetComponent<PlayerPhysics>();
        playerHeartSensor = GetComponentInChildren<PlayerHeartSensor>();

    }
    private void Start()
    {

    }
}
