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

    // 싱글톤 인스턴스 설정
    public CharacterSelectionManager()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public List<Button> characterButtons; // 캐릭터 버튼들 (8명)
    public TMP_Text characterName;        // 캐릭터 이름

    public Button selectButton; // 선택 버튼
    public Button deselectButton; // 선택 해제 버튼 
    public Button cancelButton; // 취소 버튼 
    public GameObject confirmationUI; // 6명 선택 시 표시할 UI

    private Dictionary<string, BasicStats> refereceBasicStats;  // Player1 진영 유닛의 BasicStats 딕셔너리
    private List<string> keyList;   // 캐릭터 이름을 순서대로 저장하는 List

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
    TMP_FontAsset maplestoryFont;

    public List<string> selectedCharacters = new List<string>(); // 선택한 캐릭터 이름 List 

    public List<string> SelectedCharacters
    {
        get { return selectedCharacters; }  // 리스트를 반환하는 getter

        set { selectedCharacters = value; }  // 리스트에 값을 설정하는 setter
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        unitManager = UnitManager.Instance;
        gameManager = GameManager.Instance;
        

        // 유닛 기본 능력치 및 폰트 로드 (스파게티 코드... 수정 예정)
        maplestoryFont = Resources.Load<TMP_FontAsset>("Fonts/Orbit-Regular SDF");

        if (unitManager == null)
            print("null");
        unitManager.LoadBasicStatsFromJSON(); // JSON 데이터 로드

        statusText = GameObject.Find("StatusText");
        storyText = GameObject.Find("StoryText");
        passiveText = GameObject.Find("PassiveText");
        skill1Text = GameObject.Find("Skill1Text");
        skill2Text = GameObject.Find("Skill2Text");
        
        // 모든 텍스트에 한글 폰트 적용
        statusText.GetComponent<TMP_Text>().font = maplestoryFont;
        storyText.GetComponent<TMP_Text>().font = maplestoryFont;
        passiveText.GetComponent<TMP_Text>().font = maplestoryFont;
        skill1Text.GetComponent<TMP_Text>().font = maplestoryFont;
        skill2Text.GetComponent<TMP_Text>().font = maplestoryFont;

        // referenceBasicStats에 선택한 진영 캐릭터의 기본 능력치를 저장, keyList에 각 캐릭터 이름을 저장
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
            Debug.LogError("CharacterSelectionManager Error : 진영이 식별되지 않음");
        }

        int n = 0;
        // 각 캐릭터 버튼에 캐릭터 이름 및 클릭 이벤트 추가
        foreach (Button characterButton in characterButtons)
        {
            Transform childTransform = characterButton.transform.Find("Text (TMP)");    // 오브젝트 이름 변경하면 안됨 !
            textMeshPro = childTransform.GetComponent<TextMeshProUGUI>();
            textMeshPro.font = maplestoryFont;
            textMeshPro.text = keyList[n++];

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

        storyText.GetComponent<TMP_Text>().font = maplestoryFont;
        storyText.GetComponent<TMP_Text>().text = refereceBasicStats[name].characterDescription;
        // 스킬의 이름과 설명
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
