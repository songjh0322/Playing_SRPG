using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{
    public static InGameManager Instance { get; private set; }

    // ���� ����
    public bool isPlayerTurn;
    public GameObject currentHoveredTile;
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
        // Ŭ���� Ÿ�� ������ ������
        currentTileInfo = tileInfo;
        currentUnitPrefab = currentTileInfo.unitPrefab;

        if (isPlayerTurn)
        {
            // �Ʊ��� �ִ� Ÿ�� Ŭ�� �� -> �ൿ ���� ��ư ǥ��
            if (tileInfo.unit != null && tileInfo.unit.team == Team.Ally)
            {
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

        // ���콺 ��ġ���� ����ĳ��Ʈ ����
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 0f, LayerMask.GetMask("Map"));

        // ���� ���콺�� ����Ű�� �ִ� Ÿ��
        GameObject hoveredTile = hit.collider != null ? hit.collider.gameObject : null;

        // ���� Ÿ���� ���� Ÿ�ϰ� �ٸ� ��쿡�� ó��
        if (hoveredTile != currentHoveredTile)
        {
            // ���� Ÿ���� ������ ������� �ǵ���
            if (currentHoveredTile != null)
            {
                SpriteRenderer currentSR = currentHoveredTile.GetComponent<SpriteRenderer>();
                if (currentSR != null)
                {
                    currentSR.color = Color.white; // ������� �ǵ���
                }
            }

            // ���ο� Ÿ���� ������ �ʷϻ����� ����
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

            // ���� ���콺�� ����Ű�� �ִ� Ÿ�� ������Ʈ
            currentHoveredTile = hoveredTile;
        }
    }
}
