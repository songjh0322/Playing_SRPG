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

    // ���� ����
    public State state;     // ���� ����
    public int currentUnitCode;     // ���� ��ġ�Ϸ��� ������ �ڵ�
    GameObject currentHoveredTile;
    public GameObject currentUnitPrefab;    // ���� ��ġ�Ϸ��� ������ ������
    public List<int> deployedUnitsCodes;    // ���� ��ġ�Ǿ��ִ� ������ �ڵ� ����Ʈ
    public List<GameObject> playerUnitPrefabs;

    // ������ ���� (Inspector���� �Ҵ�)
    public List<Button> playerUnitButtons;
    public List<Text> unitNameTexts;
    public Button completeButton;
    public Button playerResetButton;
    public Button aiRandomDeployButton;
    public GameObject inGameManager;
    public GameObject ActiveUnits;  // ������ ���� �����յ��� ���� �� ���� ������Ʈ(�Ʊ� �� ����)

    private void Awake()
    {
        GameManager.Instance.gameState = GameState.InitialDeployment;
        // Debug.Log("InitialDeployManager ������");

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

        // �̺�Ʈ ������ ���
        for (int i = 0; i < playerUnitButtons.Count; i++)
        {
            int index = i;
            playerUnitButtons[index].onClick.AddListener(() => OnPlayerUnitButtonClicked(UnitManager.Instance.player1UnitCodes[index]));
        }

        completeButton.onClick.AddListener(() => OnCompleteButtonClicked());
        playerResetButton.onClick.AddListener(() => OnPlayerResetButtonClicked());
        aiRandomDeployButton.onClick.AddListener(() => OnAIRandomDeployButtonClicked());
    }

    // �÷��̾��� Ű���� �Է°� �ǽð� ������Ʈ�� �ʿ��� �ڵ常 ���⼭ ó��
    void Update()
    {
        // Ű���� �Է�
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

        // ��� ������Ʈ ���
        HightlightHoveredTile();
    }

    private void OnPlayerUnitButtonClicked(int unitCode)
    {
        // �̹� ��ġ�� �������� Ȯ��
        if (deployedUnitsCodes.Contains(unitCode))
            return;
        
        // ���� ��ư�� Ŭ���ϸ� ���� ����
        if (currentUnitCode == unitCode)
        {
            CancelUnitSelection();
            return;
        }

        // �ٸ� ��ư�� Ŭ���ϸ� �ٸ� �������� ��ü
        if (currentUnitCode != unitCode)
        {
            // ���� ������ ���� ���
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
        // ��� ��ġ�� ��쿡 ���� �Ϸ� ��ư Ŭ�� ����
        if (deployedUnitsCodes.Count == UnitManager.Instance.player1UnitCodes.Count && AIManager.Instance.isAllDeployed)
        {
            // ��ġ���� ���Ǵ� ������ ��� ��ư�� ��Ȱ��ȭ
            for (int i = 0; i < deployedUnitsCodes.Count; i++)
                playerUnitButtons[i].gameObject.SetActive(false);
            completeButton.gameObject.SetActive(false);
            playerResetButton.gameObject.SetActive(false);
            aiRandomDeployButton.gameObject.SetActive(false);

            inGameManager.SetActive(true);

            // InitialDeployManager���� InGameManager�� �ʿ��� ���� ����
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
            // �ش� TileInfo ������Ʈ
            tileInfo.unit = UnitManager.Instance.GetPlayer1Unit(currentUnitCode);
            // ������ �ð������� ��ġ
            currentUnitPrefab.transform.position = tileInfo.worldXY;
            tileInfo.unitPrefab = currentUnitPrefab;    // ���� ���� ���� ����!!!!!!!!!!!
            currentUnitPrefab.transform.SetParent(ActiveUnits.transform);
            playerUnitPrefabs.Add(currentUnitPrefab);

            // ���� ���� �� �ʱ�ȭ
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
        // TileInfo ������Ʈ
        List<TileInfo> deployableTileInfos = MapManager.Instance.GetTileInfos(InitialDeployment.Player1);
        for (int i = 0; i < deployableTileInfos.Count; i++)
            deployableTileInfos[i].unit = null;
        foreach (GameObject playerUnitPrefab in playerUnitPrefabs)
            Destroy(playerUnitPrefab);
    }

    // (GPT) ��ġ ������ Ÿ�ϸ� �ʷϻ�, �� �ܿ��� ������
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

            // ���� ���콺�� ����Ű�� �ִ� Ÿ�� ������Ʈ
            currentHoveredTile = hoveredTile;
        }
    }
}
