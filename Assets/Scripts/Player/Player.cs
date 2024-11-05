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
    private void Awake()
    {
        PlayerManager.Instance.Player = this;
        playerController = GetComponent<PlayerController>();
        playerCondition = GetComponent<PlayerCondition>();
        playerMovement = GetComponent<PlayerMovement>();
        playerPhysics = GetComponent<PlayerPhysics>();
        playerHeartSensor = GetComponentInChildren<PlayerHeartSensor>();
    }

}
