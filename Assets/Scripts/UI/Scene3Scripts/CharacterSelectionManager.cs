using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectionManager : MonoBehaviour
{
    public List<Button> characterButtons; // ĳ���� ��ư�� (8��)
    public Button selectButton; // ���� ��ư
    public GameObject confirmationUI; // 6�� ���� �� ǥ���� UI
    //public Text selectedCharactersText; // ���õ� ĳ���� ����� ǥ���� �ؽ�Ʈ

    private List<string> selectedCharacters = new List<string>(); // ���õ� ĳ���� ����Ʈ

    void Start()
    {
        // �� ĳ���� ��ư�� Ŭ�� �̺�Ʈ �߰�
        foreach (Button characterButton in characterButtons)
        {
            characterButton.onClick.AddListener(() => OnCharacterButtonClick(characterButton));
        }

        // ���� ��ư Ŭ�� �̺�Ʈ �߰�
        selectButton.onClick.AddListener(OnSelectButtonClick);

        // �ʱ�ȭ
        confirmationUI.SetActive(false);
        // UpdateSelectedCharactersText();
    }

    // ĳ���� ��ư Ŭ�� �� ����Ǵ� �Լ�
    void OnCharacterButtonClick(Button clickedButton)
    {
        string characterName = clickedButton.GetComponentInChildren<TMP_Text>().text;

        if (selectedCharacters.Contains(characterName))
        {
            // �̹� ���õ� ĳ���͸� ���� ����
            selectedCharacters.Remove(characterName);
            clickedButton.image.color = Color.white; // ��ư ���� �ʱ�ȭ
        }
        else if (selectedCharacters.Count < 6)
        {
            // 6�� �̸��� ���� ���� ����
            selectedCharacters.Add(characterName);
            clickedButton.image.color = Color.green; // ���õ� ĳ���ʹ� �ʷϻ����� ǥ��
        }

        //UpdateSelectedCharactersText();
    }

    // ���� ��ư Ŭ�� �� ����Ǵ� �Լ�
    void OnSelectButtonClick()
    {
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

    // ���õ� ĳ���� ����� ������Ʈ�ϴ� �Լ�
    // void UpdateSelectedCharactersText()
    // {
    //     selectedCharactersText.text = "���õ� ĳ����: " + string.Join(", ", selectedCharacters);
    // }
}
