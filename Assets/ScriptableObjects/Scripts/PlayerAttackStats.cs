using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttackStats", menuName = "Player/AttackStats")]
public class PlayerAttackStats : ScriptableObject
{
    [Header("Attack")]
    public float attackDamage;
    public float attackDelay;
}
