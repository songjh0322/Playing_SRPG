using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLogTest : MonoBehaviour
{
    void Update()
    {
        // ���콺 ��Ŭ���� ����
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺 ��ġ���� Ray �߻�
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // Ray �ð��� �����
            Debug.DrawRay(mousePosition, Vector2.zero, Color.red, 1.0f);

            // Collider�� �ִ��� Ȯ��
            if (hit.collider != null)
            {
                // Ŭ���� ������Ʈ�� Collider Ȯ��
                //Debug.Log("Collider�� �����Ǿ����ϴ�.");

                // �±װ� "Player"���� Ȯ��
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("Player ������Ʈ�� Ŭ���Ǿ����ϴ�.");
                }
            }
        }
    }
}
