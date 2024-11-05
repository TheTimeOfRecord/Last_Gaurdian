using UnityEngine;

[CreateAssetMenu(fileName = "HealthStats", menuName = "Health/HealthStats")]
public class HealthStats : ScriptableObject
{
    [Header("Health")]
    public float curHP;
    public float maxHP;
}