using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ���� ��ġ�� �����ϴ� �Ŵ���
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
    List<Button> characterButtonComponents;
    Button characterButtonComponent;
    Button completeButtonComponent;

    // ������Ʈ
    TextMeshProUGUI textMeshPro;
    TMP_FontAsset hangeulFont;

    private void Start()
    {
        // �÷��̾ ������ ���ֵ��� �����ϰ� AI �÷��̾ maxUnits(6)���� ������ ������
        UnitManager.Instance.ConfirmPlayer1Units(CharacterSelectionManager.Instance.selectedCharacters);
        UnitManager.Instance.RandomizePlayer2Units();

        // ���̻� ������� �ʴ� CharacterSelectionManager�� ����
        GameObject characterSelectionManager = GameObject.Find("@CharacterSelectionManager");
        if (characterSelectionManager != null)
            Destroy(characterSelectionManager);

        hangeulFont = Resources.Load<TMP_FontAsset>("Fonts/Orbit-Regular SDF");

        // ���� ������Ʈ ���� (�̸����� ã��)
        characterButton = GameObject.Find("CharacterButtons");

        // ��� ĳ���� ��ư�� �̸��� ǥ�� (���� ������ ĳ���� �̸��� ���������� ���Ե��� ����)
        for (int i = 0; i < 6; i++)
        {
            Transform characterButtonTransform = characterButton.transform.GetChild(i);
            Transform textTransform = characterButtonTransform.Find("Text (TMP)");
            TMP_Text textMeshPro = textTransform.GetComponent<TMP_Text>();

            textMeshPro.text = UnitManager.Instance.player1Units[i].basicStats.unitName;
            textMeshPro.font = hangeulFont;

            Unit currentUnit = UnitManager.Instance.player1Units[i];
            characterButtonComponent = GetComponent<Button>();
            characterButtonComponent.onClick.AddListener(() => OnCharacterButtonClick(currentUnit));
        }

        completeButtonComponent.onClick.AddListener(OnCompleteButtonClick);
    }

    private void OnCharacterButtonClick(Unit currentUnit)
    {
        Debug.Log(currentUnit.basicStats.unitName);
    }

    private void OnCompleteButtonClick()
    {
        Debug.Log("�Ϸ� ��ư Ŭ����");
    }
}
