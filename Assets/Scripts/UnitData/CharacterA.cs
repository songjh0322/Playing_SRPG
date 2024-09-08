using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterA : Unit
{
    public CharacterA() : base(new BaseStats("A", 100, 20, 10, 5))
    {

    }

    // CharacterA의 고유 스킬
    public override void castSkill()
    {
        Debug.Log($"{name} 스킬 발동! 방어력이 X로 증가했습니다!");
    }
}
