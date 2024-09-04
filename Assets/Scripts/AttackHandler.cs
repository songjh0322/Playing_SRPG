using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    private PlayerStats attacker;
    private PlayerStats target;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // ���� Ŭ������ ������ ����
        {
            SelectAttacker();
        }
        else if (Input.GetMouseButtonDown(1))  // ������ Ŭ������ ��� ����
        {
            SelectTarget();
            if (attacker != null && target != null)
            {
                PerformAttack();
            }
        }
    }

    void SelectAttacker()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            attacker = hit.collider.GetComponent<PlayerStats>();
            if (attacker != null)
            {
                Debug.Log(attacker.name + " has been selected as the attacker.");
            }
        }
    }

    void SelectTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            target = hit.collider.GetComponent<PlayerStats>();
            if (target != null)
            {
                Debug.Log(target.name + " has been selected as the target.");
            }
        }
    }

    void PerformAttack()
    {
        int damage = Mathf.Max(attacker.AttackPower - target.Defense, 0);  // �������� ���ݷ� - ����, �ּ� 0
        target.TakeDamage(damage);

        Debug.Log(attacker.name + " attacked " + target.name + " for " + damage + " damage.");
        Debug.Log(target.name + "'s remaining HP: " + target.CurrentHP);

        // ���� �� �ʱ�ȭ
        attacker = null;
        target = null;
    }
}