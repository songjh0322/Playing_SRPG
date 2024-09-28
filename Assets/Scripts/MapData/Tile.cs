using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isClickable;        // 클릭 가능 여부

    public int row; //{ get; private set; }           // 타일의 행
    public int col; //{ get; private set; }           // 타일의 열
    public Unit unit = null;                          // 현재 타일에 존재하는 유닛 (없는 경우 null)

    public TileType tileType = TileType.Normal;       // 타일의 특성 (일반, 숲지대, 수역, 막힌 구역 등)
    public Deployable tilePlacementState;

    // 타일 초기화
    public void Initialize(int row, int col, TileType tileType, Deployable tilePlacementState)
    {
        this.isClickable = true;
        this.row = row;
        this.col = col;
        this.tileType = tileType;
        this.tilePlacementState = tilePlacementState;
    }

    // 타일의 행과 열을 설정하는 함수
    public void SetCoordinates(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

    public void SetTileType(TileType tileType)
    {
        this.tileType = tileType;
    }

/*    // 타일 색상을 변경
    public void ChangeColor(Color color)
    {
        // 타일의 색상을 변경하는 로직 (예: SpriteRenderer를 통한 색상 변경)
        GetComponent<SpriteRenderer>().color = color;
    }*/

    // 
    private void OnMouseDown()
    {
        if (isClickable)
        {
            // 타일의 위치와 초기 배치 가능 여부 표시 (임시)
            Debug.Log($"({row},{col}) : {tilePlacementState}");
        }

        /*// 클릭된 타일을 GameManager에 알려서 주변 타일을 처리하도록 함
        GameManager.Instance.OnTileClicked(this);*/
    }
}
