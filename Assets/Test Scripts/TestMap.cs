using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tile의 경우 Scripts/Map 참고

// 실험 대상 맵
public class TestMap
{
    // 2차원 배열 map
    public Tile[,] map = new Tile[2, 2];
    readonly (int, int) size;

    // 생성과 동시에 모두 초기화
    public TestMap()
    {
        /*map[0, 0] = new Tile((0, 0), TileType.Normal, true);
        map[0, 1] = new Tile((0, 1), TileType.Normal, true);
        map[1, 0] = new Tile((1, 0), TileType.Normal, true);
        map[1, 1] = new Tile((1, 1), TileType.Normal, true);*/
        size = (2, 2);
    }
}
