using UnityEngine;

[CreateAssetMenu(fileName = "AttackStats", menuName = "Attack/AttackStats")]
public class AttackStats : ScriptableObject
{
    [Header("Attack")]
    public float attackDamage;
    public float attackDelay;
}
