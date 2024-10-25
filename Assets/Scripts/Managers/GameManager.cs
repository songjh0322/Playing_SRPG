using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// 게임의 전반적인 흐름 및 전역 변수 관리
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState gameState;
    public Faction playerFaction;

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

        SceneManager.LoadScene("MainScene");
    }

    // 디버깅용
    private void Update()
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
