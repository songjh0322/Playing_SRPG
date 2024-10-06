using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ��ġ�� �����ϴ� �Ŵ���
public class DeployManager : MonoBehaviour
{
    public static DeployManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // �÷��̾� ������ ���ֵ��� ���
        UnitManager.Instance.ConfirmPlayer1Units(CharacterSelectionManager.Instance.selectedCharacters);
        UnitManager.Instance.RandomizePlayer2Units();
    }


}
