using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEnums
{
    public enum TileType
    {
        Normal,         // 특성 없음
        Forest,         // 피격 시 회피할 확률 40%, 스킬 사용 시 공격 범위가 3 이상인 경우 -2
        Water,          // 이동 시 이동 범위가 4 이상인 경우 -3 (### 지나갈 경우에도 적용돼야 함 -> 해당 로직 아이디어 필요... ###)
        Unreachable,    // 도달할 수 없는 타일
    }

    // 초기 배치 가능 여부
    public enum InitialDeployment
    {
        None,
        Player1,
        Player2
    }

}
