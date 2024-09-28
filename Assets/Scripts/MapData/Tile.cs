using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isClickable;        // Ŭ�� ���� ����

    public int row; //{ get; private set; }           // Ÿ���� ��
    public int col; //{ get; private set; }           // Ÿ���� ��
    public Unit unit = null;                          // ���� Ÿ�Ͽ� �����ϴ� ���� (���� ��� null)

    public TileType tileType = TileType.Normal;       // Ÿ���� Ư�� (�Ϲ�, ������, ����, ���� ���� ��)
    public Deployable tilePlacementState;

    // Ÿ�� �ʱ�ȭ
    public void Initialize(int row, int col, TileType tileType, Deployable tilePlacementState)
    {
        this.isClickable = true;
        this.row = row;
        this.col = col;
        this.tileType = tileType;
        this.tilePlacementState = tilePlacementState;
    }

    // Ÿ���� ��� ���� �����ϴ� �Լ�
    public void SetCoordinates(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

    public void SetTileType(TileType tileType)
    {
        this.tileType = tileType;
    }

/*    // Ÿ�� ������ ����
    public void ChangeColor(Color color)
    {
        // Ÿ���� ������ �����ϴ� ���� (��: SpriteRenderer�� ���� ���� ����)
        GetComponent<SpriteRenderer>().color = color;
    }*/

    // 
    private void OnMouseDown()
    {
        if (isClickable)
        {
            // Ÿ���� ��ġ�� �ʱ� ��ġ ���� ���� ǥ�� (�ӽ�)
            Debug.Log($"({row},{col}) : {tilePlacementState}");
        }

        /*// Ŭ���� Ÿ���� GameManager�� �˷��� �ֺ� Ÿ���� ó���ϵ��� ��
        GameManager.Instance.OnTileClicked(this);*/
    }
}
