using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementStats", menuName = "Player/MovementStats")]
public class PlayerMovementStats : ScriptableObject
{
    [Header("Movement")]
    public ForceMode forceMode;
    public float forceDelay;
    public float walkSpeed;
    public float runSpeed;
    public float jumpImpulse;
    public float waterJumpDelay;
    public LayerMask canJumpLayer;
}
