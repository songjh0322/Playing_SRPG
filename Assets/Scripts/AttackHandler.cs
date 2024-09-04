using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    private PlayerStats attacker;
    private PlayerStats target;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // 왼쪽 클릭으로 공격자 선택
        {
            SelectAttacker();
        }
        else if (Input.GetMouseButtonDown(1))  // 오른쪽 클릭으로 대상 선택
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
        int damage = Mathf.Max(attacker.AttackPower - target.Defense, 0);  // 데미지는 공격력 - 방어력, 최소 0
        target.TakeDamage(damage);

        Debug.Log(attacker.name + " attacked " + target.name + " for " + damage + " damage.");
        Debug.Log(target.name + "'s remaining HP: " + target.CurrentHP);

        // 공격 후 초기화
        attacker = null;
        target = null;
    }
}