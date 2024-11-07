using System;
using System.Collections.Generic;
using UnityEngine;
using static ConsumableItem;


public enum ConsumableType 
{ 
    Thirsty, Hunger, HP, Speed, Damage, Water
}

[Serializable]
public class ConsumableValuePerType
{
    public ConsumableType type;
    public float value;
}

[CreateAssetMenu(fileName = "Consumable Item", menuName = "Items/Items/Consumable Item")]
public class ConsumableItem : DefaultItem
{
    [Header("Consumable Info")]
    public ConsumableValuePerType[] consumableValuePerTypes;
    public int quantity;
}

