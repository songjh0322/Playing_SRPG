using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 불변하는 데이터를 저장하는 클래스
public class BaseStats
{
    public readonly string Name;

    public readonly int MaxHealth;
    public readonly int AttackPoint;
    public readonly int DefensePoint;
    public readonly int MoveRange;

    public BaseStats(string name, int maxHealth, int attackPoint, int defensePoint, int moveRange)
    {
        Name = name;
        MaxHealth = maxHealth;
        AttackPoint = attackPoint;
        DefensePoint = defensePoint;
        MoveRange = moveRange;
    }
}

// 게임 진행 중 변경되는 스탯을 저장하는 클래스 (BaseStats로부터 초기값을 받음)
public class CurrentStats
{
    public int MaxHealth { get; set; }
    public int Health { get; set; }
    public int AttackPoint { get; set; }
    public int DefensePoint { get; set; }
    public int MoveRange { get; set; }

    public CurrentStats(BaseStats baseStats)
    {
        // 기본 스탯을 현재 스탯으로 초기화
        MaxHealth = baseStats.MaxHealth;
        Health = baseStats.MaxHealth;       // 현재 체력을 최대 체력으로 설정
        AttackPoint = baseStats.AttackPoint;
        DefensePoint = baseStats.DefensePoint;
        MoveRange = baseStats.MoveRange;
    }
}

// 각 유닛의 추상 클래스 (각 유닛은 이를 상속받아 구현됨)
public abstract class Unit : MonoBehaviour
{
    public BaseStats BaseStats { get; private set; }
    public CurrentStats CurrentStats { get; private set; }

    // 생성자
    protected Unit(BaseStats baseStats)
    {
        BaseStats = baseStats;
        CurrentStats = new CurrentStats(baseStats);
    }

    // 기본 공격 로직
    public virtual void attack(Unit target)
    {
        Debug.Log($"{this.name}이(가) 공격력 {this.CurrentStats.AttackPoint}로 {target.name}을(를) 공격했습니다!");
        
        if (this.CurrentStats.AttackPoint > target.CurrentStats.DefensePoint)
        {
            int realDamage = this.CurrentStats.AttackPoint - target.CurrentStats.DefensePoint;
            target.CurrentStats.Health -= realDamage;
            Debug.Log($"{target.name}의 현재 체력 : {target.CurrentStats.Health}");
        }
        else
        {
            Debug.Log($"{this.name}의 공격력보다 {target.name}의 수비력이 더 높습니다.");
        }
        
    }

    // 기본 이동 로직
    public virtual void move()
    {
        Debug.Log($"{this.name}이(가) {this.CurrentStats.MoveRange}칸만큼 이동했습니다.");
    }

    // 스킬은 각 캐릭터마다 다르므로 추상 메서드로 선언
    public abstract void castSkill();
}

// 각 유닛의 인터페이스
public interface IUnit
{
    void attack();
    void move();
    void castSkill();   // 일반적으로 유닛마다 2개의 스킬을 가질 예정
}