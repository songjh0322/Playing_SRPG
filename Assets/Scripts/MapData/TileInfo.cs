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

// �ʱ� ��ġ ���� ����
public enum Deployable
{
    None,
    Player1,
    Player2
}


