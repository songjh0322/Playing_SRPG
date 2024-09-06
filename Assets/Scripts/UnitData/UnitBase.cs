using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ������ �߻� Ŭ���� (�� ������ �̸� ��ӹ޾� ������)
public abstract class Unit
{
    // ��� ������ �Ʒ��� �Ӽ��� �ݵ�� ������
    public string name { get; protected set; }      // ĳ���� �̸� (�ĺ���)
    public int health { get; protected set; }       // ü��
    public int att_point { get; protected set; }    // ���ݷ�
    public int def_point { get; protected set; }    // ����
    public int move_range { get; protected set; }   // �̵� ����
}

// �� ������ �������̽�
public interface IUnit
{
    void attack();
    void move();
    void castSkill();   // �Ϲ������� ���ָ��� 2���� ��ų�� ���� ����
}