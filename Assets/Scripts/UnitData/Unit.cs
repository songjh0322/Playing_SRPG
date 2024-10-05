using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// �� ������ �⺻ �ɷ�ġ ����ü (�ΰ��ӿ��� ���� �Һ���)
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

// �� ������ �������̽�
public interface IUnit
{
    void move();
    void castSkill(string unitName);
}

// �ΰ��ӿ��� ��ü�� ���� Unit Ŭ����
[Serializable]
public class Unit : IUnit
{
    // �� ������ �⺻ �ɷ�ġ (�ΰ��ӿ��� ���� �Һ���)
    public BasicStats basicStats;

    // �ΰ��ӿ��� Ȱ��� �ɷ�ġ (�нú곪 ��ų ���� ���� �÷��� ���� ����� �� ����)
    public int currentHealth;
    public int currentAttackPoint;
    public int currentDefensePoint;
    public int currentMoveRange;

    // Unit ������ (���� �ɷ�ġ�� �⺻ �ɷ�ġ�κ��� �����Ͽ� ������)
    public Unit(BasicStats basicStats)
    {
        this.basicStats = basicStats;
        this.currentHealth = basicStats.maxHealth;
        this.currentAttackPoint = basicStats.attackPoint;
        this.currentDefensePoint = basicStats.defensePoint;
        this.currentMoveRange = basicStats.moveRange;
    }

    // ���� ������ (�ɷ�ġ�� ���� ���°� ��� ������ ������ ���ο� ������ ����)
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

                // ������� �ʴ� �ɷ�ġ
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

                // �ΰ��ӿ��� ���� �ɷ�ġ ���� (��� �⺻ �ɷ�ġ�� ������� ����)
                this.currentHealth = maxHealth;
                this.currentAttackPoint = attackPoint;
                this.currentDefensePoint = defensePoint;
                this.currentMoveRange = moveRange;
            }
            else
            {
                Debug.LogError("CharacterStats.JSON�� �ش� ĳ���Ͱ� �����ϴ�: " + unitName);
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
        // this�� ���� ĳ���� ������ ��Ÿ��
        throw new NotImplementedException();
    }
}