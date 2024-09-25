using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager
{
    int maxUnit;                    // 선택 가능한 최대 캐릭터 수
    Unit[] Player1Units;            // Player1이 선택한 유닛
    Unit[] Player2Units;            // Player2가 선택한 유닛

    public UnitManager()
    {
        maxUnit = 6;
        Player1Units = new Unit[maxUnit];  // Player1 유닛 배열 초기화
        Player2Units = new Unit[maxUnit];  // Player2 유닛 배열 초기화
    }

    // UI : [캐릭터 선택을 모두 마쳤습니다. 전투를 시작하시겠습니까?]
    // [예]를 클릭한 경우 호출
    public void ConfirmPlayer1Units()
    {

    }

    public void RandomizePlayer2Units()
    {

    }
}

