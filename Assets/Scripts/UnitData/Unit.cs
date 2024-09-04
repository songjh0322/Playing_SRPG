using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ������ �߻� Ŭ���� (�� ������ �̸� ��ӹ���)
public abstract class Unit
{
    // ��� ������ �Ʒ��� �Ӽ��� ������
    public string name { get; protected set; }
    public int health { get; protected set; }
    public int att_point { get; protected set; }
    public int def_point { get; protected set; }
    public int move_range { get; protected set; }
}
