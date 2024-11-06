using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable // 인터페이스 구현
{
    public ItemData data; //상호작용 할때 필요한 기능들

    public string GetInteractPrompt()
    { // IInteractable 이라는 컴포넌트를 찾았다면 그 인터페이스 안에 내용( 2개 ) 을 쓸 수 있다.
        string str = $"{data.itemName}\n{data.description}"; // 프롬프트에 띄울 정보 ( 이름과 설명 )
        return str; // 문자열 반환
    }

    public void OnInteract()
    { //정보를 player로 넘겨줘야하는데 직접적으로 넘길 수 없다.
        PlayerManager.Instance.Player.itemData = data; // 내가 가진 데이터 넣어주기
        PlayerManager.Instance.Player.addItem?.Invoke(); // 실행시키게 한다.
        Destroy(gameObject); // 아이템을 인벤토리에 넣었으니 맵에서 지우기
    }
}


public interface IInteractable
{
    public string GetInteractPrompt(); // 화면에 띄워줄 프롬프트
    public void OnInteract(); // 인터렉트 됐을때 어떻게 할건지
}