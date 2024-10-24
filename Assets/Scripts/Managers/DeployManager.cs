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
    Done,   // 모두 배치된 상태
}

public class DeployManager : MonoBehaviour
{
    public static DeployManager Instance { get; private set; }

    // 상태 관리
    public State state;     // 현재 상태
    public int currentUnitCode;     // 현재 배치하려는 유닛의 코드
    public GameObject currentUnitPrefab;    // 현재 배치하려는 유닛의 프리팹

    public List<Button> playerUnitButtons;
    public List<Text> unitNameTexts;
    public Tilemap tilemap;

    private void Awake()
    {
        Debug.Log("DeployManager 생성됨");

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

        // 이벤트 리스너 등록
        for (int i = 0; i < playerUnitButtons.Count; i++)
        {
            int index = i;
            playerUnitButtons[index].onClick.AddListener(() => OnPlayerUnitButtonClicked(UnitManager.Instance.player1UnitCodes[index]));
        }
    }

    // 플레이어의 키보드 입력과 실시간 업데이트가 필요한 코드만 여기서 처리
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
        // 같은 버튼을 클릭하면 상태 유지
        if (currentUnitCode == unitCode)
            return;
        // 다른 버튼을 클릭하면 다른 유닛으로 대체
        if (currentUnitCode != unitCode)
        {
            Destroy(currentUnitPrefab);
            state = State.Selected;
            currentUnitCode = unitCode;
            currentUnitPrefab = Instantiate(UnitPrefabManager.Instance.GetUnitPrefab(unitCode));
        }
    }
}
