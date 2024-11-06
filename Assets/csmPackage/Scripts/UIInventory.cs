using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static ConsumableItem;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots; // 아이템 슬롯들의 정보

    public GameObject inventoryWindow; // 인벤토리 창
    public Transform slotPanel; // 슬롯 판넬
    public Transform dropPosition; // 떨어지는 아이템 위치

    [Header("Selected Item")]
    private ItemSlot selectedItem; // 고른 아이템
    private int selectedItemIndex; // 고른 인덱스
    public TextMeshProUGUI selectedItemName; // 아이템 이름
    public TextMeshProUGUI selectedItemDescription; // 아이템 설명
    public TextMeshProUGUI selectedItemStatName; // 스탯 이름
    public TextMeshProUGUI selectedItemStatValue; // 스탯 값
    public GameObject useButton; // 사용 버튼
    public GameObject equipButton; // 장착 버튼
    public GameObject unEquipButton; // 장착해제 버튼
    public GameObject dropButton; // 버리기 버튼

    private int curEquipIndex; // 장착하기를 누르면 여기로 아이템 정보가 들어가고, 해당 데이터를 넘겨준다.


    private PlayerController controller; // 정보를 주고받을 컨트롤러
    private PlayerCondition condition; // 정보를 주고받을 컨디션

    void Start()
    {
        controller = PlayerManager.Instance.Player.playerController;
        condition = PlayerManager.Instance.Player.playerCondition; ;
        dropPosition = PlayerManager.Instance.Player.transform;

        controller.onInventoryInput += Toggle;
        PlayerManager.Instance.Player.addItem += AddItem;

        inventoryWindow.SetActive(false); // 처음에는 인벤토리창 꺼줘야한다.
        slots = new ItemSlot[slotPanel.childCount]; //슬롯을 초기화해줘야한다.

        for (int i = 0; i < slots.Length; i++)
        { //각각 초기화하기위한 for문
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>(); // 정보기입
            slots[i].index = i; // 인덱스번호 지정
            slots[i].inventory = this; // 인벤토리는 나야..!
            slots[i].Clear(); // 값 초기화
        }

        ClearSelectedItemWindow(); // 선택된 친구들도 다 초기화 시켜줘야한다.
    }

    public void Toggle() // 켜져있다면 false, 꺼져있다면 true
    {
        if (IsOpen())
        {
            inventoryWindow.SetActive(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
        }
    }

    public bool IsOpen() // 열려있는지 아닌지 판단해주는 bool값
    {   // 하이라키창에 켜져있는지 아닌지 알 수 있는 친구
        return inventoryWindow.activeInHierarchy;
    }

    public void AddItem() // 아이템을 추가
    {   //여기 itemData는 현재 상호작용된 아이템의 데이터를 넣어놨다.
        ItemData data = PlayerManager.Instance.Player.itemData;

        if (data.canStack) // 아이템이 중복 가능한지
        {
            ItemSlot slot = GetItemStack(data); // 슬롯 가져오기
            if (slot != null) // 슬롯이 비어있지 않다면
            {
                slot.quantity++; // 슬롯 갯수 증가
                UpdateUI(); //UI 업데이트
                PlayerManager.Instance.Player.itemData = null; // 아이템 데이터 없애기
                return;
            }
        }
        // 비어있는 슬롯 가져오기
        ItemSlot emptySlot = GetEmptySlot();

        //비어있는 슬롯이 있다면
        if (emptySlot != null)
        {
            emptySlot.item = data; // 데이터 넣어주고
            emptySlot.quantity = 1; //1로 올려주고
            UpdateUI(); // 업데이트 해주고
            PlayerManager.Instance.Player.itemData = null; // 아이템 데이터 없애기
            return;
        }
        //비어있는 슬롯이 없다면
        ThrowItem(data); // 아이템을 버려야한다.
        PlayerManager.Instance.Player.itemData = null; // 아이템 데이터 없애기
    }

    public void ThrowItem(ItemData data) // 아이템 떨어뜨리기
    {   //생성, 버릴 위치, 버릴 프리팹, 360도 어디에서 랜덤하게 떨어뜨리기
        Instantiate(data.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++) // 슬롯 배열 순회
        {
            if (slots[i].item != null) // 데이터가 들어가있다면
            {
                slots[i].Set(); // 세팅해주세요
            }
            else
            {
                slots[i].Clear(); // 없애주세요
            }
        }
    }

    ItemSlot GetItemStack(ItemData data)
    {
        for (int i = 0; i < slots.Length; i++) // 슬롯 배열 순회
        {   // 데이터와 슬롯의 아이템이 같다면 그리고 그 갯수가 최대값이 아니면
            if (slots[i].item == data && slots[i].quantity < data.maxStackAmount)
            {
                return slots[i]; // 슬롯을 반환
            }
        }
        return null; // 아니면 스킵, 여기서 스킵일 경우 새롭게 슬롯을 만드는 방향으로 진행 될 것!
    }

    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++) // 슬롯 배열 순회
        {
            if (slots[i].item == null) // 아이템에 값이 없다면
            {
                return slots[i]; // 그냥 그대로 반환해주세요.없는거 보여주세요!
            }
        }
        return null; // 꽉찼으면 그냥 꽉찬채로!
    }

    public void SelectItem(int index) // 우리가 고른 아이템, 슬롯번호만 받아온다.  => 입는건지 먹는건지 구별하는 방법..
    {
        if (slots[index].item == null) return; // 인덱스의 정보에 접근할건데 값이 없으면 넘기기

        selectedItem = slots[index]; // 선택된 아이템의 정보를 가지고 있어보자
        selectedItemIndex = index; // 선택된 아이템의 인덱스도 가지고 있자

        selectedItemName.text = selectedItem.item.itemName; //각각의 요소에다가 값을 넣어준다. 여기는 이름
        selectedItemDescription.text = selectedItem.item.description; // 그리고 설명을 적어준다.
        //스탯이랑 스탯값도 넣어주어야하는데 모든 아이템에 스탯이 있는건 아니다.
        selectedItemStatName.text = string.Empty; // 그래서 일단 값을 없앤다.
        selectedItemStatValue.text = string.Empty; // 없애고 포문을 돌려서 넣어준다.

        //for (int i = 0; i < selectedItem.item.consumables.Length; i++) // 사용아이템이 없다면 for문이 돌지 않을것이다. => 사용 이유 health 10 만들기 위해
        //{
        //    selectedItemStatName.text += selectedItem.item.consumables[i].consumableTypes.ToString() + "\n"; // 다음을 위해 엔터
        //    selectedItemStatValue.text += selectedItem.item.consumables[i].effectAmounts.ToString() + "\n";
        //}

        useButton.SetActive(selectedItem.item is ConsumableItem); // 버튼 type을 가져와서 넣어준다.
        equipButton.SetActive(selectedItem.item is EquipmentItem && !slots[index].equipped); // 그리고 장착이 되어있지않다면
        unEquipButton.SetActive(selectedItem.item is EquipmentItem && slots[index].equipped); // 장착이 되어있다면
        dropButton.SetActive(true); // 없애는건 무조건 켜줘야한다.
    }

    void ClearSelectedItemWindow()
    {
        selectedItem = null;

        selectedItemName.text = string.Empty; // 비우기
        selectedItemDescription.text = string.Empty;
        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        useButton.SetActive(false); // 비활성화
        equipButton.SetActive(false);
        unEquipButton.SetActive(false);
        dropButton.SetActive(false);
    }

    public void OnUseButton() // 사용하기
    {
        if (selectedItem.item is ConsumableItem) //type이 consumable일때만 가능하다 먹어야하니까..
        {
            for (int i = 0; i < selectedItem.item.consumables.Length; i++) // 아이템 조회
            {
                //switch (selectedItem.item.consumables[i].consumableTypes) //i번째 type을 보고
                //{   //우리는 29번째줄에 condition을 가져왔었으니
                //    case ConsumableType.Health: // 체력 회복
                //        condition.Heal(selectedItem.item.consumables[i].effectAmounts); break;
                //    case ConsumableType.Food: // 배고픔 회복
                //        condition.Eat(selectedItem.item.consumables[i].effectAmounts); break;
                //}
            }
            RemoveSelctedItem(); // 아이템 하나 빼기
        }
    }

    public void OnDropButton() // 버리기
    {
        ThrowItem(selectedItem.item); // 버리기 기능
        RemoveSelctedItem(); // 템을 지워주자.
    }

    void RemoveSelctedItem() //UI 정보 업데이트용
    {
        selectedItem.quantity--; // 갯수 하나 빼주기

        if (selectedItem.quantity <= 0) // 0보다 작아지면
        {
            //if (slots[selectedItemIndex].equipped)
            //{
            //    UnEquip(selectedItemIndex);
            //}

            selectedItem.item = null; //템 없애기
            ClearSelectedItemWindow(); //템 없애기
        }

        UpdateUI();
    }

    public void OnEquipButton() // 장착하기 버튼
    {
        if (slots[curEquipIndex].equipped) //그 인덱스가 있다면
        {
            UnEquip(curEquipIndex); //장착을 해제
        }

        slots[selectedItemIndex].equipped = true; // 이제 장착
        curEquipIndex = selectedItemIndex; // 골라진 아이템을 넣어준다.
        //PlayerManager.Instance.Player.equip.EquipNew(selectedItem.item); //데이터를 넘겨준다.
        UpdateUI(); //UI업데이트

        SelectItem(selectedItemIndex); //선택한 아이템을 호출
    }

    void UnEquip(int index) // 장착 해제
    {
        slots[index].equipped = false; // 장착 해제
        //PlayerManager.Instance.Player.equip.UnEquip(); // 빼주기
        UpdateUI(); // 빼줬으니 UI업데이트

        if (selectedItemIndex == index) //혹시라도, 선택한 데이터가 있다면
        {
            SelectItem(selectedItemIndex); //선택한것이기 때문에 호출해줘서 끼고있다고 알려준다.
        }
    }

    public void OnUpEquipButton() //장착 해제
    {
        UnEquip(selectedItemIndex); //이러면 장착해제는 한줄 함수 완성
    }

    public bool HasItem(ItemData item, int quantity)
    {
        return false;
    }
}