using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TileEnums;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    // 맵 크기 정보
    private int xLength, yLength;

    // 프리팹 관리 (Inspector에서 할당, 맵 생성용)
    public GameObject tilePrefab;
    public Sprite middleZone;

    // 현재 맵 정보 관리
    public List<GameObject> allTiles;   // 모든 Tile 게임 오브젝트
    public List<TileInfo> allTileInfos; // 모든 TileInfo
    public GameObject lastHoveredTile;
    public GameObject currentHoveredTile;
    public TileInfo currentHoveredTileInfo;

    private void Awake()
    {
        // Debug.Log("MapManager 생성됨");

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

        // 가리키는 타일이 변경될때마다 초기화
        if (currentHoveredTile != lastHoveredTile)
        {
            foreach (TileInfo tileInfo in allTileInfos)
            {
                SpriteRenderer sr = tileInfo.GetComponent<SpriteRenderer>();
                sr.color = Color.white;
            }
        }

        // 배치 화면에서 표기
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

    // 맵 프리팹 생성용 (10 by 12)
    public void CreateTestMap()
    {
        GameObject testMap = new GameObject("TestMap");
        // 화면 상의 적절한 위치에 표시
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

    // (GPT) 초기 배치가 가능한 타일의 리스트를 획득 (맨해튼 거리 내)
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

        // 기준 타일의 좌표
        int targetX = targetTile.x;
        int targetY = targetTile.y;

        // 모든 타일을 검색
        foreach (TileInfo tile in allTileInfos)
        {
            // 타일의 좌표
            int tileX = tile.x;
            int tileY = tile.y;

            // 맨해튼 거리 계산
            int distance = Mathf.Abs(targetX - tileX) + Mathf.Abs(targetY - tileY);

            // 거리가 range 이내이며 맵의 범위를 벗어나지 않으면 리스트에 추가
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

        // 마우스 위치에서 레이캐스트 수행
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 0f, LayerMask.GetMask("Map"));

        // 현재 마우스가 가리키고 있는 타일
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
