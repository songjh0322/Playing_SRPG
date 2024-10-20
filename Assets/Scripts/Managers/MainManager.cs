using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public GameObject StartButton;
    public GameObject QuitButton;
    public GameObject SettingsButton;

    private void Awake()
    {
        Debug.Log("MainScene 로드");

        if (StartButton == null)
            Debug.LogError("StartButton이 할당되지 않음", StartButton);
        if (QuitButton == null)
            Debug.LogError("QuitButton이 할당되지 않음", QuitButton);
        if (SettingsButton == null)
            Debug.LogError("SettingsButton이 할당되지 않음", SettingsButton);
    }

    private void Start()
    {
        Button startBtnComponent = StartButton.GetComponent<Button>();
        Button quitBtnComponent = QuitButton.GetComponent<Button>();
        Button settingsBtnComponent = SettingsButton.GetComponent<Button>();

        startBtnComponent.onClick.AddListener(OnStartButtonClicked);
        quitBtnComponent.onClick.AddListener(OnQuitButtonClicked);
        settingsBtnComponent.onClick.AddListener(OnSettingsButtonClicked);
    }

    // 시작 버튼
    private void OnStartButtonClicked()
    {
        SceneManager.LoadScene("FactionSelectionScene");
    }

    // 종료 버튼
    private void OnQuitButtonClicked()
    {
        Debug.Log("게임 종료");
    }

    // 설정 버튼
    private void OnSettingsButtonClicked()
    {
        
    }




}
