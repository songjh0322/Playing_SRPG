using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public enum State
{
    NotSelected,
    Selected,
    Done,   // ��� ��ġ�� ����
}

public class DeployManager : MonoBehaviour
{
    public static DeployManager Instance { get; private set; }

    // ���� ����
    public State state;     // ���� ����
    public int currentUnitCode;     // ���� ��ġ�Ϸ��� ������ �ڵ�
    public GameObject currentUnitPrefab;    // ���� ��ġ�Ϸ��� ������ ������

    public List<Button> playerUnitButtons;
    public List<Text> unitNameTexts;
    public Tilemap tilemap;

    private void Awake()
    {
        Debug.Log("DeployManager ������");

        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        state = State.NotSelected;
        currentUnitCode = -1;

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
    }

    // �÷��̾��� Ű���� �Է°� �ǽð� ������Ʈ�� �ʿ��� �ڵ常 ���⼭ ó��
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && state == State.Selected)
        {
            Destroy(currentUnitPrefab);
            state = State.NotSelected;
            currentUnitCode = -1;
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
        // ���� ��ư�� Ŭ���ϸ� ���� ����
        if (currentUnitCode == unitCode)
            return;
        // �ٸ� ��ư�� Ŭ���ϸ� �ٸ� �������� ��ü
        if (currentUnitCode != unitCode)
        {
            Destroy(currentUnitPrefab);
            state = State.Selected;
            currentUnitCode = unitCode;
            currentUnitPrefab = Instantiate(UnitPrefabManager.Instance.GetUnitPrefab(unitCode));
        }
    }
}
