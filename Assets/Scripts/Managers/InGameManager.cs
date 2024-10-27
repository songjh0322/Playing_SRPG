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
        Move
    }

    public static InGameManager Instance { get; private set; }

    // ���� ����
    public bool isPlayerTurn;
    public State state;
    public TileInfo firstTileInfo;  // ù ��° Ÿ�� ���� (�Ʊ��� �ִ� ��츸)
    public TileInfo lastTileInfo;   // ��� Ÿ�� ����

    // Inspector���� �Ҵ�
    public GameObject unitBehaviourButtons;
    public Button attackButton;
    public Button moveButton;
    public Button cancelButton;

    // ���� ���࿡ �ʿ��� ������ (�ڵ����� �ε�)
    //public List

    private void Awake()
    {
        GameManager.Instance.gameState = GameState.InGame;
        // Debug.Log("InGameManager ������");

        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        //Debug.Log("InGameManager�� Start() ȣ���");

        // Player1���� ����
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

    public void OnTileClicked(TileInfo targetTileInfo)
    {
        if (isPlayerTurn)
        {
            if (state == State.NotSelected)
            {
                // �Ʊ��� �ִ� Ÿ�� Ŭ�� �� -> �ൿ ���� ��ư ǥ��
                if (targetTileInfo.unit != null && targetTileInfo.unit.team == Team.Ally)
                {
                    firstTileInfo = targetTileInfo;

                    state = State.BehaviourButtonsOn;
                    Vector3 currentMousePosition = Input.mousePosition;
                    unitBehaviourButtons.SetActive(true);
                    RectTransform rt = unitBehaviourButtons.GetComponent<RectTransform>();
                    rt.position = Camera.main.WorldToScreenPoint(targetTileInfo.worldXY + new Vector3(2.0f, -1.0f, 0.0f));
                }
                // �׿� ���
                else
                    OnCancelButtonClicked();
            }
            else if (state == State.Attack)
            {
                lastTileInfo = targetTileInfo;
                List<TileInfo> inRangeTiles = MapManager.Instance.GetManhattanTileInfos(firstTileInfo, firstTileInfo.unit.currentAttackRange);
                OnCancelButtonClicked();
            }
            else if (state == State.Move)
            {
                lastTileInfo = targetTileInfo;
                List<TileInfo> inRangeTiles = MapManager.Instance.GetManhattanTileInfos(firstTileInfo, firstTileInfo.unit.currentMoveRange);

                // �̵� ���� ���̸鼭 ������ ���� ��� -> �̵�
                if (inRangeTiles.Contains(lastTileInfo) && lastTileInfo.unit == null)
                {
                    firstTileInfo.unitPrefab.transform.position = lastTileInfo.worldXY;

                    lastTileInfo.unit = firstTileInfo.unit;
                    lastTileInfo.unitPrefab = firstTileInfo.unitPrefab;

                    firstTileInfo.unit = null;
                    firstTileInfo.unitPrefab = null;
                }
                OnCancelButtonClicked();
            }
            else if (state == State.BehaviourButtonsOn)
                OnCancelButtonClicked();
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
        firstTileInfo = null;
        lastTileInfo = null;
        unitBehaviourButtons.SetActive(false);
        state = State.NotSelected;
    }

    private void EndTurn()
    {
        isPlayerTurn = !isPlayerTurn;
    }
}
