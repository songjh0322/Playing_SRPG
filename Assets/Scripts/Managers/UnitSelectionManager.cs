using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;

public class UnitSelectionManager : MonoBehaviour
{
    public static UnitSelectionManager Instance { get; private set; }

    public int currentUnitCode;  // 현재 선택한 유닛의 코드 (0~7)
    private GameObject currentUnitPrefab;
    private List<int> selectedUnitCodes;
    private List<Unit> units;   // 현재 선택한 진영의 유닛들 (8명)
    private List<Unit> selectedUnits;

    // LeftPanel
    public List<Button> unitButtons;    // Inspector에서 할당
    public List<Text> unitNameTexts;

    // CenterPanel
    public Text currentUnitNameText;
    public GameObject unitDisplayArea;  // Inspector에서 할당
    public GameObject unitModel;
    public Button idleButton;   // Inspector에서 할당
    public Button moveButton;   // Inspector에서 할당
    public Button attackButton; // Inspector에서 할당
    public Button damagedButton;    // Inspector에서 할당
    public Button diedButton;   // Inspector에서 할당
    public Button DebuffedButton;   // Inspector에서 할당

    // RightPanel
    public List<GameObject> selectedUnitBars;
    public GameObject content;      // Vertical Layout Group 컴포넌트를 가짐 (Inspector에서 할당)
    public GameObject selectedUnitBarPrefab;    // Inspector에서 할당
    public Button selectButton;     // Inspector에서 할당
    public Button startButton;      // Inspector에서 할당
    public GameObject[] popup; // 캐릭터 선택에 대한 팝업창

    bool isFirstCall = true;

    // 싱글톤 인스턴스 설정
    private void Awake()
    {
        Debug.Log("UnitSelectionManager 생성됨");

        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        selectedUnitCodes = new List<int>();
        units = new List<Unit>();
        selectedUnits = new List<Unit>();
        selectedUnitBars = new List<GameObject>();
        selectedUnitCodes.Clear();
        units.Clear();
        selectedUnits.Clear();
        selectedUnitBars.Clear();

        // 플레이어가 선택한 진영의 모든 유닛을 저장
        units = UnitManager.Instance.GetAllUnits(GameManager.Instance.playerFaction);

        // 현재 진영의 유닛 정보를 등록
        for (int i = 0; i < unitNameTexts.Count; i++)
        {
            unitNameTexts[i].text = units[i].basicStats.unitName;
        }

        // 이벤트 리스너 등록
        for (int i = 0; i < unitButtons.Count; i++)
        {
            int index = i;
            unitButtons[index].onClick.AddListener(() => OnUnitButtonClicked(units[index]));
            // idleButton.onClick.AddListener(() => OnIdleButtonClicked(units[index]));
            // moveButton.onClick.AddListener(() => OnMoveButtonClicked(units[index]));
            // attackButton.onClick.AddListener(() => OnAttackButtonClicked(units[index]));
            // damagedButton.onClick.AddListener(OnDamagedButtonClicked);
            // diedButton.onClick.AddListener(OnDiedButtonClicked);
            // debuffedButton.onClick.AddListener(OnDebuffedButtonClicked);    
        }

        idleButton.onClick.AddListener(OnIdleButtonClicked);
        moveButton.onClick.AddListener(OnMoveButtonClicked);
        attackButton.onClick.AddListener(OnAttackButtonClicked);
        // damagedButton.onClick.AddListener(OnDamagedButtonClicked);
        // diedButton.onClick.AddListener(OnDiedButtonClicked);
        // debuffedButton.onClick.AddListener(OnDebuffedButtonClicked);

        selectButton.onClick.AddListener(OnSelectButtonClicked);
        startButton.onClick.AddListener(OnStartButtonClicked);

        // 첫 번째 유닛의 정보를 화면에 표시한 상태로 시작
        OnUnitButtonClicked(units[0]);
        // OnUnitButtonClicked 함수가 처음 불렸을 때는 효과음이 재생되지 않도록 함
        isFirstCall = false;
    }

    private void OnIdleButtonClicked()
    {
        Animator anim = unitModel.GetComponent<Animator>();
        anim.runtimeAnimatorController = UnitPrefabManager.Instance.allIdleAnimControllers[currentUnitCode];
    }

        private void OnMoveButtonClicked()
    {
        Animator anim = unitModel.GetComponent<Animator>();
        anim.runtimeAnimatorController = UnitPrefabManager.Instance.allIdleAnimControllers[currentUnitCode];
    }

    private void OnAttackButtonClicked()
    {
        Animator anim = unitModel.GetComponent<Animator>();
    }

