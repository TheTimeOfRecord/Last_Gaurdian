using System.Collections.Generic;
using UnityEngine;

public class ItemDB
{
    Dictionary<int, DefaultItem> data = new Dictionary<int, DefaultItem>();

    public ItemDB() 
    {
        ItemContainer itemContainer = Resources.Load<ItemContainer>("Datas/ItemList"); //불러오기

        for (int i = 0; i < itemContainer.items.Count; i++)
        {
            DefaultItem item = itemContainer.items[i];
            data.Add(item.itemId, item);
        }

    }

    public DefaultItem Get(int itemID) 
    {
        if(data.ContainsKey(itemID)) return data[itemID];
        return null;
    }
    
}

