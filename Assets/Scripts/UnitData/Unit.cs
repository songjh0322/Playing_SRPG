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
    public int x, y;

    // ������ (���� ���� �� ��ǥ�� �����ؾ� ��)
    protected Unit(BaseStats baseStats, int x, int y)
    {
        BaseStats = baseStats;
        CurrentStats = new CurrentStats(baseStats);
        this.x = x;
        this.y = y;
    }

    // ��ǥ(��� ��)�� ��ȯ
    public (int, int) GetPosition()
    {
        return (x, y);
    }

    // ������ ��ǥ(��� ��)�� ����
    public void SetPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    /*    // �׽�Ʈ�� ����
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
        }*/

    // �ش� ������ �̹� ������ ���¿��� ȣ�� ([�̵��ϱ�] ��ư�� ������ �� �Լ��� ȣ��)
    // ��� ������ ���� �ൿ�� ����
    public virtual void move(BasicMap map)
    {
        Debug.Log("[�̵��ϱ�] ��ư�� ������");
        // �˾�â�� �ݰ�, �ð������� �̵� ������ Ÿ���� ���� �ٸ��� ǥ��

        Tile selected_tile = null;
        while (true)
        {
            // �̵��� Ÿ���� ��Ŭ������ Ŭ���Ͽ� �ش� Ÿ�� ��ü�� ���� (Tile�� ���� ��� �� ��ü�� ���������� �����ؾ� ��)
            selected_tile =  null;

            if (selected_tile == null)
            {
                // Ŭ������ �ν��� ������Ʈ�� Ÿ���� �ƴ� ��� : �ƹ��͵� ó������ �ʰ� ���� Ŭ���� �Է¹���
                Debug.Log("�̵� �Ÿ� ���� Ÿ���� �����ϼ���.");
            }
            else if (selected_tile.tileType == TileType.Unreachable || selected_tile.unit != null)
            {
                // �̵� �Ұ� Ÿ�� �Ǵ� ������ �̹� �ִ� Ÿ���� ���
                Debug.Log("�̵� �Ұ����� Ÿ���̰ų� �̹� ������ �����ϴ� Ÿ���Դϴ�.");
            }
            else
            {
                // �̵� ������ Ÿ���� ���
                List<(int, int)> list = map.GetReachableTiles(selected_tile, this.CurrentStats.MoveDistance, 10, 10);
                break;
            }
        }
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