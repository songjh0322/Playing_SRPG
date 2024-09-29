using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 유닛 배치를 관리하는 매니저
public class DeployManager : MonoBehaviour
{
    public static DeployManager Instance { get; private set; }
    UnitManager unitManager;
    CharacterSelectionManager characterSelectionManager;

    // 싱글톤 인스턴스 설정
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
