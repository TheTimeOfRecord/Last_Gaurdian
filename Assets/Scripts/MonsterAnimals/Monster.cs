using UnityEngine;
public enum EAIState
{
    Idle,
    Wandering,
    Attacking
}

public abstract class Monster : MonoBehaviour, IDamagable
{
    public abstract void SetState(EAIState state);

    public abstract void PassiveUpdate();

    public abstract void AttackingUpdate();

    public abstract bool IsPlayerInFieldOfView();

    public abstract void TakeDamage(int amount, Transform attacker);
    public abstract void Heal(int amount);
}
