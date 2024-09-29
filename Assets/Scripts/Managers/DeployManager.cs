using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ��ġ�� �����ϴ� �Ŵ���
public class DeployManager : MonoBehaviour
{
    public static DeployManager Instance { get; private set; }
    UnitManager unitManager;
    CharacterSelectionManager characterSelectionManager;

    // �̱��� �ν��Ͻ� ����
    public DeployManager()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    
    private void Start()
    {
        unitManager = UnitManager.Instance;
        characterSelectionManager = CharacterSelectionManager.Instance;

        unitManager.ConfirmPlayer1Units(characterSelectionManager.selectedCharacters);
    }


}
