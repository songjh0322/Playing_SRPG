using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TileEnums;

public class InitialDeployManager : MonoBehaviour
{
    public enum State
    {
        NotSelected,
        Selected,
    }

    public static InitialDeployManager Instance { get; private set; }

    // 상태 관리
    public State state;     // 현재 상태
    public int currentUnitCode;     // 현재 배치하려는 유닛의 코드
    GameObject currentHoveredTile;
    public GameObject currentUnitPrefab;    // 현재 배치하려는 유닛의 프리팹
    public List<int> deployedUnitsCodes;    // 현재 배치되어있는 유닛의 코드 리스트
    public List<GameObject> playerUnitPrefabs;

    // 프리팹 관리 (Inspector에서 할당)
    public List<Button> playerUnitButtons;
    public List<Text> unitNameTexts;
    public Button completeButton;
    public Button playerResetButton;
    public Button aiRandomDeployButton;
    public GameObject inGameManager;
    public GameObject ActiveUnits;  // 생성된 유닛 프리팹들을 묶을 빈 게임 오브젝트(아군 및 적군)

    private void Awake()
    {
        GameManager.Instance.gameState = GameState.InitialDeployment;
        // Debug.Log("InitialDeployManager 생성됨");

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
        playerUnitPrefabs = new List<GameObject>();

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

        completeButton.onClick.AddListener(() => OnCompleteButtonClicked());
        playerResetButton.onClick.AddListener(() => OnPlayerResetButtonClicked());
        aiRandomDeployButton.onClick.AddListener(() => OnAIRandomDeployButtonClicked());
    }

    // 플레이어의 키보드 입력과 실시간 업데이트가 필요한 코드만 여기서 처리
    void Update()
    {
        // 키보드 입력
        if (Input.GetKeyDown(KeyCode.Escape) && state == State.Selected)
        {
            CancelUnitSelection();
        }
        if (state == State.Selected)
        {
            if (MapManager.Instance.currentHoveredTile != null)
                currentUnitPrefab.transform.position = MapManager.Instance.currentHoveredTile.GetComponent<TileInfo>().worldXY;
            else
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0f;

                currentUnitPrefab.transform.position = mousePosition;
            }
        }

        // 상시 업데이트 요소
        HightlightHoveredTile();
    }

    private void OnPlayerUnitButtonClicked(int unitCode)
    {
        // 이미 배치된 유닛인지 확인
        if (deployedUnitsCodes.Contains(unitCode))
            return;
        
        // 같은 버튼을 클릭하면 선택 해제
        if (currentUnitCode == unitCode)
        {
            CancelUnitSelection();
            return;
        }

        // 다른 버튼을 클릭하면 다른 유닛으로 대체
        if (currentUnitCode != unitCode)
        {
            // 기존 프리팹 생성 방식
            Destroy(currentUnitPrefab);
            state = State.Selected;
            currentUnitCode = unitCode;
            currentUnitPrefab = UnitPrefabManager.Instance.InstantiateUnitPrefab(unitCode, 2.0f, true);

            state = State.Selected;
            currentUnitCode = unitCode;
        }
    }

    private void OnCompleteButtonClicked()
    {
        // 모두 배치한 경우에 한해 완료 버튼 클릭 가능
        if (deployedUnitsCodes.Count == UnitManager.Instance.player1UnitCodes.Count && AIManager.Instance.isAllDeployed)
        {
            // 배치에서 사용되는 기존의 모든 버튼을 비활성화
            for (int i = 0; i < deployedUnitsCodes.Count; i++)
                playerUnitButtons[i].gameObject.SetActive(false);
            completeButton.gameObject.SetActive(false);
            playerResetButton.gameObject.SetActive(false);
            aiRandomDeployButton.gameObject.SetActive(false);

            inGameManager.SetActive(true);

            // InitialDeployManager에서 InGameManager로 필요한 정보 전달
            //

            gameObject.SetActive(false);
        }
    }

    private void OnPlayerResetButtonClicked()
    {
        Reset();
    }

    private void OnAIRandomDeployButtonClicked()
    {
        AIManager.Instance.RandomDeploy();
    }

    public void OnTileClicked(TileInfo tileInfo)
    {
        if (state == State.Selected
            && tileInfo.unit == null
            && tileInfo.initialDeployment == InitialDeployment.Player1)
        {
            // 해당 TileInfo 업데이트
            tileInfo.unit = UnitManager.Instance.GetPlayer1Unit(currentUnitCode);
            // 유닛을 시각적으로 배치
            currentUnitPrefab.transform.position = tileInfo.worldXY;
            tileInfo.unitPrefab = currentUnitPrefab;    // 오류 나면 여기 볼것!!!!!!!!!!!
            currentUnitPrefab.transform.SetParent(ActiveUnits.transform);
            playerUnitPrefabs.Add(currentUnitPrefab);

            // 상태 설정 및 초기화
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

    private void Reset()
    {
        deployedUnitsCodes.Clear();
        // TileInfo 업데이트
        List<TileInfo> deployableTileInfos = MapManager.Instance.GetTileInfos(InitialDeployment.Player1);
        for (int i = 0; i < deployableTileInfos.Count; i++)
            deployableTileInfos[i].unit = null;
        foreach (GameObject playerUnitPrefab in playerUnitPrefabs)
            Destroy(playerUnitPrefab);
    }

    // (GPT) 배치 가능한 타일만 초록색, 그 외에는 빨간색
    private void HightlightHoveredTile()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // 마우스 위치에서 레이캐스트 수행
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 0f, LayerMask.GetMask("Map"));

        // 현재 마우스가 가리키고 있는 타일
        GameObject hoveredTile = hit.collider != null ? hit.collider.gameObject : null;

        // 현재 타일이 이전 타일과 다른 경우에만 처리
        if (hoveredTile != currentHoveredTile)
        {
            // 이전 타일의 색상을 흰색으로 되돌림
            if (currentHoveredTile != null)
            {
                SpriteRenderer currentSR = currentHoveredTile.GetComponent<SpriteRenderer>();
                if (currentSR != null)
                {
                    currentSR.color = Color.white; // 흰색으로 되돌림
                }
            }

            if (hoveredTile != null)
            {
                SpriteRenderer newSR = hoveredTile.GetComponent<SpriteRenderer>();
                if (newSR != null)
                {
                    TileInfo tileInfo = hoveredTile.GetComponent<TileInfo>();
                    if (tileInfo.initialDeployment == InitialDeployment.Player1 && tileInfo.unit == null)
                        newSR.color = Color.green;
                    else
                        newSR.color = Color.red;
                }
            }

            // 현재 마우스가 가리키고 있는 타일 업데이트
            currentHoveredTile = hoveredTile;
        }
    }
}
