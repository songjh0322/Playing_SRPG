using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterB : Unit
{
    public CharacterB() : base(new BaseStats("B", 120, 15, 15, 4))
    {

    }

    // CharacterB�� ���� ��ų
    public override void castSkill()
    {
        Debug.Log($"{name} ��ų �ߵ�! ü���� X�� ȸ���Ǿ����ϴ�!");
    }
}
