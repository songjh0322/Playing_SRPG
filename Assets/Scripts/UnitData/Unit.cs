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
    public int x_pos, y_pos;

    // ������ (���� ���� �� ��ǥ�� �����ؾ� ��)
    protected Unit(BaseStats baseStats, int x_pos, int y_pos)
    {
        BaseStats = baseStats;
        CurrentStats = new CurrentStats(baseStats);
        this.x_pos = x_pos;
        this.y_pos = y_pos;
    }

    // ��ǥ(��� ��)�� Ʃ�÷� ��ȯ
    public (int, int) GetPosition()
    {
        (int, int) position = (x_pos, y_pos);
        return position;
    }

    // ������ ��ǥ(��� ��)�� ����
    public void SetPosition(int x_pos, int y_pos)
    {
        this.x_pos = x_pos;
        this.y_pos = y_pos;
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

    // �ൿ ���� UI : [�̵��ϱ�], [��ų1], [��ų2] UI
    // [�̵��ϱ�] ��ư�� ������ �ش� �Լ��� ȣ��
    // �ൿ ���� UI�� �ݰ�, �ð������� �̵� ������ Ÿ���� ���� �ٸ��� ǥ���Ͽ� ��� (Map�� GetReachableCoordinate() �Լ� �̿�)
    // �� ���� �ƴ� UIManager���� �ۼ��ٶ�, �� move() �Լ��� ȣ��Ǳ� ������ �� ����� ����Ǿ�� ��
    // Ŭ���� ���� Tile ��ü�� ����ٰ� ����(��, fromTile != null�� ���¿��� ȣ��)
    // fromTile : ���� Ŭ���� ���� ���õ� Ÿ��
    // toTile�� ���� : null ���� Ȯ�� -> ��ȿ �Ÿ� Ȯ�� -> �̵� �Ұ� Ÿ�� Ȯ�� -> �̵� ���� -> �� ����
    public virtual void move(BasicMap map, Tile fromTile)
    {


        Tile toTile = null;

        // ��ȿ�� ��ġ�� Ŭ���� ������ ���콺 Ŭ���� �Է¹���
        while (true)
        {
            // �̵��� Ÿ���� ��Ŭ������ Ŭ���Ͽ� �ش� Ÿ�� ��ü�� ���� (Tile�� ���� ��� �� ��ü�� ���������� �����ؾ� ��)
            // toTile = OnclickFunc();

            if (toTile == null)
            {
                // Ÿ���� �ƴ� ������Ʈ�� Ŭ���� ���
                print("Ÿ���� Ŭ���ϼ���.");
            }
            else if (toTile.tileType == TileType.Unreachable || toTile.unit != null)
            {
                // �̵� �Ұ� Ÿ��(TileType.Unreachable)�̰ų� ������ �̹� �����ϴ� Ÿ���� ���
                print("�̵� �Ұ����� Ÿ���Դϴ�.");
            }
            else
            {
                // �̵� ������ Ÿ���� ��� -> ������ �̵� �Ÿ� ���� Ÿ������ Ȯ��
                //int toX =
                //int toY = position.Item2;

                // fromTile�κ��� ��ȿ�� �Ÿ�
                List<(int, int)> reachableCoorinates = map.GetReachableCoordinates(fromTile, this.CurrentStats.MoveDistance, (10, 10));
                
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