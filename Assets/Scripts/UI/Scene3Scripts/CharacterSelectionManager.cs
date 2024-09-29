using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Xml;
using UnityEngine.TextCore.Text;
using System;

public class CharacterSelectionManager : MonoBehaviour
{
    public static CharacterSelectionManager Instance { get; private set; }
    GameManager gameManager;
    UnitManager unitManager;

    // �̱��� �ν��Ͻ� ����
    public CharacterSelectionManager()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public List<Button> characterButtons; // ĳ���� ��ư�� (8��)
    public TMP_Text characterName;        // ĳ���� �̸�

    public Button selectButton; // ���� ��ư
    public Button deselectButton; // ���� ���� ��ư 
    public Button cancelButton; // ��� ��ư 
    public GameObject confirmationUI; // 6�� ���� �� ǥ���� UI

    private Dictionary<string, BasicStats> refereceBasicStats;  // Player1 ���� ������ BasicStats ��ųʸ�
    private List<string> keyList;   // ĳ���� �̸��� ������� �����ϴ� List

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
    TMP_FontAsset maplestoryFont;

    public List<string> selectedCharacters = new List<string>(); // ������ ĳ���� �̸� List 

    public List<string> SelectedCharacters
    {
        get { return selectedCharacters; }  // ����Ʈ�� ��ȯ�ϴ� getter

        set { selectedCharacters = value; }  // ����Ʈ�� ���� �����ϴ� setter
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        unitManager = UnitManager.Instance;
        gameManager = GameManager.Instance;
        

        // ���� �⺻ �ɷ�ġ �� ��Ʈ �ε� (���İ�Ƽ �ڵ�... ���� ����)
        maplestoryFont = Resources.Load<TMP_FontAsset>("Fonts/Orbit-Regular SDF");

        if (unitManager == null)
            print("null");
        unitManager.LoadBasicStatsFromJSON(); // JSON ������ �ε�

        statusText = GameObject.Find("StatusText");
        storyText = GameObject.Find("StoryText");
        passiveText = GameObject.Find("PassiveText");
        skill1Text = GameObject.Find("Skill1Text");
        skill2Text = GameObject.Find("Skill2Text");
        
        // ��� �ؽ�Ʈ�� �ѱ� ��Ʈ ����
        statusText.GetComponent<TMP_Text>().font = maplestoryFont;
        storyText.GetComponent<TMP_Text>().font = maplestoryFont;
        passiveText.GetComponent<TMP_Text>().font = maplestoryFont;
        skill1Text.GetComponent<TMP_Text>().font = maplestoryFont;
        skill2Text.GetComponent<TMP_Text>().font = maplestoryFont;

        // referenceBasicStats�� ������ ���� ĳ������ �⺻ �ɷ�ġ�� ����, keyList�� �� ĳ���� �̸��� ����
        if (gameManager.player1Camp == Player1Camp.Guwol)
        {
            refereceBasicStats = unitManager.guwol_basicStatsData;
            keyList = new List<string>(unitManager.guwol_basicStatsData.Keys);
        }
        else if (gameManager.player1Camp == Player1Camp.Seo)
        {
            refereceBasicStats = unitManager.seo_basicStatsData;
            keyList = new List<string>(unitManager.seo_basicStatsData.Keys);
        }
        else
        {
            Debug.LogError("CharacterSelectionManager Error : ������ �ĺ����� ����");
        }

        int n = 0;
        // �� ĳ���� ��ư�� ĳ���� �̸� �� Ŭ�� �̺�Ʈ �߰�
        foreach (Button characterButton in characterButtons)
        {
            Transform childTransform = characterButton.transform.Find("Text (TMP)");    // ������Ʈ �̸� �����ϸ� �ȵ� !
            textMeshPro = childTransform.GetComponent<TextMeshProUGUI>();
            textMeshPro.font = maplestoryFont;
            textMeshPro.text = keyList[n++];

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

        storyText.GetComponent<TMP_Text>().font = maplestoryFont;
        storyText.GetComponent<TMP_Text>().text = refereceBasicStats[name].characterDescription;
        // ��ų�� �̸��� ����
        skill1Text.GetComponent<TMP_Text>().font = maplestoryFont;
        skill1Text.GetComponent<TMP_Text>().text = refereceBasicStats[name].skillName1
            + "\n" + refereceBasicStats[name].skillDescription1;
        skill2Text.GetComponent<TMP_Text>().font = maplestoryFont;
        skill2Text.GetComponent<TMP_Text>().text = refereceBasicStats[name].skillName2
            + "\n" + refereceBasicStats[name].skillDescription2;
        passiveText.GetComponent<TMP_Text>().font = maplestoryFont;
        passiveText.GetComponent<TMP_Text>().text = refereceBasicStats[name].passiveName
            + "\n" + refereceBasicStats[name].passiveDescription;

        statusText.GetComponent<TMP_Text>().font = maplestoryFont;
        statusText.GetComponent<TMP_Text>().text =
            "HP " + refereceBasicStats[name].maxHealth.ToString()
            + "\nATK " + refereceBasicStats[name].attackPoint.ToString()
            + "\nDEF " + refereceBasicStats[name].defensePoint.ToString()
            + "\nMOV " + refereceBasicStats[name].moveRange.ToString();
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
