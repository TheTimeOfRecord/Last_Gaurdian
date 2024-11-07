using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Container", menuName = "Items/Item Container/Item Container")]
public class ItemContainer : ScriptableObject
{
    public List<DefaultItem> items;
}
