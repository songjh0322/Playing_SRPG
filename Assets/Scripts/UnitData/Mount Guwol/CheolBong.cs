using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheolBong : Unit
{
    public CheolBong() : base(new BaseStats("철봉", 500, 60, 40, 3))
    {

    }

    // 패시브 - 전투 본능 : 현재 체력이 전체 체력의 30% 이하이면 기본 공격력과 기본 방어력이 100% 증가한다.

    // 공포의 쓴 맛 : 이동 거리 2 이내의 적에게 공격력(기본 60)의 150% 피해를 입힌다.
    public override void castSkill1()
    {
        Debug.Log($"{name}의 1번 스킬. 방어력이 X로 증가했습니다!");
    }

    // 쌍도끼 던지기 : 이동 거리 4 이내의 적에게 공격력(기본 60)의 200% 피해를 입힌다.
    public override void castSkill2()
    {

    }
}
