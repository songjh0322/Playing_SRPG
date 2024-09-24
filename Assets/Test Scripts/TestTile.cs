using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTile : MonoBehaviour
{
    public (int, int) position;   // ���� Ÿ���� Map �󿡼��� ��� ��
    public Unit unit;             // ���� Ÿ�Ͽ� �����ϴ� ���� (���� ��� null)
    public bool deployable;       // (�÷��̾���) �ʱ� ���� ��ġ ���� ���� ����
    public TileType tileType;     // Ÿ���� Ư�� (�Ϲ�, ������, ����, ���� ���� ��)

    private void Start()
    {
        position = (0, 0);
        unit = new CharacterExample();
        deployable = true;
        tileType = TileType.Normal;

    }

    // ��ǥ(��� ��)�� Ʃ�÷� ��ȯ
    public (int, int) GetPosition()
    {
        return position;
    }

    private void OnMouseDown()
    {
        Debug.Log("Tile clicked!");
    }
}
