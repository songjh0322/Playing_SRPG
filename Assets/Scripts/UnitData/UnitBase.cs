using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 각 유닛의 추상 클래스 (각 유닛은 이를 상속받아 구현됨)
public abstract class Unit
{
    // 모든 유닛은 아래의 속성을 반드시 포함함
    public string name { get; protected set; }      // 캐릭터 이름 (식별자)
    public int health { get; protected set; }       // 체력
    public int att_point { get; protected set; }    // 공격력
    public int def_point { get; protected set; }    // 방어력
    public int move_range { get; protected set; }   // 이동 범위
}

// 각 유닛의 인터페이스
public interface IUnit
{
    void attack();
    void move();
    void castSkill();   // 일반적으로 유닛마다 2개의 스킬을 가질 예정
}