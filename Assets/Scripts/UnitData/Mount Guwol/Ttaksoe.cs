using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttaksoe : Unit
{
    public Ttaksoe() : base(new BaseStats("����", 400, 30, 80, 3), 0, 0)
    {

    }

    // ��� ���� : �̵� �Ÿ� 4 �̳��� ������ �Ʊ��� �⺻ ���ݷ��� 30%, �⺻ ������ 20% ������Ų��.
    public override void castSkill1()
    {
        Debug.Log($"{name}�� 1�� ��ų. ������ X�� �����߽��ϴ�!");
    }

    // �ֵ��� ������ : �̵� �Ÿ� 3 �̳��� ������ �⺻ ���ݷ�(30)�� 200%�� ���ظ� ������ �ش� ���� ������ 30% Ȯ���� �����Ͽ� �ൿ�� ����ȴ�.
    public override void castSkill2()
    {

    }
}
