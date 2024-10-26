using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{
    public enum State
    {
        NotSelected,
        BehaviourButtonsOn,
        Selected
    }

    public static InGameManager Instance { get; private set; }

    // 상태 관리
    public bool isPlayerTurn;
    public State state;
    public TileInfo currentTileInfo;
    public GameObject currentUnitPrefab;

    // Inspector에서 할당
    public GameObject unitBehaviourButtons;
    public Button attackButton;
    public Button moveButton;
    public Button cancelButton;

    // 게임 진행에 필요한 데이터 (자동으로 로드)
    //public List

    private void Awake()
    {
        GameManager.Instance.gameState = GameState.InGame;
        // Debug.Log("InGameManager 생성됨");

        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        //Debug.Log("InGameManager의 Start() 호출됨");

        // Player1부터 시작
        isPlayerTurn = true;
        state = State.NotSelected;

        attackButton.onClick.AddListener(OnAttackButtonClicked);
        moveButton.onClick.AddListener(OnMoveButtonClicked);
        cancelButton.onClick.AddListener(OnCancelButtonClicked);
    }

    void Update()
    {
        
    }

    public void OnUnitClicked()
    {

    }

    public void OnTileClicked(TileInfo tileInfo)
    {
        // 클릭한 타일 정보를 가져옴
        currentTileInfo = tileInfo;
        currentUnitPrefab = currentTileInfo.unitPrefab;

        if (isPlayerTurn)
        {
            // 아군이 있는 타일 클릭 시 -> 행동 선택 버튼 표시
            if (tileInfo.unit != null && tileInfo.unit.team == Team.Ally)
            {
                state = State.BehaviourButtonsOn;

                Vector3 currentMousePosition = Input.mousePosition;

                unitBehaviourButtons.SetActive(true);
                RectTransform rt = unitBehaviourButtons.GetComponent<RectTransform>();
                rt.position = Camera.main.WorldToScreenPoint(tileInfo.worldXY + new Vector3(2.0f, -1.0f, 0.0f));
            }
            // 그외 경우
            else
                OnCancelButtonClicked();
        }
        
    }

    void OnAttackButtonClicked()
    {
        state = State.Selected;

        Unit unit = currentTileInfo.unit;
        unitBehaviourButtons.SetActive(false);

        List<GameObject> targetTiles = MapManager.Instance.GetManhattanTiles(currentTileInfo, unit.currentAttackRange);
        MapManager.Instance.HighlightTiles(targetTiles, Color.red);
    }

    void OnMoveButtonClicked()
    {
        state = State.Selected;

        Unit unit = currentTileInfo.unit;
        unitBehaviourButtons.SetActive(false);

        //DisplayRange(unit.currentMoveRange);
    }

    void OnCancelButtonClicked()
    {
        unitBehaviourButtons.SetActive(false);
        state = State.NotSelected;
    }

    private void EndTurn()
    {
        isPlayerTurn = !isPlayerTurn;
    }
}
