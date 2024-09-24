using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager
{
<<<<<<< HEAD
    private GameObject tilePrefab; // 타일 프리팹
    private const int mapSize = 10; // 맵의 크기 (10x10)

    public MapManager(GameObject tilePrefab)
    {
        this.tilePrefab = tilePrefab;
    }

    public void CreateMap()
    {
        // Map이라는 빈 게임 오브젝트 생성
        GameObject mapObject = new GameObject("Map");

        for (int row = 0; row < mapSize; row++)
        {
            for (int col = 0; col < mapSize; col++)
            {
                // 타일 프리팹을 인스턴스화
                GameObject tile = Object.Instantiate(tilePrefab, mapObject.transform); // Map 오브젝트의 자식으로 생성

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
=======
    private static MapManager instance;
    public static MapManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MapManager();
            }
            return instance;
        }
    }

    private MapManager() { }

    public void InitializeMap()
    {
        Debug.Log("MapManager: 맵 초기화");
        // 맵 생성 로직 추가
    }
}

>>>>>>> 9e268010859d94b39b3f0e05584f489f7eddac52
