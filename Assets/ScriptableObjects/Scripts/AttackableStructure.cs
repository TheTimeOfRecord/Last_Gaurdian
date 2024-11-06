using UnityEngine;

[CreateAssetMenu(fileName = "Attackable Structure", menuName = "Structure/Attackable Structure")]
public class AttackableStructure : Structure
{
    [Header("Attack Info")]
    public float attackDamage;
    public float attackDelay;
    public float damageIncreasePerLevel;
    public float attackRange;

    [Header("Ranged Info")]
    public bool isRanged;
    public GameObject projectilePrefab;
}