// 3. 장비(Equipment) 아이템
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Item", menuName = "Items/Equipment")]
public class EquipmentItem : ItemData
{
    public enum EquipmentType { Tool, Weapon }
    public EquipmentType equipmentType;
    public int attackPower; // 공격력, 도구의 효율성 등

    public override void Use()
    {
        // 장비 장착 로직
        Debug.Log(itemName + "을(를) 장착했습니다. 장비 타입: " + equipmentType);

    }
}