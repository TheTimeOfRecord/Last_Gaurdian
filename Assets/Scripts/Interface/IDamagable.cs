using UnityEngine;

public interface IDamagable
{
    void TakeDamage(int amount, Transform attacker);
    void Heal(int amount);
}
