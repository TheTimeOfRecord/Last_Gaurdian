using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInteraction : MonoBehaviour
{ // 카메라를 기준으로 Ray를 쏠 것이다.
    public float checkRate = 0.05f; // 얼마나 자주 업데이트 될 것인가.
    private float lastCheckTime; // 마지막 채크시간
    public float maxCheckDistance; // 얼마나 멀리 있는지 채크
    public LayerMask layerMask; // 레이어마스크 어떤 레이어마스크를 추출할건지

    public GameObject curInteractGameObject; // 현재 상호작용 된 오브젝트를 가지고 있을 것이다.
    public IInteractable curInteractable; // 인터페이스 캐싱된 정보가 여기 담긴다.

    public TextMeshProUGUI promptText; // 프롬포트에 보이게 하기 => private 배워보기
    private Camera camera;

    void Start()
    {   // 카메라는 메인카메라 값을 넣어준다.
        camera = Camera.main;
        PlayerManager.Instance.Player.playerController.onInteractInput += OnInteractInput;
    }

    void Update()
    { // 계속 상호작용 할 수 있게끔 Ray를 쏴줘야한다.
        if (Time.time - lastCheckTime > checkRate) // 얼마나 자주 검사해줄건지,
        {
            lastCheckTime = Time.time; // 마지막시간을 여기 넣어주고,
            // Ray는 카메라에서 쏜다. 정중앙에서 쏘기 위해 /2를 해준다.
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit; // 부딪히면 여기 데이터가 담기게끔

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask)) // 충돌이 됐을때
            {
                if (hit.collider.gameObject != curInteractGameObject)  // 상호작용인 오브젝트가 없다면
                {
                    curInteractGameObject = hit.collider.gameObject; // 새로운 오브젝트 넣어주기
                    curInteractable = hit.collider.GetComponent<IInteractable>(); // 컴포넌트 가져오기
                    SetPromptText(); // 이 정보를 담아뒀다면 프롬포트에 출력해주라
                }
            }
            else
            {   //빈공간에 쐈다면 정보를 암것도 없게 비워둬야한다. 새로 넣을 자리도 만들 겸,
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false); // 프롬포트 꺼주기
            }
        }
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    { // f 키를 눌렀을때 작동하게끔 해주는 엑션
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        { // 버튼이 눌렸을때, 허공에 아이템을 눌리지 않았을때 ( = 물건을 바라볼때 )
            curInteractable.OnInteract(); // 상호작용한다.
            curInteractGameObject = null; // 상호작용하고나서 꺼주기
            curInteractable = null;
            promptText.gameObject.SetActive(false); // 프롬포트 꺼주기
        }
    }

    private void SetPromptText() // 프롬포트에 출력해주기
    {
        promptText.gameObject.SetActive(true); // 프롬포트 보이게 하기
        promptText.text = curInteractable.GetInteractPrompt(); // ItemObject.cs에 있는 함수가 실행된다.
    }

}