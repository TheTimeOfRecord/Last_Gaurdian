// 2. 소비(Consumable) 아이템
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Items/Consumable")]
public class ConsumableItem : ItemData
{
    public enum ConsumableType { Water, Food, Health, Buff }
    public ConsumableType consumableType;
    public float effectAmount; // 회복 또는 버프 수치
    public float duration; // 버프 지속 시간

    public override void Use()
    {
        // 소비 아이템의 효과를 적용하는 로직
        Debug.Log(itemName + "을(를) 사용하여 " + consumableType + " 효과를 발동했습니다.");
    }
}