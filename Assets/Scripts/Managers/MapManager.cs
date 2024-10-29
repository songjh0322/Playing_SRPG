using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TileEnums;

// 맵 형태 생성 및 맵의 시각적 변화(타일 색)를 담당
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

    // 마우스 포인터 관련 정보
    public GameObject lastHoveredTile;
    public GameObject currentHoveredTile;
    public TileInfo currentHoveredTileInfo;
    public List<TileInfo> lastTileInfos;    // 범위 표시를 했던 타일의 정보 (초기화를 위함)

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
        UpdateCurrentHoveredTile();

        // 배치 중일 때
        if (GameManager.Instance.gameState == GameState.InitialDeployment)
        {
            HightlightAllTlies(Color.white);
            DisplayRange();
        }
        // 배치 이후 인게임일 때
        else if (GameManager.Instance.gameState == GameState.InGame)
        {
            HightlightAllTlies(Color.white);
            if (InGameManager.Instance.state == InGameManager.State.NotSelected
            || InGameManager.Instance.state == InGameManager.State.BehaviourButtonsOn)
                DisplayRange();
        }

        // 가리키는 타일이 변경될때마다 초기화
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

    public List<TileInfo> GetManhattanTileInfos(TileInfo targetTile, int range)
    {
        List<TileInfo> nearbyTileInfos = new List<TileInfo>();

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
    private void HighlightCurrentHoveredTile(Color color)
    {
        if (currentHoveredTile != null)
        {
            SpriteRenderer sr = currentHoveredTile.GetComponent<SpriteRenderer>();
            sr.color = color;
        }
    }

    // 모든 타일의 색을 초기화 (조건 : 마우스 포인터가 가리키는 대상이 변경될 때)
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

    // moveRange와 attackRange를 모두 시각적으로 표기
    private void DisplayRange()
    {
        // 타일을 가리키고 있는 경우
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

    // 하나의 사거리만 표기 (미구현)
    private void DisplayRange(int range, Color color)
    {

    }
}
