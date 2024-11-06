using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NonAggressiveMonsterData", menuName = "Monster/NonAggressiveMonsterData")]
public class NonAggressiveMonsterData : MonsterData
{
    [Header("Wandering")] 
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;
}
