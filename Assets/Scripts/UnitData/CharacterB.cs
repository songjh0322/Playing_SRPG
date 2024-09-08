using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterB : Unit
{
    public CharacterB() : base(new BaseStats("B", 120, 15, 15, 4))
    {

    }

    // CharacterB의 고유 스킬
    public override void castSkill()
    {
        Debug.Log($"{name} 스킬 발동! 체력이 X로 회복되었습니다!");
    }
}
