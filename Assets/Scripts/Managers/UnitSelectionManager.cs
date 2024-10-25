using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class UnitSelectionManager : MonoBehaviour
{
    public static UnitSelectionManager Instance { get; private set; }

    private int currentUnitCode;  // 현재 선택한 유닛의 코드 (0~7)
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
        units = UnitManager.Instance.GetUnits(GameManager.Instance.playerFaction);

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
        }

        /*idleButton.onClick.AddListener(OnIdleButtonClicked);
        moveButton.onClick.AddListener(OnMoveButtonClicked);
        attackButton.onClick.AddListener(OnAttackButtonClicked);
        damagedButton.onClick.AddListener(OnDamagedButtonClicked);
        diedButton.onClick.AddListener(OnDiedButtonClicked);
        debuffedButton.onClick.AddListener(OnDebuffedButtonClicked);*/

        selectButton.onClick.AddListener(OnSelectButtonClicked);
        startButton.onClick.AddListener(OnStartButtonClicked);

        // 첫 번째 유닛의 정보를 화면에 표시한 상태로 시작
        OnUnitButtonClicked(units[0]);
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
        Destroy(currentUnitPrefab);
        currentUnitCode = unit.basicStats.unitCode;
        currentUnitPrefab = UnitPrefabManager.Instance.InstantiateUnitPrefab(currentUnitCode, 360.0f);
        currentUnitPrefab.transform.SetParent(unitDisplayArea.transform, false);
        RectTransform rt = currentUnitPrefab.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(0.0f, -180.0f);

        // CenterPanel 상단에 현재 선택한 유닛 이름을 표시
        currentUnitNameText.text = unit.basicStats.unitName;
    }

    void OnSelectButtonClicked()
    {
        if (selectedUnitCodes.Count < 5)
        {
            if (!selectedUnitCodes.Contains(currentUnitCode))
            {
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
            }
            else
            {
                Debug.Log("선택된 유닛입니다.");
            }
        }
        else
        {
            Debug.Log("이미 5명입니다.");
        }
        
    }

    void OnStartButtonClicked()
    {
        UnitManager.Instance.player1Units.Clear();
        if (selectedUnitCodes.Count == 5)
        {
            foreach (int unitCode in selectedUnitCodes)
            {
                UnitManager.Instance.player1UnitCodes.Add(unitCode);
                UnitManager.Instance.player1Units.Add(new(UnitManager.Instance.GetUnit(unitCode)));
            }

            SceneManager.LoadScene("DeployScene");
        }
        else
        {
            Debug.Log("5개의 유닛을 선택해야 합니다.");
        }
    }

    void OnDeleteButtonClicked(int unitCode)
    {
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
