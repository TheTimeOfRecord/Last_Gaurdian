using UnityEngine;

[CreateAssetMenu(fileName = "PhysicsConfig", menuName = "Enviornments/PhysicsConfig")]
public class PhysicsConfig : ScriptableObject
{
    [Header("Physics")]
    public float groundDrag;
    public float waterDrag;
}