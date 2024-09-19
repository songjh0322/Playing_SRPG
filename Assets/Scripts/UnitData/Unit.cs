using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Һ��ϴ� �����͸� �����ϴ� Ŭ����
public class BaseStats
{
    public readonly string Name;

    public readonly int MaxHealth;
    public readonly int AttackPoint;
    public readonly int DefensePoint;
    public readonly int MoveDistance;

    public BaseStats(string name, int maxHealth, int attackPoint, int defensePoint, int moveDistance)
    {
        Name = name;
        MaxHealth = maxHealth;
        AttackPoint = attackPoint;
        DefensePoint = defensePoint;
        MoveDistance = moveDistance;
    }
}

// ���� ���� �� ����Ǵ� ������ �����ϴ� Ŭ���� (BaseStats�κ��� �ʱⰪ�� ����)
public class CurrentStats
{
    public int MaxHealth { get; set; }
    public int Health { get; set; }
    public int AttackPoint { get; set; }
    public int DefensePoint { get; set; }
    public int MoveDistance { get; set; }

    public CurrentStats(BaseStats baseStats)
    {
        // �⺻ ������ ���� �������� �ʱ�ȭ
        MaxHealth = baseStats.MaxHealth;
        Health = baseStats.MaxHealth;       // ���� ü���� �ִ� ü������ ����
        AttackPoint = baseStats.AttackPoint;
        DefensePoint = baseStats.DefensePoint;
        MoveDistance = baseStats.MoveDistance;
    }
}

// �� ������ �߻� Ŭ���� (�� ������ �̸� ��ӹ޾� ������)
public abstract class Unit : MonoBehaviour
{
    public BaseStats BaseStats { get; private set; }
    public CurrentStats CurrentStats { get; private set; }

    // ������
    protected Unit(BaseStats baseStats)
    {
        BaseStats = baseStats;
        CurrentStats = new CurrentStats(baseStats);
    }

    // �׽�Ʈ�� ����
    public virtual void attack(Unit target)
    {
        Debug.Log($"{this.name}��(��) ���ݷ� {this.CurrentStats.AttackPoint}�� {target.name}��(��) �����߽��ϴ�!");
        
        if (this.CurrentStats.AttackPoint > target.CurrentStats.DefensePoint)
        {
            int realDamage = this.CurrentStats.AttackPoint - target.CurrentStats.DefensePoint;
            target.CurrentStats.Health -= realDamage;
            Debug.Log($"{target.name}�� ���� ü�� : {target.CurrentStats.Health}");
        }
        else
        {
            Debug.Log($"{this.name}�� ���ݷº��� {target.name}�� ������� �� �����ϴ�.");
        }
    }

    // �̵�
    public virtual void move()
    {
        Debug.Log($"{this.name}��(��) {this.CurrentStats.MoveDistance}ĭ��ŭ �̵��߽��ϴ�.");
    }

    // ��ų�� �� ĳ���͸��� �ٸ��Ƿ� �߻� �޼���� ����
    public abstract void castSkill1();
    public abstract void castSkill2();
}

// �� ������ �������̽�
public interface IUnit
{
    void attack();
    void move();
    void castSkill1();   // 1�� ��ų
    void castSkill2();   // 2�� ��ų
}