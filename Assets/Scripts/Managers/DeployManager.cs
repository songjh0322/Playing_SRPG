using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 유닛 배치를 관리하는 매니저
public class DeployManager : MonoBehaviour
{
    public static DeployManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    GameObject characterButtons_OB;
    List<Button> characterButtonComponents;
    Button characterButtonComponent;
    Button completeButtonComponent;

    // 컴포넌트
    TMP_FontAsset hangeulFont;
    TMP_Text textMeshPro;

    // foreach 인덱스용 변수
    private int idx;

    private void Start()
    {
        // 플레이어가 선택한 유닛들을 설정하고 AI 플레이어가 maxUnits(6)개의 유닛을 선택함
        UnitManager.Instance.ConfirmPlayer1Units(CharacterSelectionManager.Instance.selectedCharacters);
        UnitManager.Instance.RandomizePlayer2Units();

        // 더이상 사용하지 않는 CharacterSelectionManager를 삭제
        GameObject characterSelectionManager = GameObject.Find("@CharacterSelectionManager");
        if (characterSelectionManager != null)
            Destroy(characterSelectionManager);

        // 폰트 로드
        hangeulFont = Resources.Load<TMP_FontAsset>("Fonts/Orbit-Regular SDF");

        // 게임 오브젝트 지정 (이름으로 찾음)
        characterButtons_OB = GameObject.Find("CharacterButtons");
        Transform characterButtons_TR = characterButtons_OB.transform;

        // 버튼에 캐릭터 이름 배치
        idx = 0;
        foreach (Transform characterButtonXX_TR in characterButtons_TR)
        {
            Transform textTMP_TR = characterButtonXX_TR.Find("Text (TMP)");
            textMeshPro = textTMP_TR.GetComponent<TMP_Text>();
            textMeshPro.text = UnitManager.Instance.player1Units[idx++].basicStats.unitName;
            textMeshPro.font = hangeulFont;
        }

        completeButtonComponent.onClick.AddListener(OnCompleteButtonClick);
    }

    private void OnCharacterButtonClick(Unit currentUnit)
    {
        Debug.Log(currentUnit.basicStats.unitName);
    }

    private void OnCompleteButtonClick()
    {
        Debug.Log("완료 버튼 클릭됨");

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Test!");
        }
    }
}
