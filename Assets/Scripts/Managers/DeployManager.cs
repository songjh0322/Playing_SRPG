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

    GameObject characterButton;

    List<Button> characterButtons;

    // 컴포넌트
    TextMeshProUGUI textMeshPro;
    TMP_FontAsset hangeulFont;

    private void Start()
    {
        // 플레이어가 선택한 유닛들을 설정하고 AI 플레이어가 maxUnits(6)개의 유닛을 선택함
        UnitManager.Instance.ConfirmPlayer1Units(CharacterSelectionManager.Instance.selectedCharacters);
        UnitManager.Instance.RandomizePlayer2Units();

        // 더이상 사용하지 않는 CharacterSelectionManager를 삭제
        GameObject characterSelectionManager = GameObject.Find("@CharacterSelectionManager");
        if (characterSelectionManager != null)
            Destroy(characterSelectionManager);

        hangeulFont = Resources.Load<TMP_FontAsset>("Fonts/Orbit-Regular SDF");

        // 게임 오브젝트 지정 (이름으로 찾음)
        characterButton = GameObject.Find("CharacterButtons");

        // 모든 캐릭터 버튼에 이름을 표기
        for (int i = 0; i < 6; i++)
        {
            Transform characterButtonTransform = characterButton.transform.GetChild(i);
            Transform textTransform = characterButtonTransform.Find("Text (TMP)");
            TMP_Text textMeshPro = textTransform.GetComponent<TMP_Text>();
            textMeshPro.text = UnitManager.Instance.player1Units[i].basicStats.unitName;
            textMeshPro.font = hangeulFont;
        }
    }


}
