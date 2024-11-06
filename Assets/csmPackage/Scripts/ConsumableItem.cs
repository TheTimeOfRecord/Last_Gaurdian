// 2. 소비(Consumable) 아이템
using UnityEngine;
using static ConsumableItem;

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

        for (int i = 0; i < consumableTypes.Length; i++)
        {
            ConsumableType type = consumableTypes[i];
            float amount = effectAmounts[i];

            // 각 효과에 따른 처리를 구현
            switch (type)
            {
                case ConsumableType.Water:
                    Debug.Log("Water 게이지가 " + amount + "만큼 증가합니다.");
                    // 물 게이지 회복 로직
                    break;
                case ConsumableType.Food:
                    Debug.Log("Food 게이지가 " + amount + "만큼 증가합니다.");
                    // 음식 게이지 회복 로직
                    break;
                case ConsumableType.Health:
                    Debug.Log("Health 게이지가 " + amount + "만큼 증가합니다.");
                    // 체력 회복 로직
                    break;
                case ConsumableType.Buff:
                    Debug.Log("버프 효과가 " + duration + "초 동안 적용됩니다.");
                    // 버프 효과 적용 로직
                    break;
            }
        }
        Debug.Log(itemName + "을(를) 사용하여 " + consumableType + " 효과를 발동했습니다.");
    }
}

