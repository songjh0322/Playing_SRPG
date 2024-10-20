using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// 게임의 전반적인 흐름 및 전역 변수 관리
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerFaction playerFaction;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            gameObject.AddComponent<UnitManager>();
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            foreach (Unit unit in UnitManager.Instance.player1Units)
                Debug.Log($"{unit.basicStats.unitName}");
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            foreach (Unit unit in UnitManager.Instance.player2Units)
                Debug.Log($"{unit.basicStats.unitName}");
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(CharacterSelectionManager.Instance.selectedCharacters.Count);
            Debug.Log(CharacterSelectionManager.Instance.selectedCharacters[0]);
        }
    }
}

public enum PlayerFaction
{
    Guwol,
    Seo,
}
