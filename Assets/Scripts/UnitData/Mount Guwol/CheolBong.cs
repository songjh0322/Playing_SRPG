using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheolBong : Unit
{
    public CheolBong() : base(new BaseStats("ö��", 500, 60, 40, 3))
    {

    }

    // �нú� - ���� ���� : ���� ü���� ��ü ü���� 30% �����̸� �⺻ ���ݷ°� �⺻ ������ 100% �����Ѵ�.

    // ������ �� �� : �̵� �Ÿ� 2 �̳��� ������ ���ݷ�(�⺻ 60)�� 150% ���ظ� ������.
    public override void castSkill1()
    {
        Debug.Log($"{name}�� 1�� ��ų. ������ X�� �����߽��ϴ�!");
    }

    // �ֵ��� ������ : �̵� �Ÿ� 4 �̳��� ������ ���ݷ�(�⺻ 60)�� 200% ���ظ� ������.
    public override void castSkill2()
    {

    }
}
