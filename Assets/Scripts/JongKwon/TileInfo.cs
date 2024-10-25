using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TileEnums;

public class TileInfo : MonoBehaviour
{
    public int x, y;
    public Unit unit;
    public TileType tileType;
    public InitialDeployment initialDeployment;
    public Vector3 worldXY;

    private void Start()
    {
        unit = null;
        worldXY = transform.position;
    }

    private void OnMouseDown()
    {
        Debug.Log($"({x},{y}) 타일 클릭됨");
        if (unit != null)
            Debug.Log($"현재 {unit.basicStats.unitName}이(가) 위치하고 있습니다.");
        if (GameManager.Instance.gameState == GameState.InitialDeployment)
            InitialDeployManager.Instance.OnTileClicked(this);
    }

    public void Initialize(int x, int y, TileType tileType, InitialDeployment initialDeployment)
    {
        this.x = x;
        this.y = y;
        this.tileType = tileType;
        this.initialDeployment = initialDeployment;
    }

    /*public Vector3 GetCenterWorldPosition()
    {
        return transform.position;
    }*/
}
