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

    private Tile[,] tiles;          // ��� Tile ��ü�� �� ��ġ�� �°� �����ϴ� 2���� �迭
    private const int mapSize = 10; // ���� ũ�� (10x10)

    // �̱��� �ν��Ͻ� ����
    private MapManager()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // �̱��� �ν��Ͻ��� ��ȯ
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
        // ���⿡ �� Ÿ�� Ÿ�Կ� ���� �������� ��� �߰�
        tile_normal = Resources.Load<GameObject>("Prefabs/Tiles/basic_tile");   // TileType.Normal
    }

    // ��� : [ĳ���� ������ ��� ���ƽ��ϴ�. ������ �����ϰڽ��ϱ�?] -> [��] ��ư Ŭ�� �� ȣ��
    // ��� : tiles 2���� �迭�� Tile ��ü�� �����Ͽ� �ְ�, ���� �����Ͽ� �ð������� ǥ��
    public void CreateMap()
    {
        tiles = new Tile[mapSize, mapSize];

        // ��� Ÿ���� �����ϴ� Map ���� ������Ʈ ����
        GameObject mapObject = new GameObject("Map");

        for (int row = 0; row < mapSize; row++)
        {
            for (int col = 0; col < mapSize; col++)
            {
                // Ÿ�� �������� �ν��Ͻ�ȭ
                GameObject tile = Object.Instantiate(tile_normal, mapObject.transform); // Map ������Ʈ�� �ڽ����� ����

                // Ÿ���� ��ġ ����
                float posX = col * 100; // Ÿ���� �ʺ� 100�̹Ƿ� col�� ���� x ��ġ ����
                float posY = -row * 100; // Ÿ���� ���̰� 100�̹Ƿ� row�� ���� y ��ġ ���� (Y�� ���� �������� ����)
                tile.transform.localPosition = new Vector3(posX, posY, 0);

                // Tile ������Ʈ�� �����ͼ� �ʱ�ȭ
                Tile tileScript = tile.GetComponent<Tile>();

                // ���� ������ Player2, �Ʒ��� ������ Player1 ��ġ ����
                Deployable placementState = (row < mapSize / 2) ? Deployable.Player2 : Deployable.Player1;

                // Ÿ�� �ʱ�ȭ (��� Ÿ���� TileType�� Normal�� ����)
                tileScript.Initialize(row, col, TileType.Normal, placementState);

                // Ÿ�� �̸� ����
                tile.name = $"Tile{row * mapSize + col:00}"; // �̸��� Tile00, Tile01, ... Tile99�� ����

                // tiles 2���� �迭�� �߰�
                tiles[row, col] = tileScript;
            }
        }
    }


    // 
    public void MoveTo(Tile fromTile, Tile totile)
    {

    }

    // startTile�κ��� maxMoveRange ���� ��� Tile ��ü�� ����Ʈ�� ��ȯ�ϴ� �Լ�
    public List<Tile> GetReachableTiles(int maxMoveRange, Tile startTile)
    {
        List<Tile> reachableTiles = new List<Tile>();
        int startRow = startTile.row;
        int startCol = startTile.col;

        for (int row = 0; row < mapSize; row++)
        {
            for (int col = 0; col < mapSize; col++)
            {
                // ����ư �Ÿ� ���
                int manhattanDistance = Mathf.Abs(startRow - row) + Mathf.Abs(startCol - col);

                // ����ư �Ÿ� �̳��� Ÿ���� �߰�
                if (manhattanDistance <= maxMoveRange)
                {
                    reachableTiles.Add(tiles[row, col]);
                }
            }
        }

        return reachableTiles;
    }
}
