using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttaksoe : Unit
{
    public Ttaksoe() : base(new BaseStats("딱쇠", 400, 30, 80, 3), 0, 0)
    {

    }

    // 사기 진작 : 이동 거리 4 이내의 지정한 아군의 기본 공격력이 30%, 기본 방어력이 20% 증가시킨다.
    public override void castSkill1()
    {
        Debug.Log($"{name}의 1번 스킬. 방어력이 X로 증가했습니다!");
    }

    // 쌍도끼 던지기 : 이동 거리 3 이내의 적에게 기본 공격력(30)의 200%의 피해를 입히고 해당 적은 다음턴 30% 확률로 기절하여 행동이 봉쇄된다.
    public override void castSkill2()
    {

    }
}
