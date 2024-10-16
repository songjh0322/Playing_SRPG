using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 instance;  // 싱글톤 패턴으로 모든 씬에서 GameManager에 쉽게 접근할 수 있도록 함.
    public List<GameObject> characterPrefabs; // 각 캐릭터의 프리팹을 미리 연결해둘 리스트
    public List<GameObject> selectedCharacterPrefabs = new List<GameObject>(); // 선택된 캐릭터의 프리팹 저장 리스트

    public Text characterNameText; // 캐릭터 이름을 출력할 텍스트 UI
    private List<string> selectedCharacters = new List<string>(); // 선택된 캐릭터들을 저장할 리스트
    public List<CharacterStats> selectedCharacterStats = new List<CharacterStats>(); // 캐릭터 스탯 저장
    private int maxSelectableCharacters = 5; // 최대 선택 가능한 캐릭터 수

    public Image[] selectedCharacterImages; // 선택된 캐릭터 정보를 표시할 Content 밑의 5개의 Image 오브젝트
    public Text[] selectedCharacterTexts; // 선택된 캐릭터 이름을 표시할 Text 오브젝트 배열 (Image에 붙은 Text)
    public Image[] characterButtons; // 캐릭터 버튼들의 Image 배열 (각각의 주황색 버튼을 관리)
    public Button[] DeleteButtons; // 삭제 버튼 배열


    private int currentSelectedIndex = -1; // 현재 선택된 캐릭터의 인덱스 (-1은 선택되지 않은 상태)
    private string currentSelectedName = ""; // 선택된 캐릭터의 이름

    public BGMoveManager bgMoveManager; // BGMoveManager 참조
    public LeftPanelMoveManager leftPanelMoveManager;  // LeftPanelMoveManager 참조
    private int placedCharacterCount = 0;

    private void Awake()
    {
        // 싱글톤 패턴
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 GameManager가 파괴되지 않도록..
        }
        else
        {
            Destroy(gameObject);
        }
        // 씬 로드 이벤트에 OnSceneLoaded 함수 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDestroy()
    {
        // 씬 로드 이벤트에서 함수 제거 (중복 등록 방지)
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        ResetSelectedCharacterPrefabs();  // 게임 시작 시 프리팹 리스트 초기화
        // 게임 시작 시 모든 Image와 Text를 비활성화
        ResetSelectedCharactersUI();
    }

    // 씬 전환 함수
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }

    // 게임 시작 시 모든 캐릭터 선택 창을 비활성화하는 함수
    private void ResetSelectedCharactersUI()
    {
        for (int i = 0; i < selectedCharacterImages.Length; i++)
        {
            selectedCharacterImages[i].enabled = false;  // 이미지 비활성화
            selectedCharacterTexts[i].text = "";         // 텍스트 초기화
            DeleteButtons[i].gameObject.SetActive(false); // 삭제 버튼 비활성화
        }
    }

    // 캐릭터 인덱스를 설정하는 함수 (OnClick에서 인덱스 전달)
    public void SetCharacterIndex(int characterIndex)
    {
        currentSelectedIndex = characterIndex;
    }

    // 캐릭터 이름을 설정하는 함수 (OnClick에서 이름 전달)
    public void SetCharacterName(string characterName)
    {
        if (characterNameText != null)
        {
            characterNameText.text = characterName; // UI에 캐릭터 이름 출력
            currentSelectedName = characterName;    // 선택된 캐릭터 이름을 저장
            Debug.Log("선택된 캐릭터: " + characterName + " 인덱스: " + currentSelectedIndex);
        }
    }

    // 선택한 캐릭터를 저장하는 함수 (Select 버튼을 눌렀을 때 호출됨)
    public void ConfirmCharacterSelection()
    {
        if (currentSelectedIndex != -1 && currentSelectedName != "")
        {
            // 캐릭터가 이미 선택되지 않았고, 최대 선택 수를 넘지 않았을 때만 저장
            if (!selectedCharacters.Contains(currentSelectedName))
            {
                if (selectedCharacters.Count < maxSelectableCharacters)
                {
                    selectedCharacters.Add(currentSelectedName);
                    Debug.Log("" + currentSelectedName);
                    //스탯 매니저를 통해 선택된 캐릭터의 스탯 가져오기
                    CharacterStats stats = CharacterStatsManager.instance.GetCharacterStats(currentSelectedName);
                    if (stats != null)
                    {
                        selectedCharacterStats.Add(stats); //스탯 저장
                    }
                    // 선택된 캐릭터 이름에 맞는 프리팹을 가져와서 리스트에 저장
                    GameObject prefab = FindCharacterPrefabByName(currentSelectedName);
                    if (prefab != null)
                    {
                        selectedCharacterPrefabs.Add(prefab); // 프리팹 저장
                    }
                    else { Debug.Log("프리팹 저장 안 됨"); }
                    UpdateSelectedCharactersUI();  // UI 업데이트
                    ChangeButtonColor(currentSelectedIndex);  // 버튼 색상 변경
                    Debug.Log("저장된 캐릭터: " + currentSelectedName);
                }
                else
                {
                    // 캐릭터가 5명을 넘었을 때 오류 메시지 출력
                    Debug.LogWarning("5명의 캐릭터가 이미 선택되었습니다.");
                }
            }
            else
            {
                Debug.LogWarning("이 캐릭터는 이미 선택되었습니다.");
            }
        }
    }
    // 캐릭터 삭제 함수
    public void RemoveCharacter(int index)
    {
        if (index >= 0 && index < selectedCharacters.Count)
        {
            Debug.Log("캐릭터 삭제: " + selectedCharacters[index]);

            // // 버튼 색상 초기화
            // for (int i = 0; i < characterButtons.Length; i++)
            // {
            //     // 캐릭터 이름이 동일하면 해당 캐릭터 버튼의 색상만 초기화
            //     if (characterButtons[i].transform.Find("Text").GetComponent<Text>().text == selectedCharacters[index])
            //     {
            //         Debug.Log("RemoveCharacter ButtonColor");
            //         characterButtons[i].color = new Color32(190, 104, 69, 255);  // 원래 색상으로 변경
            //         break;
            //     }
            // }
            selectedCharacters.RemoveAt(index);  // 리스트에서 해당 캐릭터 삭제
            UpdateSelectedCharactersUI();        // UI 업데이트
        }
    }

    // 선택된 캐릭터들을 UI에 업데이트하는 함수
    private void UpdateSelectedCharactersUI()
    {
        for (int i = 0; i < selectedCharacterImages.Length; i++)
        {
            if (i < selectedCharacters.Count)
            {
                // 선택된 캐릭터 정보가 있을 경우, 이미지와 텍스트 업데이트
                selectedCharacterTexts[i].text = selectedCharacters[i];  // 이름 업데이트
                selectedCharacterImages[i].enabled = true;  // 이미지 활성화
                DeleteButtons[i].gameObject.SetActive(true); // 삭제 버튼 활성화

                // 삭제 버튼의 OnClick 이벤트 설정
                int index = i;  // 로컬 변수로 i 저장
                DeleteButtons[i].onClick.RemoveAllListeners();  // 기존 리스너 제거
                DeleteButtons[i].onClick.AddListener(() => RemoveCharacter(index));  // 삭제 함수 호출 연결
            }
            else
            {
                // 선택되지 않은 칸은 비워두거나 비활성화
                selectedCharacterTexts[i].text = "";
                selectedCharacterImages[i].enabled = false;  // 이미지 비활성화
                DeleteButtons[i].gameObject.SetActive(false); // 삭제 버튼 비활성화
            }
        }

        // 선택된 캐릭터의 텍스트와 버튼이 일치하는지 확인하고 색상 초기화
        for (int i = 0; i < characterButtons.Length; i++)
        {
            string buttonCharacterName = characterButtons[i].transform.Find("Text").GetComponent<Text>().text;
            if (!selectedCharacters.Contains(buttonCharacterName))
            {
                characterButtons[i].color = new Color32(190, 104, 69, 255);  // 원래 색상으로 변경
            }
        }
    }

    // 버튼 색상을 변경하는 함수
    public void ChangeButtonColor(int characterIndex)
    {
        if (characterIndex >= 0 && characterIndex < characterButtons.Length)
        {
            characterButtons[characterIndex].color = new Color32(46, 162, 176, 255);  // #2EA2B0 색상
            Debug.Log("Button color changed for character: " + characterIndex);
        }
    }

    // 씬이 로드될 때 호출되는 함수
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 전환되면 새로 생성된 텍스트 오브젝트를 찾음
        if (scene.name == "TestScene3")
        {
            int numberOfTexts = 5;

            // 반복문을 사용해 각 텍스트 오브젝트를 찾아 배열에 저장
            for (int i = 0; i < numberOfTexts; i++)
            {
                // 오브젝트 이름을 "CharacterText1", "CharacterText2", ..., "CharacterText5" 형식으로 동적으로 생성
                string objectName = "CharacterText" + (i + 1);
                selectedCharacterTexts[i] = GameObject.Find(objectName).GetComponent<Text>();
                selectedCharacterTexts[i].text = selectedCharacters[i];
                Debug.Log("find " + objectName);
            }


            bgMoveManager = FindObjectOfType<BGMoveManager>(); // TestScene3에서 BGMoveManager 찾기
            if (bgMoveManager != null)
            {
                Debug.Log("BGMoveManager가 설정되었습니다.");
            }
            else
            {
                Debug.LogError("BGMoveManager를 찾을 수 없습니다.");
            }
            leftPanelMoveManager = FindObjectOfType<LeftPanelMoveManager>();
        }
    }
    // 캐릭터 이름에 따라 프리팹을 찾는 함수
    private GameObject FindCharacterPrefabByName(string characterName)
    {
        foreach (GameObject prefab in characterPrefabs)
        {
            if (prefab.name == characterName) // 이름으로 프리팹을 찾음
            {
                return prefab;
            }
        }
        Debug.LogWarning("프리팹을 찾을 수 없습니다: " + characterName);
        return null;
    }
    private void ResetSelectedCharacterPrefabs()
    {
        selectedCharacterPrefabs.Clear(); // 기존 리스트를 초기화
    }
    // 캐릭터가 배치될 때마다 호출
    public void OnCharacterPlaced()
    {
        placedCharacterCount++;

        if (placedCharacterCount == 5)
        {
            if (bgMoveManager != null)
            {
                // 5명의 캐릭터가 모두 배치되었을 때 BG를 이동
                bgMoveManager.MoveBG();
            }
            else
            {
                Debug.LogError("BGMoveManager가 설정되지 않았습니다.");
            }
            // 왼쪽 패널 이동
            if (leftPanelMoveManager != null)
            {
                leftPanelMoveManager.MoveLeftPanel();
                Debug.Log("왼쪽 패널 이동 중...");
            }
            else
            {
                Debug.LogError("LeftPanelMoveManager를 찾을 수 없습니다.");
            }
        }
        else
        {
            Debug.Log("현재 배치된 캐릭터 수: " + placedCharacterCount);
        }
    }
}