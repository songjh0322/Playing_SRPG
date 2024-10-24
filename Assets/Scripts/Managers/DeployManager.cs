using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;

public enum State
{
    NotSelected,
    Selected,
    Done,   // ��� ��ġ�� ����
}

public class DeployManager : MonoBehaviour
{
    // ���� ����
    public State state;
    public GameObject currentUnitPrefab;

    public List<Button> playerUnitButtons;
    public List<Text> unitNameTexts;

    public static DeployManager Instance { get; private set; }

    private void Awake()
    {
        Debug.Log("DeployScene Scene");

        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        state = State.NotSelected;

        for (int i = 0; i < unitNameTexts.Count; i++)
        {
            unitNameTexts[i].text = UnitManager.Instance.player1Units[i].basicStats.unitName;
        }

        // �̺�Ʈ ������ ���
        for (int i = 0; i < playerUnitButtons.Count; i++)
        {
            int index = i;
            playerUnitButtons[index].onClick.AddListener(() => OnPlayerUnitButtonClicked(index));
        }
    }

    void Update()
    {
        if (state == State.Selected)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            currentUnitPrefab.transform.position = mousePosition;
        }
    }

    private void OnPlayerUnitButtonClicked(int unitCode)
    {
        Debug.Log(unitCode);
        state = State.Selected;
        currentUnitPrefab = Instantiate(UnitPrefabManager.Instance.GetUnitPrefab(unitCode));
    }
}
