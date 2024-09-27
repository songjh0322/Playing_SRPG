using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager
{
    private GameObject tile_normal;
    // private GameObject tile_forest;
    // private GameObject tile_water;
    // private GameObject tile_unreachable;

    private const int mapSize = 10; // 맵의 크기 (10x10)

    public void LoadPrefabs()
    {
        // 여기에 각 타일 프리팹의 경로 추가
        tile_normal = Resources.Load<GameObject>("Prefabs/Tiles/basic_tile");   // TileType.Normal
    }

    // [캐릭터 배치 화면]으로 이동할 때 호출
    public void CreateMap()
    {
        // 모든 타일을 포함하는 Map 게임 오브젝트 생성
        GameObject mapObject = new GameObject("Map");

        for (int row = 0; row < mapSize; row++)
        {
            for (int col = 0; col < mapSize; col++)
            {
                // 타일 프리팹을 인스턴스화
                GameObject tile = Object.Instantiate(tile_normal, mapObject.transform); // Map 오브젝트의 자식으로 생성

                // 타일의 위치 설정
                float posX = col * 100; // 타일의 너비가 100이므로 col에 따라 x 위치 설정
                float posY = -row * 100; // 타일의 높이가 100이므로 row에 따라 y 위치 설정 (Y축 음수 방향으로 설정)
                tile.transform.localPosition = new Vector3(posX, posY, 0); // localPosition으로 설정

                // Tile 컴포넌트를 가져와서 좌표 설정
                Tile tileScript = tile.GetComponent<Tile>();
                tileScript.SetCoordinates(row, col);

                // 타일 이름 설정
                tile.name = $"Tile{row * mapSize + col:00}"; // 이름을 Tile00, Tile01, ... Tile99로 설정
            }
        }
    }
}
