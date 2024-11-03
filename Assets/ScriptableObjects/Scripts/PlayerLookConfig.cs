using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLookConfig", menuName = "Player/PlayerLookConfig")]
public class PlayerLookConfig : ScriptableObject
{
    [Header("Look")]
    public float maxXRotLook;
    public float minXRotLook;
    public float lookSensitivity;
}
