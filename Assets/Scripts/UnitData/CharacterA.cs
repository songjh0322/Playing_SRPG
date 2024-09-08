using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterA : Unit
{
    public CharacterA() : base(new BaseStats("A", 100, 20, 10, 5))
    {

    }

    // CharacterA�� ���� ��ų
    public override void castSkill()
    {
        Debug.Log($"{name} ��ų �ߵ�! ������ X�� �����߽��ϴ�!");
    }
}
