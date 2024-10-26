using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum Team
{
    Ally,
    Enemy
}

// 각 유닛의 기본 능력치 구조체 (인게임에서 절대 불변)
[Serializable]
public struct BasicStats
{
    public string unitName;
    public string characterDescription;
    public string passiveName;
    public string passiveDescription;
    public string skillName1;
    public string skillDescription1;
    public string skillName2;
    public string skillDescription2;

    public int unitCode;
    public Faction faction;
    public int maxHealth;
    public int attackPoint;
    public int defensePoint;
    public int moveRange;
    public int attackRange;
}

/*// 각 유닛의 인터페이스
public interface IUnit
{
    void move(Tile toTile);
    void castSkill(string unitName);
}*/

// 인게임에서 객체로 만들 Unit 클래스
[Serializable]
public class Unit// : IUnit
{
    // 각 유닛의 기본 능력치 (프로그램에서 불변)
    public BasicStats basicStats;

    // 선택을 통해 부여되는 요소
    public Team team;

    // 인게임에서 활용될 능력치 (인게임 도중 변경될 수 있음)
    public int currentHealth;
    public int currentAttackPoint;
    public int currentDefensePoint;
    public int currentMoveRange;
    public int currentAttackRange;

    // Unit 생성자 (현재 능력치는 기본 능력치로부터 복사하여 생성됨)
    public Unit(BasicStats basicStats)
    {
        this.basicStats = basicStats;
        this.currentHealth = basicStats.maxHealth;
        this.currentAttackPoint = basicStats.attackPoint;
        this.currentDefensePoint = basicStats.defensePoint;
        this.currentMoveRange = basicStats.moveRange;
        this.currentAttackRange = basicStats.attackRange;
    }

    // 복사 생성자 (능력치와 현재 상태가 모두 같지만 완전히 새로운 유닛을 생성)
    public Unit(Unit originalUnit)
    {
        this.basicStats = originalUnit.basicStats;
        this.currentHealth = originalUnit.currentHealth;
        this.currentAttackPoint = originalUnit.currentAttackPoint;
        this.currentDefensePoint = originalUnit.currentDefensePoint;
        this.currentMoveRange = originalUnit.currentMoveRange;
        this.currentAttackRange = originalUnit.currentAttackRange;
    }

    public (int, int) GetPosition()
    {
        MapManager mapManager = MapManager.Instance;

        return (0, 0);
    }

    public void castSkill(string skillName)
    {
        
    }
}