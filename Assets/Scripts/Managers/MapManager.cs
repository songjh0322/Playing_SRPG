using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TileEnums;

// �� ���� ���� �� ���� �ð��� ��ȭ(Ÿ�� ��)�� ���
public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    // �� ũ�� ����
    private int xLength, yLength;

    // ������ ���� (Inspector���� �Ҵ�, �� ������)
    public GameObject tilePrefab;
    public Sprite middleZone;

    // ���� �� ���� ����
    public List<GameObject> allTiles;   // ��� Tile ���� ������Ʈ
    public List<TileInfo> allTileInfos; // ��� TileInfo

    // ���콺 ������ ���� ����
    public GameObject lastHoveredTile;
    public GameObject currentHoveredTile;
    public TileInfo currentHoveredTileInfo;
    public List<TileInfo> lastTileInfos;    // ���� ǥ�ø� �ߴ� Ÿ���� ���� (�ʱ�ȭ�� ����)

    private void Awake()
    {
        // Debug.Log("MapManager ������");

        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        xLength = 10;
        yLength = 12;
        allTiles = new List<GameObject>();
        allTileInfos = new List<TileInfo>();

        CreateTestMap();
    }

    private void Update()
    {
        UpdateCurrentHoveredTile();

        // ��ġ ���� ��
        if (GameManager.Instance.gameState == GameState.InitialDeployment)
        {
            HightlightAllTlies(Color.white);
            DisplayRange();
        }
        // ��ġ ���� �ΰ����� ��
        else if (GameManager.Instance.gameState == GameState.InGame)
        {
            HightlightAllTlies(Color.white);
            if (InGameManager.Instance.state == InGameManager.State.NotSelected
            || InGameManager.Instance.state == InGameManager.State.BehaviourButtonsOn)
                DisplayRange();
        }

        // ����Ű�� Ÿ���� ����ɶ����� �ʱ�ȭ
/*        if (GameManager.Instance.gameState == GameState.InitialDeployment)
            HightlightAllTlies(Color.white);
        else if (GameManager.Instance.gameState == GameState.InGame)
            HightlightAllTlies(Color.white);

        if (GameManager.Instance.gameState == GameState.InitialDeployment)
            DisplayRange();
        else if (InGameManager.Instance.state == InGameManager.State.NotSelected
            || InGameManager.Instance.state == InGameManager.State.BehaviourButtonsOn)
            DisplayRange();*/

        HighlightCurrentHoveredTile(Color.gray);
    }

    // �� ������ ������ (10 by 12)
    public void CreateTestMap()
    {
        GameObject testMap = new GameObject("TestMap");
        // ȭ�� ���� ������ ��ġ�� ǥ��
        testMap.transform.position = new Vector3(-20, 0, 0);

        for (int y = 0; y < yLength; y++)
        {
            for (int x = 0; x < xLength; x++)
            {
                Vector3 position = new Vector3((x + y) * 2, y - x, 0);
                GameObject createdTile = Instantiate(tilePrefab, position, Quaternion.identity);
                createdTile.transform.SetParent(testMap.transform, false);
                TileInfo tileInfo = createdTile.GetComponent<TileInfo>();
                if (y < 5)
                    tileInfo.Initialize(x, y, TileType.Normal, InitialDeployment.Player1);
                else if (y < 7)
                {
                    tileInfo.Initialize(x, y, TileType.Normal, InitialDeployment.None);
                    SpriteRenderer sr = tileInfo.GetComponent<SpriteRenderer>();
                    sr.sprite = middleZone;
                }
                else
                    tileInfo.Initialize(x, y, TileType.Normal, InitialDeployment.Player2);

                allTiles.Add(createdTile);
                allTileInfos.Add(tileInfo);
            }
        }
    }

    // (GPT) �ʱ� ��ġ�� ������ Ÿ���� ����Ʈ�� ȹ�� (����ư �Ÿ� ��)
    public List<TileInfo> GetTileInfos(InitialDeployment initialDeployment)
    {
        List<TileInfo> returnTileInfos = new List<TileInfo>();
        foreach (TileInfo tileInfo in allTileInfos)
        {
            if (initialDeployment == tileInfo.initialDeployment)
                returnTileInfos.Add(tileInfo);
        }
        return returnTileInfos;
    }

    public List<GameObject> GetManhattanTiles(TileInfo targetTile, int range)
    {
        List<GameObject> nearbyTiles = new List<GameObject>();

        // ���� Ÿ���� ��ǥ
        int targetX = targetTile.x;
        int targetY = targetTile.y;

        // ��� Ÿ���� �˻�
        foreach (TileInfo tile in allTileInfos)
        {
            // Ÿ���� ��ǥ
            int tileX = tile.x;
            int tileY = tile.y;

            // ����ư �Ÿ� ���
            int distance = Mathf.Abs(targetX - tileX) + Mathf.Abs(targetY - tileY);

            // �Ÿ��� range �̳��̸� ���� ������ ����� ������ ����Ʈ�� �߰�
            if (distance <= range && tileX >= 0 && tileX < xLength && tileY >= 0 && tileY < yLength)
                nearbyTiles.Add(tile.transform.gameObject);
        }

        return nearbyTiles;
    }

    public List<TileInfo> GetManhattanTileInfos(TileInfo targetTile, int range)
    {
        List<TileInfo> nearbyTileInfos = new List<TileInfo>();

        // ���� Ÿ���� ��ǥ
        int targetX = targetTile.x;
        int targetY = targetTile.y;

        // ��� Ÿ���� �˻�
        foreach (TileInfo tile in allTileInfos)
        {
            // Ÿ���� ��ǥ
            int tileX = tile.x;
            int tileY = tile.y;

            // ����ư �Ÿ� ���
            int distance = Mathf.Abs(targetX - tileX) + Mathf.Abs(targetY - tileY);

            // �Ÿ��� range �̳��̸� ���� ������ ����� ������ ����Ʈ�� �߰�
            if (distance <= range && tileX >= 0 && tileX < xLength && tileY >= 0 && tileY < yLength)
                nearbyTileInfos.Add(tile);
        }
        return nearbyTileInfos;
    }

    private void HighlightTiles(List<GameObject> targetTiles, Color color)
    {
        foreach (GameObject tile in targetTiles)
        {
            SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();
            sr.color = color;
        }
    }

    // GPT
    private void UpdateCurrentHoveredTile()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // ���콺 ��ġ���� ����ĳ��Ʈ ����
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 0f, LayerMask.GetMask("Map"));

        // ���� ���콺�� ����Ű�� �ִ� Ÿ��
        GameObject newHoveredTile = hit.collider != null ? hit.collider.gameObject : null;

        if (newHoveredTile != currentHoveredTile)
        {
            lastHoveredTile = currentHoveredTile;
            currentHoveredTile = newHoveredTile;

            if (currentHoveredTile != null)
                currentHoveredTileInfo = currentHoveredTile.GetComponent<TileInfo>();
            else
                currentHoveredTileInfo = null;
        }
    }

    // GPT
    private void HighlightCurrentHoveredTile(Color color)
    {
        if (currentHoveredTile != null)
        {
            SpriteRenderer sr = currentHoveredTile.GetComponent<SpriteRenderer>();
            sr.color = color;
        }
    }

    // ��� Ÿ���� ���� �ʱ�ȭ (���� : ���콺 �����Ͱ� ����Ű�� ����� ����� ��)
    private void HightlightAllTlies(Color color)
    {
        if (currentHoveredTile != lastHoveredTile)
        {
            foreach (TileInfo tileInfo in allTileInfos)
            {
                SpriteRenderer sr = tileInfo.GetComponent<SpriteRenderer>();
                sr.color = color;
            }
        }
    }

    // moveRange�� attackRange�� ��� �ð������� ǥ��
    private void DisplayRange()
    {
        // Ÿ���� ����Ű�� �ִ� ���
        if (currentHoveredTile != null)
        {
            int moveRange;
            int attackRange;

            if (currentHoveredTileInfo.unit != null)
            {
                moveRange = currentHoveredTileInfo.unit.currentMoveRange;
                attackRange = currentHoveredTileInfo.unit.currentAttackRange;
            }
            else if (InitialDeployManager.Instance.currentUnitCode != -1)
            {
                int unitCode = InitialDeployManager.Instance.currentUnitCode;
                Unit unit = UnitManager.Instance.GetUnit(unitCode);
                moveRange = unit.currentMoveRange;
                attackRange = unit.currentAttackRange;
            }
            else
                return;
            
            if (moveRange > attackRange)
            {
                List<GameObject> targetTiles1 = GetManhattanTiles(currentHoveredTileInfo, moveRange);
                HighlightTiles(targetTiles1, Color.green);
                List<GameObject> targetTiles2 = GetManhattanTiles(currentHoveredTileInfo, attackRange);
                HighlightTiles(targetTiles2, Color.yellow);
            }
            else if (moveRange < attackRange)
            {
                List<GameObject> targetTiles1 = GetManhattanTiles(currentHoveredTileInfo, attackRange);
                HighlightTiles(targetTiles1, Color.red);
                List<GameObject> targetTiles2 = GetManhattanTiles(currentHoveredTileInfo, moveRange);
                HighlightTiles(targetTiles2, Color.yellow);
            }
            else
            {
                List<GameObject> targetTiles = GetManhattanTiles(currentHoveredTileInfo, moveRange);
                HighlightTiles(targetTiles, Color.yellow);
            }
        }
    }

    // �ϳ��� ��Ÿ��� ǥ�� (�̱���)
    private void DisplayRange(int range, Color color)
    {

    }
}
