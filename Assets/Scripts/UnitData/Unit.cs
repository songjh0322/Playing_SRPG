using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
    public PlayerFaction faction;
    public int maxHealth;
    public int attackPoint;
    public int defensePoint;
    public int moveRange;
}

// �� ������ �������̽�
public interface IUnit
{
    void move(Tile toTile);
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

    public (int, int) GetPosition()
    {
        MapManager mapManager = MapManager.Instance;

        return (0, 0);
    }

    public void move(Tile toTile)
    {
        
    }

    public void castSkill(string skillName)
    {
        
    }
}