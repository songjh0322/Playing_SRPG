using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FactionSelectionManager : MonoBehaviour
{
    public GameObject GuwolButton;
    public GameObject SeoButton;
    public GameObject BackButton;

    private void Awake()
    {
        Debug.Log("FactionScene 로드");

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

    // 시작 버튼
    private void OnGuwolButtonClicked()
    {
        GameManager.Instance.playerFaction = Faction.Guwol;
        SceneManager.LoadScene("UnitSelectionScene");
    }

    // 종료 버튼
    private void OnSeoButtonClicked()
    {
        GameManager.Instance.playerFaction = Faction.Seo;
        SceneManager.LoadScene("UnitSelectionScene");
    }

    // 설정 버튼
    private void OnBackButtonClicked()
    {
        SceneManager.LoadScene("MainScene");
    }
}
