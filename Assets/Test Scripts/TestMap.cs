using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tile�� ��� Scripts/Map ����

// ���� ��� ��
public class TestMap
{
    // 2���� �迭 map
    public Tile[,] map = new Tile[2, 2];
    readonly (int, int) size;

    // ������ ���ÿ� ��� �ʱ�ȭ
    public TestMap()
    {
        /*map[0, 0] = new Tile((0, 0), TileType.Normal, true);
        map[0, 1] = new Tile((0, 1), TileType.Normal, true);
        map[1, 0] = new Tile((1, 0), TileType.Normal, true);
        map[1, 1] = new Tile((1, 1), TileType.Normal, true);*/
        size = (2, 2);
    }
}
