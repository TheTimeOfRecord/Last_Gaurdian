using System.Collections.Generic;
using UnityEngine;

public class ItemDB
{
    Dictionary<int, ItemData> data = new Dictionary<int, ItemData>();

    public ItemDB() 
    {
        ItemContainer itemContainer = Resources.Load<ItemContainer>("Datas/ItemList"); //불러오기

        for (int i = 0; i < itemContainer.itmes.Count; i++)
        {
            ItemData item = itemContainer.itmes[i];
            data.Add(item.itemID, item);
        }

    }

    public ItemData Get(int itemID) 
    {
        if(data.ContainsKey(itemID)) return data[itemID];
        return null;
    }
    
}

