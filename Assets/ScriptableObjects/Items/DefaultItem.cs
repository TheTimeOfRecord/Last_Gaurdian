using UnityEngine;

[CreateAssetMenu(fileName = "Default Item", menuName = "Items/Items/Default Item")]
public class DefaultItem : ScriptableObject
{
    [Header("Default Info")]
    public int itemId;
    public string itemName;
    public string itemDescription;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking Info")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Utility Info")]
    public float range;
}
