using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 각 유닛의 추상 클래스 (각 유닛은 이를 상속받음)
public abstract class Unit
{
    // 모든 유닛은 아래의 속성을 포함함
    public string name { get; protected set; }
    public int health { get; protected set; }
    public int att_point { get; protected set; }
    public int def_point { get; protected set; }
    public int move_range { get; protected set; }
}
