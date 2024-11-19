using System.Collections.Generic;
using UnityEngine;
using static TileEnums;

public class MapManager1 : MonoBehaviour
{
    public static MapManager1 Instance { get; private set; }

    // 맵 크기 설정
    private int xLength, yLength;

    // 타일 관련 데이터
    public GameObject tilePrefab;
    public Sprite middleZone;
    public List<GameObject> allTiles;
    public List<TileInfo> allTileInfos;

    private void Awake()
    {
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

    // 맵 생성 (10x12)
    public void CreateTestMap()
    {
        GameObject testMap = new GameObject("TestMap");
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

    // 특정 초기 배치 타입의 타일 가져오기
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

    // 맨해튼 거리 내의 타일 가져오기 (GameObject)
    public List<GameObject> GetManhattanTiles(GameObject targetTile, int range)
    {
        List<GameObject> nearbyTiles = new List<GameObject>();
        int targetX = targetTile.GetComponent<TileInfo>().x;
        int targetY = targetTile.GetComponent<TileInfo>().y;

        foreach (TileInfo tile in allTileInfos)
        {
            int tileX = tile.x;
            int tileY = tile.y;
            int distance = Mathf.Abs(targetX - tileX) + Mathf.Abs(targetY - tileY);

            if (distance <= range && tileX >= 0 && tileX < xLength && tileY >= 0 && tileY < yLength)
                nearbyTiles.Add(tile.transform.gameObject);
        }

        return nearbyTiles;
    }

    // 맨해튼 거리 내의 타일 가져오기 (TileInfo)
    public List<TileInfo> GetManhattanTileInfos(TileInfo targetTile, int range)
    {
        List<TileInfo> nearbyTileInfos = new List<TileInfo>();
        int targetX = targetTile.x;
        int targetY = targetTile.y;

        foreach (TileInfo tile in allTileInfos)
        {
            int tileX = tile.x;
            int tileY = tile.y;
            int distance = Mathf.Abs(targetX - tileX) + Mathf.Abs(targetY - tileY);

            if (distance <= range && tileX >= 0 && tileX < xLength && tileY >= 0 && tileY < yLength)
                nearbyTileInfos.Add(tile);
        }
        return nearbyTileInfos;
    }
}
