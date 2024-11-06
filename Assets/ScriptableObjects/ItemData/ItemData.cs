using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite icon;
    public int itemID;
    public GameObject dropPrefab; //프리팹 정보도 가지고 와본다.

    [Header("Stacking")] // 아이템은 여러개 들고 있을 수 있다.
    public bool canStack; // 여러개 아이템인가? 아닌가?
    public int maxStackAmount; // 얼마나 많이 가지고 있을지

    [Header("Consumable")] // 소비하는 아이템 중
    public ConsumableItem[] consumables; // 체력회복이냐 배고픔 회복이냐 구분

    public abstract void Use();
}

