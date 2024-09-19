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
    public Unit unit;                      // ���� Ÿ�Ͽ� �����ϴ� ����
    public readonly bool deployable;       // (�÷��̾���) �ʱ� ���� ��ġ ���� ���� ����
    public readonly TileType tileType;     // Ÿ���� Ư�� (�Ϲ�, ������, ����, ���� ���� ��)

    // ������
    public Tile(TileType type, bool canDeploy)
    {
        tileType = type;
        deployable = canDeploy;
    }
}

// 10x10�� ���� ���簢�� �� (�� ��� 100���� Ÿ���� ����)
public class BasicMap
{
    // 2���� �迭 map
    public Tile[,] map = new Tile[10, 10];

    // ������
    public BasicMap()
    {
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

                map[row, col] = new Tile(tileType, deployable);
            }
        }
    }
}
