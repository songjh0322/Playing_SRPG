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
        Debug.Log("MainScene �ε�");

        if (StartButton == null)
            Debug.LogError("StartButton�� �Ҵ���� ����", StartButton);
        if (QuitButton == null)
            Debug.LogError("QuitButton�� �Ҵ���� ����", QuitButton);
        if (SettingsButton == null)
            Debug.LogError("SettingsButton�� �Ҵ���� ����", SettingsButton);
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

    // ���� ��ư
    private void OnStartButtonClicked()
    {
        SceneManager.LoadScene("FactionSelectionScene");
    }

    // ���� ��ư
    private void OnQuitButtonClicked()
    {
        Debug.Log("���� ����");
    }

    // ���� ��ư
    private void OnSettingsButtonClicked()
    {
        
    }




}
