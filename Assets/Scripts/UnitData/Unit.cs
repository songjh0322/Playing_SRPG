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
    public string passiveDescription;
    public string skillName1;
    public string skillDescription1;
    public string skillName2;
    public string skillDescription2;

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
    // ������� �ʴ� �ɷ�ġ (�ܼ� ���ڿ�)
    public string unitName; // Key������ Ȱ���� ĳ���͸�
    public string passiveName;
    public string passiveDescription;
    public string skillName1;
    public string skillDescription1;
    public string skillName2;
    public string skillDescription2;

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
    }

    public (int, int) GetPosition()
    {
        MapManager mapManager = MapManager.GetInstance();

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