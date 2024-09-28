using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager
{
    public static SceneManager Instance { get; private set; }

    // ���

    // �̱��� �ν��Ͻ��� ��ȯ
    public static SceneManager GetInstance()
    {
        if (Instance == null)
        {
            Instance = new SceneManager();
        }
        return Instance;
    }

    // private ������ (�ܺο��� ���� �������� ���ϵ��� ��)
    private SceneManager()
    {
        // UI ���� �ʱ�ȭ
    }

    
}
