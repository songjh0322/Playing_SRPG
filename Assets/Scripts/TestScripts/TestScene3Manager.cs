using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScene3Manager : MonoBehaviour
{
    public Text[] characterButtonsText; //저장된 캐릭터 정보를 담을 버튼 5개
    private List<CharacterStats> selectedCharacterStats;
    private void Start()
    {
        // //GamaManager2에서 넘어온 캐릭터 정보를 가져옴
        // selectedCharacterStats = GameManager2.instance.selectedCharacterStats;

        // // 선택된 캐릭터들을 버튼에 표시
        // for (int i = 0; i < selectedCharacterStats.Count; i++)
        // {
        //     characterButtonsText[i].GetComponentInChildren<Text>().text = selectedCharacterStats[i].characterName;

        // }
        selectedCharacterStats = GameManager2.instance.selectedCharacterStats;

        if (selectedCharacterStats != null)
        {
            Debug.Log("Selected characters count: " + selectedCharacterStats.Count);
        }

        for (int i = 0; i < selectedCharacterStats.Count; i++)
        {
            characterButtonsText[i].text = selectedCharacterStats[i].characterName;
            Debug.Log("Character name: " + selectedCharacterStats[i].characterName);
        }
    }
}