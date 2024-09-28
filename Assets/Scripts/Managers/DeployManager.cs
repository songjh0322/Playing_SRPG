using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ��ġ�� �����ϴ� �Ŵ���
public class DeployManager
{
    public static DeployManager Instance { get; private set; }

    // �̱��� �ν��Ͻ� ����
    private DeployManager()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // �̱��� �ν��Ͻ��� ��ȯ
    public static DeployManager GetInstance()
    {
        if (Instance == null)
        {
            Instance = new DeployManager();
        }
        return Instance;
    }

    // ��� : ��ġ ���� �� ���� ���� ȣ��
    // ��� : 
    public void StartDeploy()
    {
        
    }

    // ��� : ��ġ �Ϸ� �� ���������� ȣ��
    // ��� : 
    public void CompleteDeploy()
    {

    }
}
