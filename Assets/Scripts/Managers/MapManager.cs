using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class MapManager
{
    public static MapManager Instance { get; private set; }

    private GameObject tile_normal;
    // private GameObject tile_forest;
    // private GameObject tile_water;
    // private GameObject tile_unreachable;

    private Tile[,] tiles;          // 모든 Tile 객체를 각 위치에 맞게 저장하는 2차원 배열
    private const int mapSize = 10; // 맵의 크기 (10x10)

    // 싱글톤 인스턴스 설정
    private MapManager()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // 싱글톤 인스턴스를 반환
    public static MapManager GetInstance()
    {
        if (Instance == null)
        {
            Instance = new MapManager();
        }
        return Instance;
    }

    public void LoadPrefabs()
    {
        // 여기에 각 타일 타입에 따라 프리팹의 경로 추가
        tile_normal = Resources.Load<GameObject>("Prefabs/Tiles/basic_tile");   // TileType.Normal
    }

    // 사용 : [캐릭터 선택을 모두 마쳤습니다. 전투를 시작하겠습니까?] -> [예] 버튼 클릭 시 호출
    // 기능 : tiles 2차원 배열에 Tile 객체를 참조하여 넣고, 맵을 생성하여 시각적으로 표시
    public void CreateMap()
    {
        tiles = new Tile[mapSize, mapSize];

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
                tile.transform.localPosition = new Vector3(posX, posY, 0);

                // Tile 컴포넌트를 가져와서 초기화
                Tile tileScript = tile.GetComponent<Tile>();

                // 위쪽 절반은 Player2, 아래쪽 절반은 Player1 배치 가능
                Deployable placementState = (row < mapSize / 2) ? Deployable.Player2 : Deployable.Player1;

                // 타일 초기화 (모든 타일의 TileType은 Normal로 설정)
                tileScript.Initialize(row, col, TileType.Normal, placementState);

                // 타일 이름 설정
                tile.name = $"Tile{row * mapSize + col:00}"; // 이름을 Tile00, Tile01, ... Tile99로 설정

                // tiles 2차원 배열에 추가
                tiles[row, col] = tileScript;
            }
        }
    }


    // 
    public void MoveTo(Tile fromTile, Tile totile)
    {

    }

    // startTile로부터 maxMoveRange 내의 모든 Tile 객체를 리스트로 반환하는 함수
    public List<Tile> GetReachableTiles(int maxMoveRange, Tile startTile)
    {
        List<Tile> reachableTiles = new List<Tile>();
        int startRow = startTile.row;
        int startCol = startTile.col;

        for (int row = 0; row < mapSize; row++)
        {
            for (int col = 0; col < mapSize; col++)
            {
                // 맨하튼 거리 계산
                int manhattanDistance = Mathf.Abs(startRow - row) + Mathf.Abs(startCol - col);

                // 맨하튼 거리 이내의 타일을 추가
                if (manhattanDistance <= maxMoveRange)
                {
                    reachableTiles.Add(tiles[row, col]);
                }
            }
        }

        return reachableTiles;
    }
}
