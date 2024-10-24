using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TileEnums;

public class TileInfo : MonoBehaviour
{
    public int x;
    public int y;
    public Unit unit;
    public TileType tileType;
    public InitialDeployment initialDeployment;

    private void Start()
    {
        unit = null;
        tileType = TileType.Normal;
        initialDeployment = InitialDeployment.Player1;
    }

    private void OnMouseDown()
    {
        Debug.Log($"({x},{y}) ≈∏¿œ ≈¨∏Øµ ");
    }

    public void Initialize(int x, int y, TileType tileType, InitialDeployment initialDeployment)
    {
        this.x = x;
        this.y = y;
        this.tileType = tileType;
        this.initialDeployment = initialDeployment;
    }

    public Vector3 GetCenterWorld()
    {
        return transform.position;
    }



}
