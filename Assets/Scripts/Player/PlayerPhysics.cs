using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    Rigidbody rigidbody;
    PhysicsConfig physicsConfig => PhysicsManager.Instance.physicsConfig;
    private void Start()
    {
        rigidbody = PlayerManager.Instance.Player.GetComponent<Rigidbody>();
        rigidbody.drag = physicsConfig.groundDrag;

        PlayerManager.Instance.Player.playerHeartSensor.onHeartWaterEvent += OnWaterPhysics;
    }
    public void OnWaterPhysics(bool inputOnWater)
    {
        if (inputOnWater)
        {
            rigidbody.drag = physicsConfig.waterDrag;
        }
        else
        {
            rigidbody.drag = physicsConfig.groundDrag;
        }
    }
}