using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TileEnums;

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
    public GameObject lastHoveredTile;
    public GameObject currentHoveredTile;
    public TileInfo currentHoveredTileInfo;

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
        UpdateCurrentHoverTile();

        // ����Ű�� Ÿ���� ����ɶ����� �ʱ�ȭ
        if (currentHoveredTile != lastHoveredTile)
        {
            foreach (TileInfo tileInfo in allTileInfos)
            {
                SpriteRenderer sr = tileInfo.GetComponent<SpriteRenderer>();
                sr.color = Color.white;
            }
        }

        // ��ġ ȭ�鿡�� ǥ��
        if (GameManager.Instance.gameState == GameState.InitialDeployment
            && currentHoveredTile != null
            && InitialDeployManager.Instance.currentUnitCode != -1)
        {
            int moveRange = UnitManager.Instance.GetUnit(InitialDeployManager.Instance.currentUnitCode).currentMoveRange;
            List<GameObject> targetTiles = GetManhattanTiles(currentHoveredTileInfo, moveRange);
            HighlightTiles(targetTiles, Color.red);
        }
        // 
        else if (currentHoveredTile != null && currentHoveredTileInfo.unit != null)
        {
            List<GameObject> targetTiles = GetManhattanTiles(currentHoveredTileInfo, currentHoveredTileInfo.unit.basicStats.moveRange);
            HighlightTiles(targetTiles, Color.red);
        }

        HighlightCurrentHoveredTile();
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

    public void HighlightTiles(List<GameObject> targetTiles, Color color)
    {
        foreach (GameObject tile in targetTiles)
        {
            SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();
            sr.color = color;
        }
    }

    // GPT
    private void UpdateCurrentHoverTile()
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
    private void HighlightCurrentHoveredTile()
    {
        if (currentHoveredTile != null)
        {
            SpriteRenderer sr = currentHoveredTile.GetComponent<SpriteRenderer>();
            sr.color = Color.gray;
        }
    }
}
