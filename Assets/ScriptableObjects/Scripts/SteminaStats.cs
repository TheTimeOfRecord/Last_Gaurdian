using UnityEngine;

[CreateAssetMenu(fileName = "SteminaStats", menuName = "Stemina/SteminaStats")]
public class SteminaStats : ScriptableObject
{
    [Header("Stemina")]
    public float runStemina;
    public float jumpStemina;
    public float attackStemina;
}