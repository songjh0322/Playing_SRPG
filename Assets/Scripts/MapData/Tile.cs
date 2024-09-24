using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int row; //{ get; private set; }           // Ÿ���� ��
    public int col; //{ get; private set; }           // Ÿ���� ��
    public Unit unit = null;                       // ���� Ÿ�Ͽ� �����ϴ� ���� (���� ��� null)
    public bool deployable = false;                // (�÷��̾���) �ʱ� ���� ��ġ ���� ���� ����
    public TileType tileType = TileType.Normal;    // Ÿ���� Ư�� (�Ϲ�, ������, ����, ���� ���� ��)

    // Ÿ�� �ʱ�ȭ
    public void Initialize(int row, int col, bool deployable, TileType tileType)
    {
        this.row = row;
        this.col = col;
        this.deployable = deployable;
        this.tileType = tileType;
    }

    // Ÿ���� ��� ���� �����ϴ� �Լ�
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

    // Ÿ�� Ŭ�� �� ȣ��Ǵ� �޼���
    private void OnMouseDown()
    {
        // Ÿ���� ��� ���� ���
        Debug.Log($"Clicked Tile - Row: {row}, Col: {col}");
    }
}
