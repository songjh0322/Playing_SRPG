using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int row; //{ get; private set; }           // 타일의 행
    public int col; //{ get; private set; }           // 타일의 열
    public Unit unit = null;                       // 현재 타일에 존재하는 유닛 (없는 경우 null)
    public bool deployable = false;                // (플레이어의) 초기 유닛 배치 가능 구역 여부
    public TileType tileType = TileType.Normal;    // 타일의 특성 (일반, 숲지대, 수역, 막힌 구역 등)

    // 타일 초기화
    public void Initialize(int row, int col, bool deployable, TileType tileType)
    {
        this.row = row;
        this.col = col;
        this.deployable = deployable;
        this.tileType = tileType;
    }

    // 타일의 행과 열을 설정하는 함수
    public void SetCoordinates(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

    public void SetDeployable(bool deployable)
    {
        this.deployable = deployable;
    }

    public void SetTileType(TileType tileType)
    {
        this.tileType = tileType;
    }

    // 타일 클릭 시 호출되는 메서드
    private void OnMouseDown()
    {
        // 타일의 행과 열을 출력
        Debug.Log($"Clicked Tile - Row: {row}, Col: {col}");
    }
}
