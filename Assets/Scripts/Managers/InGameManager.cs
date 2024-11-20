using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{
    public enum State
    {
        NotSelected,
        BehaviourButtonsOn,
        Attack,
        Move
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
    public GameObject turnText;
    public GameObject gameResultPopup;
    public GameObject gameResultText;

    // 게임 진행에 필요한 데이터
    public int playerDeathCount;
    public int aiDeathCount;

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
        playerDeathCount = 0;
        aiDeathCount = 0;

        turnText.SetActive(true);

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
                    Animator first_animator = firstTileInfo.unitPrefab.GetComponentInChildren<Animator>();
                    Animator last_animator = lastTileInfo.unitPrefab.GetComponentInChildren<Animator>();
                    if (first_animator != null)
                    {
                        first_animator.SetTrigger("2_Attack");
                    }
                    if (last_animator != null)
                    {
                        last_animator.SetTrigger("3_Damaged");
                        lastTileInfo.unit.currentHealth -= realDamage;
                    }

                    // 이 코드는 오직 공격으로만 적이 처지된다고 가정한 코드임 !!!!
                    if (lastTileInfo.unit.currentHealth <= 0)
                    {
                        last_animator.SetTrigger("4_Death");

                        StartCoroutine(DestroyAfterAnimation(lastTileInfo.unitPrefab, lastTileInfo));

                        aiDeathCount++;
                        if (aiDeathCount == 5)
                            GameEnd();
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

                    if (firstTileInfo != null && firstTileInfo.unitPrefab != null)
                    {
                        Animator animator = firstTileInfo.unitPrefab.GetComponentInChildren<Animator>();
                        if (animator != null)
                        {
                            animator.SetBool("1_Move", true);
                            StartCoroutine(ResetMoveAnimation(animator));
                        }
                    }
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
    private IEnumerator DestroyAfterAnimation(GameObject unitPrefab, TileInfo tileInfo)
    {
        // 애니메이션의 길이를 Animator에서 가져옴
        Animator animator = unitPrefab.GetComponentInChildren<Animator>();
        if (animator != null)
        {
            // 현재 실행 중인 애니메이션 상태 정보를 가져옴
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            // 애니메이션의 길이만큼 대기
            yield return new WaitForSeconds(1.5f);
        }

        // 오브젝트 삭제 및 타일 초기화
        Destroy(unitPrefab);
        tileInfo.unit = null;
        tileInfo.unitPrefab = null;
    }
    private IEnumerator ResetMoveAnimation(Animator animator)
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("1_Move", false);
    }

    void OnAttackButtonClicked()
    {
        state = State.Attack;

        unitBehaviourButtons.SetActive(false);
        if (firstTileInfo != null && firstTileInfo.unitPrefab != null)
        {

        }
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

    // private void EndTurn()
    // {
    //     InitStates();
    //     isPlayerTurn = !isPlayerTurn;
    //     turnText.GetComponent<TMP_Text>().text = "AI's Turn";
    //     // 여기서 2초 대기
    //     AIManager.Instance.OnAITurnStarted();
    //     isPlayerTurn = true;
    //     turnText.GetComponent<TMP_Text>().text = "Player's Turn";
    // }

    public void EndTurn()
    {
        StartCoroutine(EndTurnCoroutine());
    }

    private IEnumerator EndTurnCoroutine()
    {
        InitStates();
        isPlayerTurn = !isPlayerTurn;

        // AI의 턴 텍스트로 변경
        turnText.GetComponent<TMP_Text>().text = "AI's Turn";

        // 2초 대기
        yield return new WaitForSeconds(2f);

        // AI 행동 수행
        AIManager.Instance.OnAITurnStarted();

        // 플레이어 턴으로 전환
        isPlayerTurn = true;
        turnText.GetComponent<TMP_Text>().text = "Player's Turn";
    }

    // 유닛을 선택하지 않은 상태로 되돌림
    private void InitStates()
    {
        firstTileInfo = null;
        lastTileInfo = null;
        unitBehaviourButtons.SetActive(false);
        state = State.NotSelected;
    }

    // 게임 종료
    public void GameEnd()
    {
        turnText.SetActive(false);
        gameResultPopup.SetActive(true);

        if (aiDeathCount == 5)
            gameResultText.GetComponent<Text>().text = "YOU WIN !";
        else
            gameResultText.GetComponent<Text>().text = "YOU LOSE !";
    }
}