    // 뒤로 가기
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("FactionSelectionScene");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            RectTransform rt = currentUnitPrefab.GetComponent<RectTransform>();
            rt.position = new Vector3(0f, 680.0f, 0f);
        }
    }

    void OnUnitButtonClicked(Unit unit)
    {
        if (!isFirstCall)
        {
            AudioManager.instance.PlayEffect("successButton");
        }

        currentUnitCode = unit.basicStats.unitCode;

        OnIdleButtonClicked();

        // 기존 방법
        // Destroy(currentUnitPrefab);
        // currentUnitPrefab = UnitPrefabManager.Instance.InstantiateUnitPrefab(currentUnitCode, 360.0f, false);
        // currentUnitPrefab.transform.SetParent(unitDisplayArea.transform, false);
        // RectTransform rt = currentUnitPrefab.GetComponent<RectTransform>();
        // rt.anchoredPosition = new Vector2(0.0f, -180.0f);

        // 스프라이트 사용 (애니메이션 컨트롤러)


        // CenterPanel 상단에 현재 선택한 유닛 이름을 표시
        currentUnitNameText.text = unit.basicStats.unitName;
    }

    void OnSelectButtonClicked()
    {
        if (selectedUnitCodes.Count < 5)
        {
            if (!selectedUnitCodes.Contains(currentUnitCode))
            {
                AudioManager.instance.PlayEffect("successButton");
                // 데이터 관리
                selectedUnitCodes.Add(currentUnitCode);
                selectedUnitCodes.Sort();

                // 시각적으로 표시
                GameObject unitBar = Instantiate(selectedUnitBarPrefab);
                unitBar.transform.SetParent(content.transform);
                unitBar.GetComponent<ButtonIndexer>().buttonIndex = currentUnitCode;    // buttonIndex를 unitCode와 같게 설정
                selectedUnitBars.Add(unitBar);

                Unit unit = UnitManager.Instance.GetUnit(currentUnitCode);
                unitBar.GetComponentInChildren<Text>().text = unit.basicStats.unitName;

                // 이벤트 리스너 등록
                Button deleteButton = unitBar.GetComponentInChildren<Button>();
                int capturedIndex = currentUnitCode;
                deleteButton.onClick.AddListener(() => OnDeleteButtonClicked(capturedIndex));

                // 정렬
                SortUnitBarsByButtonIndex();
                // 버튼 색상 변경 코드 추가
                if (GameManager.Instance.playerFaction == Faction.Guwol)
                    unitButtons[currentUnitCode - 1].GetComponent<Image>().color = new Color(70f / 255f, 176f / 255f, 190f / 255f);
                else
                    unitButtons[currentUnitCode - 9].GetComponent<Image>().color = new Color(70f / 255f, 176f / 255f, 190f / 255f);


            }
            else
            {
                AudioManager.instance.PlayEffect("failButton");
                Debug.Log("선택된 유닛입니다.");
            }
        }
        else
        {
            AudioManager.instance.PlayEffect("failButton");
            Debug.Log("이미 5명입니다.");
            // 팝업창을 띄우고 3초 후 비활성화
            popup[0].SetActive(true);
            StartCoroutine(HidePopupAfterDelay(3f));
        }

    }
    IEnumerator HidePopupAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        popup[0].SetActive(false);
    }

    void OnStartButtonClicked()
    {
        UnitManager.Instance.player1Units.Clear();
        if (selectedUnitCodes.Count == 5)
        {
            AudioManager.instance.PlayEffect("successButton");
            foreach (int unitCode in selectedUnitCodes)
            {
                UnitManager.Instance.player1UnitCodes.Add(unitCode);
                UnitManager.Instance.player1Units.Add(new(UnitManager.Instance.GetUnit(unitCode)));
            }

            foreach (Unit unit in UnitManager.Instance.player1Units)
                unit.team = Team.Ally;

            SceneManager.LoadScene("DeployScene");
        }
        else
        {
            AudioManager.instance.PlayEffect("failButton");

            Debug.Log("5개의 유닛을 선택해야 합니다.");
            // 팝업창을 띄우고 3초 후 비활성화
            popup[1].SetActive(true);
            StartCoroutine(HidePopupAfterDelay2(3f));
        }
    }
    IEnumerator HidePopupAfterDelay2(float delay)
    {
        yield return new WaitForSeconds(delay);
        popup[1].SetActive(false);
    }

    void OnDeleteButtonClicked(int unitCode)
    {
        AudioManager.instance.PlayEffect("discard");
        // 데이터 관리
        selectedUnitCodes.Remove(unitCode);
        foreach (GameObject unitBar in selectedUnitBars)
        {
            if (unitBar.GetComponent<ButtonIndexer>().buttonIndex == unitCode)
            {
                selectedUnitBars.Remove(unitBar);
                Destroy(unitBar);
                break;
            }
        }
        // 버튼 색상 초기화
        if (GameManager.Instance.playerFaction == Faction.Guwol)
            unitButtons[unitCode - 1].GetComponent<Image>().color = new Color(190f / 255f, 104f / 255f, 69f / 255f);
        else
            unitButtons[unitCode - 9].GetComponent<Image>().color = new Color(190f / 255f, 104f / 255f, 69f / 255f);
    }

    // selectedUnitBar를 정렬하는 함수(unitCode를 기준으로)
    void SortUnitBarsByButtonIndex()
    {
        // 자식 오브젝트들을 리스트로 불러오기
        List<Transform> children = new List<Transform>();

        foreach (Transform child in content.transform)
            children.Add(child);

        // buttonIndex를 기준으로 정렬
        children.Sort((a, b) => a.GetComponent<ButtonIndexer>().buttonIndex.CompareTo(b.GetComponent<ButtonIndexer>().buttonIndex));

        // 정렬된 순서로 다시 부모에 배치
        for (int i = 0; i < children.Count; i++)
            children[i].SetSiblingIndex(i);
    }
}
