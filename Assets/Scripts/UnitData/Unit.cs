using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// 각 유닛의 기본 능력치 구조체 (인게임에서 절대 불변함)
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

    public int faction;
    public int maxHealth;
    public int attackPoint;
    public int defensePoint;
    public int moveRange;
}

// 각 유닛의 인터페이스
public interface IUnit
{
    void move();
    void castSkill(string unitName);
}

// 인게임에서 객체로 만들 Unit 클래스
[Serializable]
public class Unit : IUnit
{
    // 각 유닛의 기본 능력치 (인게임에서 절대 불변함)
    public BasicStats basicStats;

    // 인게임에서 활용될 능력치 (패시브나 스킬 등을 통해 플레이 도중 변경될 수 있음)
    public int currentHealth;
    public int currentAttackPoint;
    public int currentDefensePoint;
    public int currentMoveRange;

    // Unit 생성자 (현재 능력치는 기본 능력치로부터 복사하여 생성됨)
    public Unit(BasicStats basicStats)
    {
        this.basicStats = basicStats;
        this.currentHealth = basicStats.maxHealth;
        this.currentAttackPoint = basicStats.attackPoint;
        this.currentDefensePoint = basicStats.defensePoint;
        this.currentMoveRange = basicStats.moveRange;
    }

    // 복사 생성자 (능력치와 현재 상태가 모두 같지만 완전히 새로운 유닛을 생성)
    public Unit(Unit originalUnit)
    {
        this.basicStats = originalUnit.basicStats;
        this.currentHealth = originalUnit.currentHealth;
        this.currentAttackPoint = originalUnit.currentAttackPoint;
        this.currentDefensePoint = originalUnit.currentDefensePoint;
        this.currentMoveRange = originalUnit.currentMoveRange;
    }

    /*    public Unit(string unitName, Dictionary<string, BasicStats> basicStatsDic)
        {
            if (basicStatsData.ContainsKey(unitName))
            {
                BasicStats stats = basicStatsData[unitName];

                // 변경되지 않는 능력치
                this.unitName = stats.unitName;
                this.characterDescription = stats.characterDescription;
                this.passiveName = stats.passiveName;
                this.passiveDescription = stats.passiveDescription;
                this.skillName1 = stats.skillName1;
                this.skillDescription1 = stats.skillDescription1;
                this.skillName2 = stats.skillName2;
                this.skillDescription2 = stats.skillDescription2;
                this.maxHealth = stats.maxHealth;
                this.attackPoint = stats.attackPoint;
                this.defensePoint = stats.defensePoint;
                this.moveRange = stats.moveRange;

                // 인게임에서 사용될 능력치 설정 (모두 기본 능력치를 기반으로 설정)
                this.currentHealth = maxHealth;
                this.currentAttackPoint = attackPoint;
                this.currentDefensePoint = defensePoint;
                this.currentMoveRange = moveRange;
            }
            else
            {
                Debug.LogError("CharacterStats.JSON에 해당 캐릭터가 없습니다: " + unitName);
            }
        }*/

    public (int, int) GetPosition()
    {
        MapManager mapManager = MapManager.Instance;

        return (0, 0);
    }

    public void move()
    {
        
    }

    public void castSkill(string skillName)
    {
        // this는 현재 캐릭터 유닛을 나타냄
        throw new NotImplementedException();
    }
}