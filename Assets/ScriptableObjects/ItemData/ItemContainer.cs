using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemList", menuName = "Items/ItemList")]
public class ItemContainer : ScriptableObject
{
    public List<ItemData> itmes;
}
