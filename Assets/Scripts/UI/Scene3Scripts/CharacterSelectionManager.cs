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
    public Button cancelButton; // ��� ��ư 
    public GameObject confirmationUI; // 6�� ���� �� ǥ���� UI
    public TMP_Text characterName;
    public Transform parentObject;
    
    private List<string> selectedCharacters = new List<string>(); // ������ ĳ���� list 

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
        cancelButton.onClick.AddListener(OnCancelButtonClick);
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
            // 6���� ���õǾ����� Ȯ�� UI ǥ��
            confirmationUI.SetActive(true);
        }
        else
        {
            Debug.Log($"Select 6 characters (now: {selectedCharacters.Count})");
        }
    }

    // ���� ��ư Ŭ�� �� ����Ǵ� �Լ�
    void OnDeselectButtonClick()
    {
        selectedCharacters.Remove(characterName.text);
    }
   
    void OnCancelButtonClick()
    {
        string currentCharacterName = characterName.text;
        selectedCharacters.Remove(currentCharacterName);
            // ���� �ֱٿ� ������ ĳ������ ��ư ���� ������� 
        confirmationUI.SetActive(false);
    }
}
