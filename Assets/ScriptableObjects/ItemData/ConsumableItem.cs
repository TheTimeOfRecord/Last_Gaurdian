// 2. 소비(Consumable) 아이템
using UnityEngine;
using static ConsumableItem;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Items/Consumable")]
public class ConsumableItem : ItemData
{
    public enum ConsumableType { Water, Food, Health, Buff, Both } //=> 물은 물게이지 증가 체력은 체력버프 증가 버프는..일단 지금은 깔끔하게 포기
    public ConsumableType[] consumableTypes; // 회복 음식 
    public float[] effectAmounts; // 회복 또는 버프 수치
    public float duration; // 버프 지속 시간

    public override void Use()
    {
        Debug.Log(itemName + "을(를) 사용했습니다.");

        // 소비 아이템의 여러 효과를 순회하면서 적용
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
    }
}
