using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FactionSelectionManager : MonoBehaviour
{
    // 프리팹 관리 (Inspector에서 할당)
    public GameObject GuwolButton;
    public GameObject SeoButton;
    public GameObject BackButton;

    private void Awake()
    {
        GameManager.Instance.gameState = GameState.FactionSelection;
        // Debug.Log("FactionSelectionScene 로드됨");

        if (GuwolButton == null)
            Debug.LogError("StartButton이 할당되지 않음", GuwolButton);
        if (SeoButton == null)
            Debug.LogError("QuitButton이 할당되지 않음", SeoButton);
        if (BackButton == null)
            Debug.LogError("SettingsButton이 할당되지 않음", BackButton);
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

    // 설정 버튼
    private void OnBackButtonClicked()
    {
        //AudioManager.instance.PlayEffect("successButton");
        SceneManager.LoadScene("MainScene");
    }
}
