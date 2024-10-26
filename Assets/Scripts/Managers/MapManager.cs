using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static TileEnums;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    // 프리팹 관리 (Inspector에서 할당, 맵 생성용)
    public GameObject tilePrefab;
    public Sprite middleZone;

    // 현재 맵 정보 관리
    public List<GameObject> allTiles;   // 모든 Tile 게임 오브젝트
    public List<TileInfo> allTileInfos; // 모든 TileInfo

    private void Awake()
    {
        Debug.Log("MapManager 생성됨");

        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        allTileInfos = new List<TileInfo>();

        CreateTestMap();

    }

    // 맵 프리팹 생성용 (10 by 12)
    public void CreateTestMap()
    {
        GameObject testMap = new GameObject("TestMap");
        testMap.transform.position = new Vector3(-20, 0, 0);

        for (int y = 0; y < 12; y++)
        {
            for (int x = 0; x < 10; x++)
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

    // 초기 배치가 가능한 타일의 리스트를 획득
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

    public void GetManhattanDistance()
    {

    }
}
