using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Xml;
using UnityEngine.TextCore.Text;

public class CharacterSelectionManager : MonoBehaviour
{
    GameManager gameManager;
    UnitManager unitManager;

    public List<Button> characterButtons; // ĳ���� ��ư�� (8��)
    public TMP_Text characterName;        // ĳ���� �̸�

    public Button selectButton; // ���� ��ư
    public Button deselectButton; // ���� ���� ��ư 
    public Button cancelButton; // ��� ��ư 
    public GameObject confirmationUI; // 6�� ���� �� ǥ���� UI

    public Transform parentObject;
    TextMeshProUGUI textMeshPro;
    TMP_FontAsset maplestoryFont;

    GameObject statusText;
    GameObject storyText;
    GameObject passiveText;
    GameObject skill1Text;
    GameObject skill2Text;

    private List<string> selectedCharacters = new List<string>(); // ������ ĳ���� list 

    public List<string> SelectedCharacters
    {
        get { return selectedCharacters; }  // ����Ʈ�� ��ȯ�ϴ� getter

        set { selectedCharacters = value; }  // ����Ʈ�� ���� �����ϴ� setter
    }
    

    void Start()
    {
        statusText = GameObject.Find("StatusText");
        storyText = GameObject.Find("StoryText");
        passiveText = GameObject.Find("PassiveText");
        skill1Text = GameObject.Find("Skill1Text");
        skill2Text = GameObject.Find("Skill2Text");

        gameManager = GameManager.Instance;
        unitManager = UnitManager.GetInstance();

        // ���İ�Ƽ �ڵ�... ���� ���� ����
        maplestoryFont = Resources.Load<TMP_FontAsset>("Fonts/Maplestory OTF Bold SDF");
        unitManager.LoadBasicStatsFromJSON(); // JSON ������ �ε�

        List<string> keyList;
        if (gameManager.player1Camp == Player1Camp.Guwol)
            keyList = new List<string>(unitManager.guwol_basicStatsData.Keys);
        else if (gameManager.player1Camp == Player1Camp.Seo)
            keyList = new List<string>(unitManager.seo_basicStatsData.Keys);
        else
            keyList = null; // ���� ���
        
        int n = 0;
        // �� ĳ���� ��ư�� ĳ���� �̸� �� Ŭ�� �̺�Ʈ �߰�
        foreach (Button characterButton in characterButtons)
        {
            Transform childTransform = characterButton.transform.Find("Text (TMP)");    // �̸� �����ϸ� �ȵ� !
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

    // ĳ���� ��ư Ŭ�� �� ����Ǵ� �Լ�
    void OnCharacterButtonClick(Button clickedButton)
    {
        string currentCharacterName = clickedButton.GetComponentInChildren<TMP_Text>().text;
        characterName.text = currentCharacterName;
        // Load selected character's status, skills, chracterImage

        Transform childTransform = clickedButton.transform.Find("Text (TMP)");
        string name = childTransform.GetComponent<TextMeshProUGUI>().text;
        if (gameManager.player1Camp == Player1Camp.Guwol)
        {
            skill1Text.GetComponent<TMP_Text>().font = maplestoryFont;
            skill1Text.GetComponent<TMP_Text>().text = unitManager.guwol_basicStatsData[name].skillName1;
            skill2Text.GetComponent<TMP_Text>().font = maplestoryFont;
            skill2Text.GetComponent<TMP_Text>().text = unitManager.guwol_basicStatsData[name].skillName2;

        }
        else if (gameManager.player1Camp == Player1Camp.Seo)
        {
             
        }

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
