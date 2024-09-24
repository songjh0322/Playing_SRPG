using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTile : MonoBehaviour
{
    public (int, int) position;   // 현재 타일의 Map 상에서의 행과 열
    public Unit unit;             // 현재 타일에 존재하는 유닛 (없는 경우 null)
    public bool deployable;       // (플레이어의) 초기 유닛 배치 가능 구역 여부
    public TileType tileType;     // 타일의 특성 (일반, 숲지대, 수역, 막힌 구역 등)

    private void Start()
    {
        position = (0, 0);
        unit = new CharacterExample();
        deployable = true;
        tileType = TileType.Normal;

    }

    // 좌표(행과 열)을 튜플로 반환
    public (int, int) GetPosition()
    {
        return position;
    }

    private void OnMouseDown()
    {
        Debug.Log("Tile clicked!");
    }
}
