using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager
{
    int maxUnit;                    // ���� ������ �ִ� ĳ���� ��
    Unit[] Player1Units;            // Player1�� ������ ����
    Unit[] Player2Units;            // Player2�� ������ ����

    public UnitManager()
    {
        maxUnit = 6;
        Player1Units = new Unit[maxUnit];  // Player1 ���� �迭 �ʱ�ȭ
        Player2Units = new Unit[maxUnit];  // Player2 ���� �迭 �ʱ�ȭ
    }

    // UI : [ĳ���� ������ ��� ���ƽ��ϴ�. ������ �����Ͻðڽ��ϱ�?]
    // [��]�� Ŭ���� ��� ȣ��
    public void ConfirmPlayer1Units()
    {

    }

    public void RandomizePlayer2Units()
    {

    }
}

