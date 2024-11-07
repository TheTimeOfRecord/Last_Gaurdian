using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ResourceGatheringSpeed
{
    public int resource;
    public float gatheringSpeed;
}
[CreateAssetMenu(fileName = "Equipment Item", menuName = "Items/Items/Equipment Item")]
public class EquipmentItem : DefaultItem
{
    [Header("Equipment Info")]
    public float attackDamage;
    public float attackDelay;
    public float swingPerStemina;
    public ResourceGatheringSpeed[] resourceGatheringSpeeds;
    public Transform equipmentTransform;
}