//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class ItemSlot : MonoBehaviour
//{
//    public ItemData item; // 아이템 정보

//    public UIInventory inventory; //UIInventory 정보
//    public Button button; // 버튼
//    public Image icon; // 아이콘
//    public TextMeshProUGUI quatityText; // 몇개 가지고 있는지
//    private Outline outline; // 겉에 깜빡 거리게 하는 선

//    public int index; // 아이템 슬롯이 몇번째 인지 정보를 가지고 있다.
//    public bool equipped; // 장착 했는지 안했는지
//    public int quantity; // 갯수

//    private void Awake()
//    {
//        outline = GetComponent<Outline>();
//    }

//    private void OnEnable() // 켜주는 함수
//    {   // 장착 되어있을때 테두리 켜주기
//        outline.enabled = equipped;
//    }

//    public void Set() // 세팅
//    {
//        icon.gameObject.SetActive(true); // 흰색 배경을 켜주기
//        icon.sprite = item.icon; // 그 값에 이미지를 넣어준다.
//        // 만약에 1보다 갯수가 많으면 갯수를 입력해주고, 아니면 아무것도 안넣는다.
//        quatityText.text = quantity > 1 ? quantity.ToString() : string.Empty;

//        if (outline != null) // 방어코드, 반짝거리는 중이라면
//        {
//            outline.enabled = equipped; // 장착!
//        }
//    }

//    public void Clear() // 버리기
//    {
//        item = null; // 아이템 비워주고 
//        icon.gameObject.SetActive(false); // 아이템은 꺼주자
//        quatityText.text = string.Empty; // text도 비우자
//    }

//    public void OnClickButton() // 버튼이 눌리면
//    {
//        inventory.SelectItem(index); // 그 인덱스에 있는 고른아이템을 보여주자.
//    }
//}