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

    // 싱글톤 인스턴스 설정
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

    public List<Button> characterButtons; // 캐릭터 버튼들 (8명)
    public TMP_Text characterName;        // 캐릭터 이름

    public Button selectButton; // 선택 버튼
    public Button deselectButton; // 선택 해제 버튼 
    public Button cancelButton; // 취소 버튼 
    public GameObject confirmationUI; // 6명 선택 시 표시할 UI

    // 텍스트 관련 오브젝트
    private GameObject storyText;
    private GameObject statusText;
    private GameObject passiveText;
    private GameObject skill1Text;
    private GameObject skill2Text;
    
    //현재 선택된 버튼을 추적하기 위한 변수
    private Button currentSelectedButton = null;

    // 오브젝트의 텍스트 관련 컴포넌트
    TextMeshProUGUI textMeshPro;
    TMP_FontAsset hangeulFont;

    public List<string> selectedCharacters = new List<string>(); // 선택한 캐릭터 이름 List 

    void Start()
    {
        // 폰트 로드
        hangeulFont = Resources.Load<TMP_FontAsset>("Fonts/Orbit-Regular SDF");

        statusText = GameObject.Find("StatusText");
        storyText = GameObject.Find("StoryText");
        passiveText = GameObject.Find("PassiveText");
        skill1Text = GameObject.Find("Skill1Text");
        skill2Text = GameObject.Find("Skill2Text");
        
        // 모든 텍스트에 한글 폰트 적용
        statusText.GetComponent<TMP_Text>().font = hangeulFont;
        storyText.GetComponent<TMP_Text>().font = hangeulFont;
        passiveText.GetComponent<TMP_Text>().font = hangeulFont;
        skill1Text.GetComponent<TMP_Text>().font = hangeulFont;
        skill2Text.GetComponent<TMP_Text>().font = hangeulFont;

        int n = 0;
        // 각 캐릭터 버튼에 캐릭터 이름 및 클릭 이벤트 추가
        foreach (Button characterButton in characterButtons)
        {
            Transform childTransform = characterButton.transform.Find("Text (TMP)");    // 오브젝트 이름 변경하면 안됨 !
            textMeshPro = childTransform.GetComponent<TextMeshProUGUI>();
            textMeshPro.font = hangeulFont;
            textMeshPro.text = UnitManager.Instance.basicStatsList[n++].unitName;

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

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(selectedCharacters[0]);
        }*/
    }

    // 캐릭터 버튼 클릭 시 실행되는 함수 (캐릭터 정보 출력)
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
        // 스킬의 이름과 설명
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

    // 선택 버튼 클릭 시 실행되는 함수
    void OnSelectButtonClick()
    {
        // get current character name
        string currentCharacterName = characterName.text;
        if (!selectedCharacters.Contains(currentCharacterName))
        {
            selectedCharacters.Add(currentCharacterName);
                // Change the selected character's button UI
             // 선택한 버튼의 색상을 변경
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
        //selectedCharacters.Remove(characterName.text);
        string currentCharacterName = characterName.text;
        if (selectedCharacters.Contains(currentCharacterName))
        {
            selectedCharacters.Remove(currentCharacterName);

            // 선택 해제 시 버튼 색상을 원래대로 변경
            if (currentSelectedButton != null)
            {
                ColorBlock colors = currentSelectedButton.colors;
                colors.normalColor = Color.white;  // 원래 색상으로 변경
                currentSelectedButton.colors = colors;
            }
        }
    }
   
    void OnCancelButtonClick()
    {
        string currentCharacterName = characterName.text;
        selectedCharacters.Remove(currentCharacterName);
            // 가장 최근에 선택한 캐릭터의 버튼 색을 원래대로 
        confirmationUI.SetActive(false);
    }
}
