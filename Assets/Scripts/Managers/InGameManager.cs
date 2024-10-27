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
        Attack,
        Move,
        GameEnd
    }

    public static InGameManager Instance { get; private set; }

    // 상태 관리
    public bool isPlayerTurn;
    public State state;
    public TileInfo firstTileInfo;  // 첫 번째 타일 정보 (아군이 있는 경우에만)
    public TileInfo lastTileInfo;   // 대상 타일 정보

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

    // 현재 타일을 기준으로 마우스 포인터 및 클릭을 감지함 -> 미사용
    public void OnUnitClicked()
    {

    }

    public void OnTileClicked(TileInfo targetTileInfo)
    {
        if (isPlayerTurn)
        {
            if (state == State.NotSelected)
            {
                // 아군이 있는 타일 클릭 시 -> 행동 선택 버튼 표시
                if (targetTileInfo.unit != null && targetTileInfo.unit.team == Team.Ally)
                {
                    firstTileInfo = targetTileInfo;

                    state = State.BehaviourButtonsOn;
                    Vector3 currentMousePosition = Input.mousePosition;
                    unitBehaviourButtons.SetActive(true);
                    RectTransform rt = unitBehaviourButtons.GetComponent<RectTransform>();
                    rt.position = Camera.main.WorldToScreenPoint(targetTileInfo.worldXY + new Vector3(2.0f, -1.0f, 0.0f));
                }
                // 그외 경우
                else
                    InitStates();
            }
            else if (state == State.Attack)
            {
                lastTileInfo = targetTileInfo;
                List<TileInfo> inRangeTiles = MapManager.Instance.GetManhattanTileInfos(firstTileInfo, firstTileInfo.unit.currentAttackRange);

                // 공격 범위 내이면서 적 유닛이 있는 경우 -> 공격
                if (inRangeTiles.Contains(lastTileInfo) && lastTileInfo.unit != null && lastTileInfo.unit.team == Team.Enemy)
                {
                    // 공격을 수행하는 동안 변하지 않으므로 복사해서 사용
                    int attackPoint = firstTileInfo.unit.currentAttackPoint;
                    int defensePoint = lastTileInfo.unit.currentDefensePoint;
                    int realDamage = Mathf.Max(attackPoint - defensePoint, 0);

                    lastTileInfo.unit.currentHealth -= realDamage;

                    // 이 코드는 오직 공격으로만 적이 처지된다고 가정한 코드임 !!!!
                    if (lastTileInfo.unit.currentHealth <= 0)
                    {
                        Destroy(lastTileInfo.unitPrefab);
                        lastTileInfo.unit = null;
                        lastTileInfo.unitPrefab = null;
                        if (lastTileInfo.unitPrefab == null) { Debug.Log("프리팹이 null임"); }
                    }

                    EndTurn();
                }
                InitStates();
            }
            else if (state == State.Move)
            {
                lastTileInfo = targetTileInfo;
                List<TileInfo> inRangeTiles = MapManager.Instance.GetManhattanTileInfos(firstTileInfo, firstTileInfo.unit.currentMoveRange);

                // 이동 범위 내이면서 유닛이 없는 경우 -> 이동
                if (inRangeTiles.Contains(lastTileInfo) && lastTileInfo.unit == null)
                {
                    firstTileInfo.unitPrefab.transform.position = lastTileInfo.worldXY;

                    lastTileInfo.unit = firstTileInfo.unit;
                    lastTileInfo.unitPrefab = firstTileInfo.unitPrefab;

                    firstTileInfo.unit = null;
                    firstTileInfo.unitPrefab = null;

                    EndTurn();
                }
                InitStates();
            }
            else // (state == State.BehaviourButtonsOn)
                InitStates();
        }
    }

    void OnAttackButtonClicked()
    {
        state = State.Attack;

        unitBehaviourButtons.SetActive(false);
    }

    void OnMoveButtonClicked()
    {
        state = State.Move;

        unitBehaviourButtons.SetActive(false);

    }

    void OnCancelButtonClicked()
    {
        InitStates();
    }

    private void EndTurn()
    {
        InitStates();
        isPlayerTurn = !isPlayerTurn;
        AIManager.Instance.OnAITurnStarted();
    }

    // 유닛을 선택하지 않은 상태로 되돌림
    private void InitStates()
    {
        firstTileInfo = null;
        lastTileInfo = null;
        unitBehaviourButtons.SetActive(false);
        state = State.NotSelected;
    }
}
