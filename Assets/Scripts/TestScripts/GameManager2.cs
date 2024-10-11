using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 instance;  // 싱글톤 패턴으로 모든 씬에서 GamaManager에 쉽게 접근할 수 있도록 함.

    public Text characterNameText; //캐릭터 이름을 출력할 텍스트 UI

    public Text selectedCharacterText; //선택된 캐릭터 목록을 표시할 텍스트 UI

    private List<String> selectedCharacters = new List<String>(); //선택된 캐릭터들을 저장할 리스트
    private int maxSelectableCharacters = 5; // 최대 선택 가능한 캐릭터 수
    private void Awake()
    {
        //싱글톤 패턴
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //씬이 바뀌어도 GameManager가 파괴되지 않도록..

        }
        else
        {
            Destroy(gameObject);
        }
    }
    //씬 전환 함수
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }

    //캐릭터 이름을 출력하는 함수
    public void SetCharacterName(string characterName)
    {
        if (characterNameText != null)
        {
            characterNameText.text = characterName; //해당 UI에 캐릭터 이름 출력
            Debug.Log("선택된 캐릭터: " + characterName);
        }
    }
    // 선택한 캐릭터를 저장하는 함수
    public void SelectCharacter()
    {
        if (characterNameText != null)
        {
            string selectedChar = characterNameText.text;

            // 캐릭터가 이미 선택되지 않았고, 최대 선택 수를 넘지 않았을 때만 저장
            if (!selectedCharacters.Contains(selectedChar))
            {
                if (selectedCharacters.Count < maxSelectableCharacters)
                {
                    selectedCharacters.Add(selectedChar);
                    UpdateSelectedCharactersUI();  // UI 업데이트
                    Debug.Log("저장된 캐릭터: " + selectedChar);
                }
                else
                {
                    // 캐릭터가 5명을 넘었을 때 오류 메시지 출력
                    Debug.LogWarning("오류: 최대 5명의 캐릭터만 선택할 수 있습니다.");
                }
            }
        }
    }
    // 선택된 캐릭터들을 UI에 업데이트하는 함수
    private void UpdateSelectedCharactersUI()
    {
        if (selectedCharacterText != null)
        {
            selectedCharacterText.text = "";  // 초기화

            foreach (string character in selectedCharacters)
            {
                selectedCharacterText.text += character + "\n";  // 각 캐릭터를 줄바꿈으로 추가
            }
        }
    }


}
