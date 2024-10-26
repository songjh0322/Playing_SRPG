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

    // ���� ����
    public bool isPlayerTurn;
    public State state;
    public TileInfo currentTileInfo;
    public GameObject currentUnitPrefab;

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

    public void OnTileClicked(TileInfo tileInfo)
    {
        // Ŭ���� Ÿ�� ������ ������
        currentTileInfo = tileInfo;
        currentUnitPrefab = currentTileInfo.unitPrefab;

        if (isPlayerTurn)
        {
            // �Ʊ��� �ִ� Ÿ�� Ŭ�� �� -> �ൿ ���� ��ư ǥ��
            if (tileInfo.unit != null && tileInfo.unit.team == Team.Ally)
            {
                state = State.BehaviourButtonsOn;

                Vector3 currentMousePosition = Input.mousePosition;

                unitBehaviourButtons.SetActive(true);
                RectTransform rt = unitBehaviourButtons.GetComponent<RectTransform>();
                rt.position = Camera.main.WorldToScreenPoint(tileInfo.worldXY + new Vector3(2.0f, -1.0f, 0.0f));
            }
            // �׿� ���
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
