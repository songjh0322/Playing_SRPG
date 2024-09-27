using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager
{
    private GameObject tile_normal;
    // private GameObject tile_forest;
    // private GameObject tile_water;
    // private GameObject tile_unreachable;

    private const int mapSize = 10; // ���� ũ�� (10x10)

    public void LoadPrefabs()
    {
        // ���⿡ �� Ÿ�� �������� ��� �߰�
        tile_normal = Resources.Load<GameObject>("Prefabs/Tiles/basic_tile");   // TileType.Normal
    }

    // [ĳ���� ��ġ ȭ��]���� �̵��� �� ȣ��
    public void CreateMap()
    {
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
                tile.transform.localPosition = new Vector3(posX, posY, 0); // localPosition���� ����

                // Tile ������Ʈ�� �����ͼ� ��ǥ ����
                Tile tileScript = tile.GetComponent<Tile>();
                tileScript.SetCoordinates(row, col);

                // Ÿ�� �̸� ����
                tile.name = $"Tile{row * mapSize + col:00}"; // �̸��� Tile00, Tile01, ... Tile99�� ����
            }
        }
    }
}
