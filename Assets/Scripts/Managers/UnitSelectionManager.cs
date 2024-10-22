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
    private List<int> selectedUnitCodes;
    private List<Unit> units;   // ���� ������ ������ ���ֵ� (8��)
    private List<Unit> selectedUnits;

    // LeftPanel
    public List<Button> unitButtons;
    public List<Text> unitNameTexts;

    // CenterPanel
    public Text currentUnitNameText;
    public Button idleButton;
    public Button moveButton;
    public Button attackButton;
    public Button damagedButton;
    public Button diedButton;
    public Button DebuffedButton;

    // RightPanel
    public List<GameObject> selectedUnitBars;
    public GameObject content;  // ���⿡ Vertical Layout Group ������Ʈ ����
    public GameObject selectedUnitBarPrefab;
    public Button selectButton;
    public Button startButton;

    // �̱��� �ν��Ͻ� ����
    private void Awake()
    {
        Debug.Log("UnitSelectionScene �ε�");

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
    }

    void OnUnitButtonClicked(Unit unit)
    {
        currentUnitCode = unit.basicStats.unitCode;

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
                UnitManager.Instance.player1Units.Add(new(UnitManager.Instance.GetUnit(unitCode)));

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