using UnityEngine;
using UnityEngine.AI;

public class MonsterData : ScriptableObject
{
    [Header("Stats")] 
    public int health;
    public float walkSpeed;
    public float runSpeed;
    public DefaultItem[] dropOnDeath;

    [Header("AI")] 
    public float detectDistance;

    [Header("Combat")] 
    public float damage;
    public float attackRate;
    public float attackDistance;

    public float fieldOfView;
}
