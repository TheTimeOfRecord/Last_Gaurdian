using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player/PlayerStats")]
public class PlayerStats : ScriptableObject
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
