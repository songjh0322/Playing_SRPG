using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectionManager : MonoBehaviour
{
    public List<Button> characterButtons; // 캐릭터 버튼들 (8명)
    public Button selectButton; // 선택 버튼
    public Button deselectButton; // 해제 버튼 
    public Button cancelButton; // 취소 버튼 
    public GameObject confirmationUI; // 6명 선택 시 표시할 UI
    public TMP_Text characterName;
    public Transform parentObject;
    
    private List<string> selectedCharacters = new List<string>(); // 선택한 캐릭터 list 

    public List<string> SelectedCharacters
    {
        get { return selectedCharacters; }  // 리스트를 반환하는 getter

        set { selectedCharacters = value; }  // 리스트에 값을 설정하는 setter
    }
    

    void Start()
    {
        // 각 캐릭터 버튼에 클릭 이벤트 추가
        foreach (Button characterButton in characterButtons)
        {
            characterButton.onClick.AddListener(() => OnCharacterButtonClick(characterButton));
        }

        // 선택 버튼 클릭 이벤트 추가
        selectButton.onClick.AddListener(OnSelectButtonClick);
        deselectButton.onClick.AddListener(OnDeselectButtonClick);
        cancelButton.onClick.AddListener(OnCancelButtonClick);
        // 초기화
        confirmationUI.SetActive(false);
        // UpdateSelectedCharactersText();
    }

    // 캐릭터 버튼 클릭 시 실행되는 함수
    void OnCharacterButtonClick(Button clickedButton)
    {
        string currentCharacterName = clickedButton.GetComponentInChildren<TMP_Text>().text;
        characterName.text = currentCharacterName;
            // Load selected character's status, skills, chracterImage
        
    }

    // 선택 버튼 클릭 시 실행되는 함수
    void OnSelectButtonClick()
    {
        // get current character name
        string currentCharacterName = characterName.text;
        if (!selectedCharacters.Contains(currentCharacterName))
        {
            selectedCharacters.Add(currentCharacterName);
                // Change the selected character's button UI
            Debug.Log($"{currentCharacterName} is selected.");
        }
        else
        {
            Debug.Log("Already selected");
        }
        if (selectedCharacters.Count == 6)
        {
            // 6명이 선택되었으면 확인 UI 표시
            confirmationUI.SetActive(true);
        }
        else
        {
            Debug.Log($"Select 6 characters (now: {selectedCharacters.Count})");
        }
    }

    // 해제 버튼 클릭 시 실행되는 함수
    void OnDeselectButtonClick()
    {
        selectedCharacters.Remove(characterName.text);
    }
   
    void OnCancelButtonClick()
    {
        string currentCharacterName = characterName.text;
        selectedCharacters.Remove(currentCharacterName);
            // 가장 최근에 선택한 캐릭터의 버튼 색을 원래대로 
        confirmationUI.SetActive(false);
    }
}
