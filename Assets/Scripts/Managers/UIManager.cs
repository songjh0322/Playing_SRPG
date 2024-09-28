using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public static UIManager Instance { get; private set; }

    // �̱��� �ν��Ͻ��� ��ȯ
    public static UIManager GetInstance()
    {
        if (Instance == null)
        {
            Instance = new UIManager();
        }
        return Instance;
    }

    // private ������ (�ܺο��� ���� �������� ���ϵ��� ��)
    private UIManager()
    {
        // UI ���� �ʱ�ȭ
    }
}
