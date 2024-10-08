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

    GameObject characterButtons_OB;
    List<Button> characterButtonComponents;
    Button characterButtonComponent;
    Button completeButtonComponent;

    // ������Ʈ
    TMP_FontAsset hangeulFont;
    TMP_Text textMeshPro;

    // foreach �ε����� ����
    private int idx;

    private void Start()
    {
        // �÷��̾ ������ ���ֵ��� �����ϰ� AI �÷��̾ maxUnits(6)���� ������ ������
        UnitManager.Instance.ConfirmPlayer1Units(CharacterSelectionManager.Instance.selectedCharacters);
        UnitManager.Instance.RandomizePlayer2Units();

        // ���̻� ������� �ʴ� CharacterSelectionManager�� ����
        GameObject characterSelectionManager = GameObject.Find("@CharacterSelectionManager");
        if (characterSelectionManager != null)
            Destroy(characterSelectionManager);

        // ��Ʈ �ε�
        hangeulFont = Resources.Load<TMP_FontAsset>("Fonts/Orbit-Regular SDF");

        // ���� ������Ʈ ���� (�̸����� ã��)
        characterButtons_OB = GameObject.Find("CharacterButtons");
        Transform characterButtons_TR = characterButtons_OB.transform;

        // ��ư�� ĳ���� �̸� ��ġ
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
        Debug.Log("�Ϸ� ��ư Ŭ����");

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Test!");
        }
    }
}
