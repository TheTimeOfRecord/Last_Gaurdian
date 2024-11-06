// 1. 자원(Resource) 아이템
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource Item", menuName = "Items/Resource")]
public class ResourceItem : ItemData
{
    public int quantity; // 갯수

    public override void Use()
    {
        // 자원 아이템은 기본적으로 'Use' 동작이 없을 수 있음.
        Debug.Log(itemName + "을(를) 사용했습니다. (실제로는 수집, 저장 등과 관련 있음)");
    }
}