using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 각 타일의 특성 정보
public enum TileType
{
    Normal,         // 특성 없음
    Forest,         // 피격 시 회피할 확률 40%, 스킬 사용 시 공격 범위가 3 이상인 경우 -2
    Water,          // 이동 시 이동 범위가 4 이상인 경우 -3 (### 지나갈 경우에도 적용돼야 함 -> 해당 로직 아이디어 필요... ###)
    Unreachable     // 도달할 수 없는 타일
}

// 맵의 내부에서 각각의 타일에 저장할 데이터
public class Tile
{
    public Unit unit;                      // 현재 타일에 존재하는 유닛
    public readonly bool deployable;       // (플레이어의) 초기 유닛 배치 가능 구역 여부
    public readonly TileType tileType;     // 타일의 특성 (일반, 숲지대, 수역, 막힌 구역 등)

    // 생성자
    public Tile(TileType type, bool canDeploy)
    {
        tileType = type;
        deployable = canDeploy;
    }
}

// 10x10의 단일 정사각형 맵 (이 경우 100개의 타일이 존재)
public class BasicMap
{
    // 2차원 배열 map
    public Tile[,] map = new Tile[10, 10];

    // 생성자
    public BasicMap()
    {
        InitializeMap();
    }

    // 맵 초기화 매서드 (이곳에 타일 종류, 배치 가능 구역 여부 선언)
    private void InitializeMap()
    {
        // 맵을 순회하면서 각 타일을 초기화
        for (int row = 0; row < 10; row++)
        {
            for (int col = 0; col < 10; col++)
            {
                // 게임 시작 시 유닛 배치 가능 구역 설정: 8~9행 및 2~7열에 대해 true, 나머지는 false
                bool deployable = ((row == 8 || row == 9) && (col >= 2 && col <= 7));

                // TileType을 기본적으로 Normal로 설정
                TileType tileType = TileType.Normal;
                
                // 숲지대 설정
                if ((row >= 4 && row <= 5) && (col >= 0 && col <= 2))
                {
                    tileType = TileType.Forest;
                }

                // 수역 설정
                if ((row == 3 || row == 6) && (col >= 7 && col <= 9))
                {
                    tileType = TileType.Water;
                }
                else if ((row >= 4 && row <= 5) && (col >= 5 && col <= 7))
                {
                    tileType = TileType.Water;
                }

                map[row, col] = new Tile(tileType, deployable);
            }
        }
    }
}
