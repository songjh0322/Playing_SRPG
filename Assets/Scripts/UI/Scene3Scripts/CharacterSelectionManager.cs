using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectionManager : MonoBehaviour
{
    public List<Button> characterButtons; // 캐릭터 버튼들 (8명)
    public Button selectButton; // 선택 버튼
    public GameObject confirmationUI; // 6명 선택 시 표시할 UI
    //public Text selectedCharactersText; // 선택된 캐릭터 목록을 표시할 텍스트

    private List<string> selectedCharacters = new List<string>(); // 선택된 캐릭터 리스트

    void Start()
    {
        // 각 캐릭터 버튼에 클릭 이벤트 추가
        foreach (Button characterButton in characterButtons)
        {
            characterButton.onClick.AddListener(() => OnCharacterButtonClick(characterButton));
        }

        // 선택 버튼 클릭 이벤트 추가
        selectButton.onClick.AddListener(OnSelectButtonClick);

        // 초기화
        confirmationUI.SetActive(false);
        // UpdateSelectedCharactersText();
    }

    // 캐릭터 버튼 클릭 시 실행되는 함수
    void OnCharacterButtonClick(Button clickedButton)
    {
        string characterName = clickedButton.GetComponentInChildren<TMP_Text>().text;

        if (selectedCharacters.Contains(characterName))
        {
            // 이미 선택된 캐릭터면 선택 해제
            selectedCharacters.Remove(characterName);
            clickedButton.image.color = Color.white; // 버튼 색상 초기화
        }
        else if (selectedCharacters.Count < 6)
        {
            // 6명 미만일 때만 선택 가능
            selectedCharacters.Add(characterName);
            clickedButton.image.color = Color.green; // 선택된 캐릭터는 초록색으로 표시
        }

        //UpdateSelectedCharactersText();
    }

    // 선택 버튼 클릭 시 실행되는 함수
    void OnSelectButtonClick()
    {
        if (selectedCharacters.Count == 6)
        {
            // 6명이 선택되었으면 확인 UI 표시
            confirmationUI.SetActive(true);
        }
        else
        {
            Debug.Log("6명의 캐릭터를 선택해야 합니다.");
        }
    }

    // 선택된 캐릭터 목록을 업데이트하는 함수
    // void UpdateSelectedCharactersText()
    // {
    //     selectedCharactersText.text = "선택된 캐릭터: " + string.Join(", ", selectedCharacters);
    // }
}
