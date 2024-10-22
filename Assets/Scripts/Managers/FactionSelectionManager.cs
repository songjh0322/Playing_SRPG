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
        Debug.Log("FactionScene �ε�");

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

    // ���� ��ư
    private void OnGuwolButtonClicked()
    {
        GameManager.Instance.playerFaction = Faction.Guwol;
        SceneManager.LoadScene("UnitSelectionScene");
    }

    // ���� ��ư
    private void OnSeoButtonClicked()
    {
        GameManager.Instance.playerFaction = Faction.Seo;
        SceneManager.LoadScene("UnitSelectionScene");
    }

    // ���� ��ư
    private void OnBackButtonClicked()
    {
        SceneManager.LoadScene("MainScene");
    }
}
