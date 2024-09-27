using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// �� ������ ����
[Serializable]
public class BasicStats
{
    public string unitName;
    public string passiveName;
    public string skillName1;
    public string skillName2;

    public int maxHealth;
    public int attackPoint;
    public int defensePoint;
    public int moveRange;
}

// �� ������ �������̽�
public interface IUnit
{
    void move();
    void castSkill1(string unitName);   // 1�� ��ų
    void castSkill2(string unitName);   // 2�� ��ų
}

// �ΰ��ӿ��� ��ü�� ���� Unit Ŭ����
[Serializable]
public class Unit : IUnit
{
    // ������� �ʴ� �ɷ�ġ (�ܼ� ���ڿ�)
    public string unitName; // Key������ Ȱ���� ĳ���͸�
    public string passiveName;
    public string skillName1;
    public string skillName2;

    // ���� ĳ���� �ɷ�ġ (�н��� ���� ����� �� ������, �÷��� ���߿��� ������� ����)
    public int maxHealth;
    public int attackPoint;
    public int defensePoint;
    public int moveRange;

    // �ΰ��ӿ��� Ȱ��� �ɷ�ġ (�нú곪 ��ų ���� ���� �÷��� ���� ����� �� ����)
    public int currentHealth;
    public int currentAttackPoint;
    public int currentDefensePoint;
    public int currentMoveRange;


    // Unit ������: unitName�� ���� JSON �����Ϳ��� ������ ������
    public Unit(string unitName, Dictionary<string, BasicStats> basicStatsData)
    {
        if (basicStatsData.ContainsKey(unitName))
        {
            BasicStats stats = basicStatsData[unitName];

            // ������� �ʴ� �ɷ�ġ
            this.unitName = stats.unitName;
            this.passiveName = stats.passiveName;
            this.skillName1 = stats.skillName1;
            this.skillName2 = stats.skillName2;
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
    }

    public void move()
    {
        throw new NotImplementedException();
    }

    public void castSkill1(string unitName)
    {
        throw new NotImplementedException();
    }

    public void castSkill2(string unitName)
    {
        throw new NotImplementedException();
    }
}