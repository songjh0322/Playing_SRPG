using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using static TileEnums;

public enum State
{
    NotSelected,
    Selected,
    Done,   // ��� ��ġ�� ����
}

public class InitialDeployManager : MonoBehaviour
{
    public static InitialDeployManager Instance { get; private set; }

    // ���� ����
    public State state;     // ���� ����
    public int currentUnitCode;     // ���� ��ġ�Ϸ��� ������ �ڵ�
    public GameObject currentUnitPrefab;    // ���� ��ġ�Ϸ��� ������ ������
    public List<int> deployedUnitsCodes;    // ���� ��ġ�Ǿ��ִ� ������ �ڵ� ����Ʈ

    // ������ ���� (Inspector���� �Ҵ�)
    public List<Button> playerUnitButtons;
    public List<Text> unitNameTexts;
    public Button completeButton;
    public GameObject map;

    private void Awake()
    {
        GameManager.Instance.gameState = GameState.InitialDeployment;
        // Debug.Log("InitialDeployManager ������");

        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        state = State.NotSelected;
        currentUnitCode = -1;
        currentUnitPrefab = null;
        deployedUnitsCodes = new List<int>();

        for (int i = 0; i < unitNameTexts.Count; i++)
        {
            unitNameTexts[i].text = UnitManager.Instance.player1Units[i].basicStats.unitName;
        }

        // �̺�Ʈ ������ ���
        for (int i = 0; i < playerUnitButtons.Count; i++)
        {
            int index = i;
            playerUnitButtons[index].onClick.AddListener(() => OnPlayerUnitButtonClicked(UnitManager.Instance.player1UnitCodes[index]));
        }

        //completeButton.onClick.AddListener(() => OnCompleteButtonClicked());
    }

    // �÷��̾��� Ű���� �Է°� �ǽð� ������Ʈ�� �ʿ��� �ڵ常 ���⼭ ó��
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && state == State.Selected)
        {
            CancelUnitSelection();
        }
        if (state == State.Selected)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            currentUnitPrefab.transform.position = mousePosition;
        }
    }

    private void OnPlayerUnitButtonClicked(int unitCode)
    {
        // �̹� ��ġ�� �������� Ȯ��
        if (deployedUnitsCodes.Contains(unitCode))
            return;
        
        // ���� ��ư�� Ŭ���ϸ� ���� ����
        if (currentUnitCode == unitCode)
        {
            CancelUnitSelection();
            return;
        }
        // �ٸ� ��ư�� Ŭ���ϸ� �ٸ� �������� ��ü
        if (currentUnitCode != unitCode)
        {
            Destroy(currentUnitPrefab);
            state = State.Selected;
            currentUnitCode = unitCode;
            currentUnitPrefab = UnitPrefabManager.Instance.InstantiateUnitPrefab(unitCode, 2.0f);
        }
    }

    private void OnCompleteButtonClicked()
    {

    }

    public void OnTileClicked(TileInfo tileInfo)
    {
        if (state == State.Selected
            && tileInfo.unit == null
            && tileInfo.initialDeployment == InitialDeployment.Player1)
        {
            // ������ �ð������� ��ġ
            currentUnitPrefab.transform.position = tileInfo.worldXY;
            // �ش� TileInfo ������Ʈ
            tileInfo.unit = UnitManager.Instance.GetPlayer1Unit(currentUnitCode);

            // ���� ���� �� �ʱ�ȭ
            deployedUnitsCodes.Add(currentUnitCode);
            deployedUnitsCodes.Sort();
            state = State.NotSelected;
            currentUnitCode = -1;
            currentUnitPrefab = null; 
        }
    }

    private void CancelUnitSelection()
    {
        Destroy(currentUnitPrefab);
        state = State.NotSelected;
        currentUnitCode = -1;
        currentUnitPrefab = null;
    }
}
