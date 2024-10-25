using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class UnitSelectionManager : MonoBehaviour
{
    public static UnitSelectionManager Instance { get; private set; }

    private int currentUnitCode;  // ���� ������ ������ �ڵ� (0~7)
    private GameObject currentUnitPrefab;
    private List<int> selectedUnitCodes;
    private List<Unit> units;   // ���� ������ ������ ���ֵ� (8��)
    private List<Unit> selectedUnits;

    // LeftPanel
    public List<Button> unitButtons;    // Inspector���� �Ҵ�
    public List<Text> unitNameTexts;

    // CenterPanel
    public Text currentUnitNameText;
    public GameObject unitDisplayArea;  // Inspector���� �Ҵ�
    public Button idleButton;   // Inspector���� �Ҵ�
    public Button moveButton;   // Inspector���� �Ҵ�
    public Button attackButton; // Inspector���� �Ҵ�
    public Button damagedButton;    // Inspector���� �Ҵ�
    public Button diedButton;   // Inspector���� �Ҵ�
    public Button DebuffedButton;   // Inspector���� �Ҵ�

    // RightPanel
    public List<GameObject> selectedUnitBars;
    public GameObject content;      // Vertical Layout Group ������Ʈ�� ���� (Inspector���� �Ҵ�)
    public GameObject selectedUnitBarPrefab;    // Inspector���� �Ҵ�
    public Button selectButton;     // Inspector���� �Ҵ�
    public Button startButton;      // Inspector���� �Ҵ�

    // �̱��� �ν��Ͻ� ����
    private void Awake()
    {
        Debug.Log("UnitSelectionManager ������");

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

        // �÷��̾ ������ ������ ��� ������ ����
        units = UnitManager.Instance.GetUnits(GameManager.Instance.playerFaction);

        // ���� ������ ���� ������ ���
        for (int i = 0; i < unitNameTexts.Count; i++)
        {
            unitNameTexts[i].text = units[i].basicStats.unitName;
        }

        // �̺�Ʈ ������ ���
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

        // ù ��° ������ ������ ȭ�鿡 ǥ���� ���·� ����
        OnUnitButtonClicked(units[0]);
    }

    // �ڷ� ����
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

        // CenterPanel ��ܿ� ���� ������ ���� �̸��� ǥ��
        currentUnitNameText.text = unit.basicStats.unitName;
    }

    void OnSelectButtonClicked()
    {
        if (selectedUnitCodes.Count < 5)
        {
            if (!selectedUnitCodes.Contains(currentUnitCode))
            {
                // ������ ����
                selectedUnitCodes.Add(currentUnitCode);
                selectedUnitCodes.Sort();

                // �ð������� ǥ��
                GameObject unitBar = Instantiate(selectedUnitBarPrefab);
                unitBar.transform.SetParent(content.transform);
                unitBar.GetComponent<ButtonIndexer>().buttonIndex = currentUnitCode;    // buttonIndex�� unitCode�� ���� ����
                selectedUnitBars.Add(unitBar);

                Unit unit = UnitManager.Instance.GetUnit(currentUnitCode);
                unitBar.GetComponentInChildren<Text>().text = unit.basicStats.unitName;

                // �̺�Ʈ ������ ���
                Button deleteButton = unitBar.GetComponentInChildren<Button>();
                int capturedIndex = currentUnitCode;
                deleteButton.onClick.AddListener(() => OnDeleteButtonClicked(capturedIndex));

                // ����
                SortUnitBarsByButtonIndex();
            }
            else
            {
                Debug.Log("���õ� �����Դϴ�.");
            }
        }
        else
        {
            Debug.Log("�̹� 5���Դϴ�.");
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
            Debug.Log("5���� ������ �����ؾ� �մϴ�.");
        }
    }

    void OnDeleteButtonClicked(int unitCode)
    {
        // ������ ����
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

    // selectedUnitBar�� �����ϴ� �Լ�(unitCode�� ��������)
    void SortUnitBarsByButtonIndex()
    {
        // �ڽ� ������Ʈ���� ����Ʈ�� �ҷ�����
        List<Transform> children = new List<Transform>();

        foreach (Transform child in content.transform)
            children.Add(child);

        // buttonIndex�� �������� ����
        children.Sort((a, b) => a.GetComponent<ButtonIndexer>().buttonIndex.CompareTo(b.GetComponent<ButtonIndexer>().buttonIndex));

        // ���ĵ� ������ �ٽ� �θ� ��ġ
        for (int i = 0; i < children.Count; i++)
            children[i].SetSiblingIndex(i);
    }
}
