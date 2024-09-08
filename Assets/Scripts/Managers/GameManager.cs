using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    private CharacterA characterA;
    private CharacterB characterB;

    void Start()
    {
        Debug.Log("���� �Ŵ��� ������: " + this.gameObject.name);

        // �̸��� "A"�� ������Ʈ�� ã��, �� ������Ʈ�� CharacterA �Ҵ�
        GameObject objectA = GameObject.Find("A");
        if (objectA != null)
        {
            characterA = objectA.AddComponent<CharacterA>();
            Debug.Log("CharacterA�� ������Ʈ A�� �Ҵ�Ǿ����ϴ�.");
        }
        else
        {
            Debug.LogWarning("������Ʈ A�� ã�� �� �����ϴ�.");
        }

        // �̸��� "B"�� ������Ʈ�� ã��, �� ������Ʈ�� CharacterB �Ҵ�
        GameObject objectB = GameObject.Find("B");
        if (objectB != null)
        {
            characterB = objectB.AddComponent<CharacterB>();
            Debug.Log("CharacterB�� ������Ʈ B�� �Ҵ�Ǿ����ϴ�.");
        }
        else
        {
            Debug.LogWarning("������Ʈ B�� ã�� �� �����ϴ�.");
        }
    }

    // ù ��° Ŭ������ ���õ� ������Ʈ�� ������ ����
    private GameObject firstSelectedObject = null;

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
                // ù ��° Ŭ��: CharacterA ������Ʈ ����
                if (firstSelectedObject == null && hit.collider.GetComponent<CharacterA>() != null)
                {
                    firstSelectedObject = hit.collider.gameObject;  // ù ��° ������Ʈ ����
                    Debug.Log("CharacterA�� ���õǾ����ϴ�.");
                }
                // �� ��° Ŭ��: CharacterB ������Ʈ ����
                else if (firstSelectedObject != null && hit.collider.GetComponent<CharacterB>() != null)
                {
                    GameObject secondSelectedObject = hit.collider.gameObject;  // �� ��° ������Ʈ ����
                    Debug.Log("CharacterB�� ���õǾ����ϴ�.");

                    firstSelectedObject.GetComponent<CharacterA>().attack(secondSelectedObject.GetComponent<CharacterB>());

                    // �� ���� Ŭ�� �Ϸ� �� �ʱ�ȭ
                    firstSelectedObject = null;
                }
                // �� ��° Ŭ���� CharacterB�� �ƴ� ��� ���
                else if (firstSelectedObject != null && hit.collider.GetComponent<CharacterB>() == null)
                {
                    Debug.Log("�� ��° Ŭ���� CharacterB���� �մϴ�. �ٽ� �õ��ϼ���.");
                }
            }
        }
    }

}
