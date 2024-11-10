using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FactionSelectionManager : MonoBehaviour
{
    // ������ ���� (Inspector���� �Ҵ�)
    public GameObject GuwolButton;
    public GameObject SeoButton;
    public GameObject BackButton;

    private void Awake()
    {
        GameManager.Instance.gameState = GameState.FactionSelection;
        // Debug.Log("FactionSelectionScene �ε��");

        if (GuwolButton == null)
            Debug.LogError("StartButton�� �Ҵ���� ����", GuwolButton);
        if (SeoButton == null)
            Debug.LogError("QuitButton�� �Ҵ���� ����", SeoButton);
        if (BackButton == null)
            Debug.LogError("SettingsButton�� �Ҵ���� ����", BackButton);
    }

    private void Start()
    {
        Button guwolBtnComponent = GuwolButton.GetComponent<Button>();
        Button seoBtnComponent = SeoButton.GetComponent<Button>();
        Button backBtnComponent = BackButton.GetComponent<Button>();

        guwolBtnComponent.onClick.AddListener(OnGuwolButtonClicked);
        seoBtnComponent.onClick.AddListener(OnSeoButtonClicked);
        backBtnComponent.onClick.AddListener(OnBackButtonClicked);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AudioManager.instance.PlayEffect("successButton");
            OnBackButtonClicked();
        }
    }

    // Hero
    private void OnGuwolButtonClicked()
    {
        AudioManager.instance.PlayEffect("successButton");
        GameManager.Instance.playerFaction = Faction.Guwol;
        SceneManager.LoadScene("UnitSelectionScene");
    }

    // Devil
    private void OnSeoButtonClicked()
    {
        AudioManager.instance.PlayEffect("successButton");
        GameManager.Instance.playerFaction = Faction.Seo;
        SceneManager.LoadScene("UnitSelectionScene");
    }

    // ���� ��ư
    private void OnBackButtonClicked()
    {
        //AudioManager.instance.PlayEffect("successButton");
        SceneManager.LoadScene("MainScene");
    }
}
