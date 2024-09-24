using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        // �ʰ� UI �ʱ�ȭ
        MapManager.Instance.InitializeMap();
        UIManager.Instance.InitializeUI();
    }

    private void Start()
    {
        // ������ ���۵� �� �ʿ��� �߰� �۾� ����
        Debug.Log("GameManager: ���� ����");
    }

    // Update �Լ��� �ʿ��� �� ���
    private void Update()
    {
        // ���� ���� ������Ʈ
    }
}

