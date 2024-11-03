using UnityEngine;

public class PhysicsManager : SingleTonBase<PhysicsManager>
{
    [Header("Physics")]
    public PhysicsConfig physicsConfig;

    private void Awake()
    {
        physicsConfig = Resources.Load<PhysicsConfig>("Physics/PhysicsConfig");
        if (physicsConfig != null)
        {
            Debug.Log("PhysicsConfig가 성공적으로 로드되었습니다.");
        }
        else
        {
            Debug.LogError("PhysicsConfig를 찾을 수 없습니다.");
        }
    }
}