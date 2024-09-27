using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isClickable;        // Ŭ�� ���� ����

    public int row; //{ get; private set; }           // Ÿ���� ��
    public int col; //{ get; private set; }           // Ÿ���� ��
    public Unit unit = null;                          // ���� Ÿ�Ͽ� �����ϴ� ���� (���� ��� null)
    public bool player1Deployable = true;             // Player1�� �ʱ� ���� ��ġ ���� ���� ����
    public bool player2Deployable = true;             // Player2�� �ʱ� ���� ��ġ ���� ���� ����
    public TileType tileType = TileType.Normal;    // Ÿ���� Ư�� (�Ϲ�, ������, ����, ���� ���� ��)

    // Ÿ�� �ʱ�ȭ
    public void Initialize(int row, int col, bool player1Deployable, bool player2Deployable, TileType tileType)
    {
        this.isClickable = false;
        this.row = row;
        this.col = col;
        this.player1Deployable = player1Deployable;
        this.player2Deployable = player2Deployable;
        this.tileType = tileType;
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
            // Ÿ���� ��� ���� ���
            Debug.Log($"Clicked Tile - Row: {row}, Col: {col}");
        }

        /*// Ŭ���� Ÿ���� GameManager�� �˷��� �ֺ� Ÿ���� ó���ϵ��� ��
        GameManager.Instance.OnTileClicked(this);*/
    }
}
