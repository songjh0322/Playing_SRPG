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

            Debug.Log("������� �����!");

            // Collider�� �ִ��� Ȯ��
            if (hit.collider != null)
            {
                // Ŭ���� ������Ʈ�� Box���� Ȯ��
                if (hit.collider.gameObject == gameObject)
                {
                    Debug.Log("Box clicked!");
                }
            }
        }
    }
}
