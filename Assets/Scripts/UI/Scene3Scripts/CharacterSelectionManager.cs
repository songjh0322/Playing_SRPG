using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectionManager : MonoBehaviour
{
    public List<Button> characterButtons; // ĳ���� ��ư�� (8��)
    public Button selectButton; // ���� ��ư
    public Button deselectButton; // ���� ��ư 
    public GameObject confirmationUI; // 6�� ���� �� ǥ���� UI
    public TMP_Text characterName;
    public Transform parentObject;
    private List<string> selectedCharacters = new List<string>(); // ������ ĳ������ �ε��� 
    public List<string> SelectedCharacters
    {
        get { return selectedCharacters; }  // ����Ʈ�� ��ȯ�ϴ� getter

        set { selectedCharacters = value; }  // ����Ʈ�� ���� �����ϴ� setter
    }
    

    void Start()
    {
        // �� ĳ���� ��ư�� Ŭ�� �̺�Ʈ �߰�
        foreach (Button characterButton in characterButtons)
        {
            characterButton.onClick.AddListener(() => OnCharacterButtonClick(characterButton));
        }

        // ���� ��ư Ŭ�� �̺�Ʈ �߰�
        selectButton.onClick.AddListener(OnSelectButtonClick);
        deselectButton.onClick.AddListener(OnDeselectButtonClick);
        // �ʱ�ȭ
        confirmationUI.SetActive(false);
        // UpdateSelectedCharactersText();
    }

    // ĳ���� ��ư Ŭ�� �� ����Ǵ� �Լ�
    void OnCharacterButtonClick(Button clickedButton)
    {
        string currentCharacterName = clickedButton.GetComponentInChildren<TMP_Text>().text;
        characterName.text = currentCharacterName;
        // Load selected character's status, skills, chracterImage
        
    }

    // ���� ��ư Ŭ�� �� ����Ǵ� �Լ�
    void OnSelectButtonClick()
    {
        // get current character name
        string currentCharacterName = characterName.text;
        selectedCharacters.Add(currentCharacterName);
        if (selectedCharacters.Count == 6)
        {
            // 6���� ���õǾ����� Ȯ�� UI ǥ��
            confirmationUI.SetActive(true);
        }
        else
        {
            Debug.Log("6���� ĳ���͸� �����ؾ� �մϴ�.");
        }
    }

    // ���� ��ư Ŭ�� �� ����Ǵ� �Լ�
    void OnDeselectButtonClick()
    {
        selectedCharacters.Remove(characterName.text);
    }
    // ���õ� ĳ���� ����� ������Ʈ�ϴ� �Լ�
    // void UpdateSelectedCharactersText()
    // {
    //     selectedCharactersText.text = "���õ� ĳ����: " + string.Join(", ", selectedCharacters);
    // }
}
