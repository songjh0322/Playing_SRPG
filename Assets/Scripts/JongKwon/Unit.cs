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

// �� ������ �⺻ �ɷ�ġ ����ü (�ΰ��ӿ��� ���� �Һ�)
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

/*// �� ������ �������̽�
public interface IUnit
{
    void move(Tile toTile);
    void castSkill(string unitName);
}*/

// �ΰ��ӿ��� ��ü�� ���� Unit Ŭ����
[Serializable]
public class Unit// : IUnit
{
    // �� ������ �⺻ �ɷ�ġ (���α׷����� �Һ�)
    public BasicStats basicStats;

    // ������ ���� �ο��Ǵ� ���
    public Team team;

    // �ΰ��ӿ��� Ȱ��� �ɷ�ġ (�ΰ��� ���� ����� �� ����)
    public int currentHealth;
    public int currentAttackPoint;
    public int currentDefensePoint;
    public int currentMoveRange;
    public int currentAttackRange;

    // Unit ������ (���� �ɷ�ġ�� �⺻ �ɷ�ġ�κ��� �����Ͽ� ������)
    public Unit(BasicStats basicStats)
    {
        this.basicStats = basicStats;
        this.currentHealth = basicStats.maxHealth;
        this.currentAttackPoint = basicStats.attackPoint;
        this.currentDefensePoint = basicStats.defensePoint;
        this.currentMoveRange = basicStats.moveRange;
        this.currentAttackRange = basicStats.attackRange;
    }

    // ���� ������ (�ɷ�ġ�� ���� ���°� ��� ������ ������ ���ο� ������ ����)
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