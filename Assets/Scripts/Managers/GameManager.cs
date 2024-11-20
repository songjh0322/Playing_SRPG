using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// ������ �������� �帧 �� ���� ���� ����
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState gameState;
    public Faction playerFaction;
    public string targetSceneName = "LearningScene";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            gameObject.AddComponent<UnitManager>();
            gameObject.AddComponent<UnitPrefabManager>();
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        UnitManager.Instance.LoadBasicStatsFromJSON();
        UnitManager.Instance.LoadAllUnits();
        
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene != targetSceneName)
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    public void ResetGame()
    {

    }
}

public enum Faction
{
    Guwol,
    Seo,
}

public enum GameState
{
    MainMenu,
    FactionSelection,
    UnitSelection,
    InitialDeployment,
    InGame
}
