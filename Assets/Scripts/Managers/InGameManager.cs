using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{
    public static InGameManager Instance { get; private set; }

    // 상태 관리
    public bool isPlayerTurn;
    public GameObject currentHoveredTile;
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

        attackButton.onClick.AddListener(OnAttackButtonClicked);
        moveButton.onClick.AddListener(OnMoveButtonClicked);
        cancelButton.onClick.AddListener(OnCancelButtonClicked);
    }

    void Update()
    {
        if (isPlayerTurn)
            HightlightHoveredTile();
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
        Unit unit = currentTileInfo.unit;
        unitBehaviourButtons.SetActive(false);

        //DisplayRange(unit.current);
    }

    void OnMoveButtonClicked()
    {
        Unit unit = currentTileInfo.unit;
        unitBehaviourButtons.SetActive(false);

        //DisplayRange(unit.currentMoveRange);
    }

    void OnCancelButtonClicked()
    {
        unitBehaviourButtons.SetActive(false);
    }

    private void EndTurn()
    {
        isPlayerTurn = !isPlayerTurn;
    }

    // GPT
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

            // 새로운 타일의 색상을 초록색으로 변경
            if (hoveredTile != null)
            {
                SpriteRenderer newSR = hoveredTile.GetComponent<SpriteRenderer>();
                if (newSR != null)
                {
                    TileInfo tileInfo = hoveredTile.GetComponent<TileInfo>();
                    if (tileInfo.unit == null)
                        newSR.color = Color.gray;
                    else if (tileInfo.unit.team == Team.Ally)
                        newSR.color = Color.green;
                    else if (tileInfo.unit.team == Team.Enemy)
                        newSR.color = Color.red;
                }
            }

            // 현재 마우스가 가리키고 있는 타일 업데이트
            currentHoveredTile = hoveredTile;
        }
    }
}
