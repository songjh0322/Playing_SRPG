using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� Ÿ���� Ư�� ����
public enum TileType
{
    Normal,         // Ư�� ����
    Forest,         // �ǰ� �� ȸ���� Ȯ�� 40%, ��ų ��� �� ���� ������ 3 �̻��� ��� -2
    Water,          // �̵� �� �̵� ������ 4 �̻��� ��� -3 (### ������ ��쿡�� ����ž� �� -> �ش� ���� ���̵�� �ʿ�... ###)
    Unreachable     // ������ �� ���� Ÿ��
}

// ���� ���ο��� ������ Ÿ�Ͽ� ������ ������
public class Tile
{
    public readonly int x, y;              // ���� Ÿ���� Map �󿡼��� ��� ��
    public Unit unit;                      // ���� Ÿ�Ͽ� �����ϴ� ���� (���� ��� null)
    public readonly bool deployable;       // (�÷��̾���) �ʱ� ���� ��ġ ���� ���� ����
    public readonly TileType tileType;     // Ÿ���� Ư�� (�Ϲ�, ������, ����, ���� ���� ��)

    // ������
    public Tile(int x, int y, TileType type, bool canDeploy)
    {
        this.x = x;
        this.y = y;
        unit = null;                // Ÿ�� ���� ������ ������ �ʱ�ȭ
        tileType = type;
        deployable = canDeploy;
    }

    // ��ǥ(��� ��)�� ��ȯ
    public (int, int) GetPosition()
    {
        return (x, y);
    }
}

// 10x10�� ���� ���簢�� �� (�� ��� 100���� Ÿ���� ����)
public class BasicMap
{
    // 2���� �迭 map
    public Tile[,] map = new Tile[10, 10];
    int x;
    int y;

    // ������
    public BasicMap()
    {
        x = 10;
        y = 10;
        InitializeMap();
    }

    // �� �ʱ�ȭ �ż��� (�̰��� Ÿ�� ����, ��ġ ���� ���� ���� ����)
    private void InitializeMap()
    {
        // ���� ��ȸ�ϸ鼭 �� Ÿ���� �ʱ�ȭ
        for (int row = 0; row < 10; row++)
        {
            for (int col = 0; col < 10; col++)
            {
                // ���� ���� �� ���� ��ġ ���� ���� ����: 8~9�� �� 2~7���� ���� true, �������� false
                bool deployable = ((row == 8 || row == 9) && (col >= 2 && col <= 7));

                // TileType�� �⺻������ Normal�� ����
                TileType tileType = TileType.Normal;
                
                // ������ ����
                if ((row >= 4 && row <= 5) && (col >= 0 && col <= 2))
                {
                    tileType = TileType.Forest;
                }

                // ���� ����
                if ((row == 3 || row == 6) && (col >= 7 && col <= 9))
                {
                    tileType = TileType.Water;
                }
                else if ((row >= 4 && row <= 5) && (col >= 5 && col <= 7))
                {
                    tileType = TileType.Water;
                }

                map[row, col] = new Tile(row, col, tileType, deployable);
            }
        }
    }

    // GPT ����
    // �̵� �Ǵ� ����(��ų)�� ���� ���� ������ ĭ ���� ����ϰ�, �ش� ��ǥ(��� ��)�� Ʃ�÷� ���� List�� ��ȯ
    public List<(int, int)> GetReachableTiles(Tile currentTile, int maxRange, int mapRows, int mapCols)
    {
        List<(int, int)> reachableTiles = new List<(int, int)>();
        (int currentX, int currentY) = currentTile.GetPosition();

        // �̵� ������ ���� ���� ��ǥ�� Ȯ��
        for (int dx = -maxRange; dx <= maxRange; dx++)
        {
            for (int dy = -maxRange; dy <= maxRange; dy++)
            {
                // �̵� ������ ����ư �Ÿ��� ��� (dx + dy <= maxRange)
                if (Math.Abs(dx) + Math.Abs(dy) <= maxRange)
                {
                    int newX = currentX + dx;
                    int newY = currentY + dy;

                    // ��ȿ�� ��ǥ���� üũ (���� ���� ���� �ִ��� Ȯ��)
                    if (newX >= 0 && newX < mapRows && newY >= 0 && newY < mapCols)
                    {
                        reachableTiles.Add((newX, newY));
                    }
                }
            }
        }

        return reachableTiles;
    }
}
