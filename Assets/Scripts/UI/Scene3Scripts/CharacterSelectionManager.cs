using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Xml;
using UnityEngine.TextCore.Text;
using System;
using System.Linq;

public class CharacterSelectionManager : MonoBehaviour
{
    public static CharacterSelectionManager Instance { get; private set; }

    // �̱��� �ν��Ͻ� ����
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<Button> characterButtons; // ĳ���� ��ư�� (8��)
    public TMP_Text characterName;        // ĳ���� �̸�

    public Button selectButton; // ���� ��ư
    public Button deselectButton; // ���� ���� ��ư 
    public Button cancelButton; // ��� ��ư 
    public GameObject confirmationUI; // 6�� ���� �� ǥ���� UI

    // �ؽ�Ʈ ���� ������Ʈ
    private GameObject storyText;
    private GameObject statusText;
    private GameObject passiveText;
    private GameObject skill1Text;
    private GameObject skill2Text;
    
    //���� ���õ� ��ư�� �����ϱ� ���� ����
    private Button currentSelectedButton = null;

    // ������Ʈ�� �ؽ�Ʈ ���� ������Ʈ
    TextMeshProUGUI textMeshPro;
    TMP_FontAsset hangeulFont;

    public List<string> selectedCharacters = new List<string>(); // ������ ĳ���� �̸� List 

    void Start()
    {
        // ��Ʈ �ε�
        hangeulFont = Resources.Load<TMP_FontAsset>("Fonts/Orbit-Regular SDF");

        statusText = GameObject.Find("StatusText");
        storyText = GameObject.Find("StoryText");
        passiveText = GameObject.Find("PassiveText");
        skill1Text = GameObject.Find("Skill1Text");
        skill2Text = GameObject.Find("Skill2Text");
        
        // ��� �ؽ�Ʈ�� �ѱ� ��Ʈ ����
        statusText.GetComponent<TMP_Text>().font = hangeulFont;
        storyText.GetComponent<TMP_Text>().font = hangeulFont;
        passiveText.GetComponent<TMP_Text>().font = hangeulFont;
        skill1Text.GetComponent<TMP_Text>().font = hangeulFont;
        skill2Text.GetComponent<TMP_Text>().font = hangeulFont;

        int n = 0;
        // �� ĳ���� ��ư�� ĳ���� �̸� �� Ŭ�� �̺�Ʈ �߰�
        foreach (Button characterButton in characterButtons)
        {
            Transform childTransform = characterButton.transform.Find("Text (TMP)");    // ������Ʈ �̸� �����ϸ� �ȵ� !
            textMeshPro = childTransform.GetComponent<TextMeshProUGUI>();
            textMeshPro.font = hangeulFont;
            textMeshPro.text = UnitManager.Instance.basicStatsList[n++].unitName;

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

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(selectedCharacters[0]);
        }*/
    }

    // ĳ���� ��ư Ŭ�� �� ����Ǵ� �Լ� (ĳ���� ���� ���)
    void OnCharacterButtonClick(Button clickedButton)
    {
        currentSelectedButton = clickedButton;
        string currentCharacterName = clickedButton.GetComponentInChildren<TMP_Text>().text;
        characterName.text = currentCharacterName;
        // Load selected character's status, skills, chracterImage

        Transform childTransform = clickedButton.transform.Find("Text (TMP)");
        string name = childTransform.GetComponent<TextMeshProUGUI>().text;

        BasicStats selectedStats = UnitManager.Instance.basicStatsList.FirstOrDefault(stats => stats.unitName == name);

        storyText.GetComponent<TMP_Text>().font = hangeulFont;
        storyText.GetComponent<TMP_Text>().text = selectedStats.characterDescription;
        // ��ų�� �̸��� ����
        skill1Text.GetComponent<TMP_Text>().font = hangeulFont;
        skill1Text.GetComponent<TMP_Text>().text = selectedStats.skillName1
            + "\n" + selectedStats.skillDescription1;
        skill2Text.GetComponent<TMP_Text>().font = hangeulFont;
        skill2Text.GetComponent<TMP_Text>().text = selectedStats.skillName2
            + "\n" + selectedStats.skillDescription2;
        passiveText.GetComponent<TMP_Text>().font = hangeulFont;
        passiveText.GetComponent<TMP_Text>().text = selectedStats.passiveName
            + "\n" + selectedStats.passiveDescription;

        statusText.GetComponent<TMP_Text>().font = hangeulFont;
        statusText.GetComponent<TMP_Text>().text =
            "HP " + selectedStats.maxHealth.ToString()
            + "\nATK " + selectedStats.attackPoint.ToString()
            + "\nDEF " + selectedStats.defensePoint.ToString()
            + "\nMOV " + selectedStats.moveRange.ToString();
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
             // ������ ��ư�� ������ ����
            if (currentSelectedButton != null)
            {
                ColorBlock colors = currentSelectedButton.colors;
                Color selectedColor = new Color(183f / 255f, 163f / 255f, 163f / 255f);

                colors.normalColor = selectedColor;
                colors.highlightedColor = selectedColor;
                colors.pressedColor = selectedColor;
                colors.selectedColor = selectedColor;
                colors.disabledColor = selectedColor;
                
                currentSelectedButton.colors = colors;
            }
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
        //selectedCharacters.Remove(characterName.text);
        string currentCharacterName = characterName.text;
        if (selectedCharacters.Contains(currentCharacterName))
        {
            selectedCharacters.Remove(currentCharacterName);

            // ���� ���� �� ��ư ������ ������� ����
            if (currentSelectedButton != null)
            {
                ColorBlock colors = currentSelectedButton.colors;
                colors.normalColor = Color.white;  // ���� �������� ����
                currentSelectedButton.colors = colors;
            }
        }
    }
   
    void OnCancelButtonClick()
    {
        string currentCharacterName = characterName.text;
        selectedCharacters.Remove(currentCharacterName);
            // ���� �ֱٿ� ������ ĳ������ ��ư ���� ������� 
        confirmationUI.SetActive(false);
    }
}
