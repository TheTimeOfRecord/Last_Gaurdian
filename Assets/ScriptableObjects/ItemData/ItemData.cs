using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite icon;
    public int itemID;

    public abstract void Use();
}






