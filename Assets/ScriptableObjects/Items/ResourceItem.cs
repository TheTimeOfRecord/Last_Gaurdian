using UnityEngine;

[CreateAssetMenu(fileName = "Resource Item", menuName = "Items/Items/Resource Item")]
public class ResourceItem : DefaultItem
{
    [Header("Resource Info")]
    public int quantity;
}