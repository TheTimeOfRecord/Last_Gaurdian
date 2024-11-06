using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public enum EAIState
{
    Idle,
    Wandering,
    Attacking
}

public abstract class Monster : MonoBehaviour
{
    public abstract void SetState(EAIState state);

    public abstract void PassiveUpdate();

    public abstract void AttackingUpdate();

    public abstract bool IsPlayerInFieldOfView();
}
