using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TileEnums;

public class InitialDeployManager1 : MonoBehaviour
{
    public enum State
    {
        NotSelected,
        Selected,
    }

    public static InitialDeployManager1 Instance { get; private set; }

    public State state;
    public List<int> deployedUnitsCodes;
    public List<GameObject> playerUnitPrefabs;

    public GameObject ActiveUnits;
    public Button completeButton;
    public GameObject inGameManager;

    private void Awake()
    {
        GameManager.Instance.gameState = GameState.InitialDeployment;

        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        state = State.NotSelected;
        deployedUnitsCodes = new List<int>();
        playerUnitPrefabs = new List<GameObject>();

        completeButton.onClick.AddListener(() => OnCompleteButtonClicked());

        // 랜덤 배치 실행
        RandomDeployUnits(InitialDeployment.Player1, UnitManager.Instance.player1Units);
        RandomDeployUnits(InitialDeployment.Player2, UnitManager.Instance.player2Units);
    }

    private void OnCompleteButtonClicked()
    {
        inGameManager.SetActive(true);
        gameObject.SetActive(false);
    }

    public void RandomDeployUnits(InitialDeployment deploymentArea, List<Unit> units)
    {
        List<TileInfo> deployableTiles = MapManager.Instance.GetTileInfos(deploymentArea);
        deployableTiles = ShuffleList(deployableTiles);

        for (int i = 0; i < units.Count; i++)
        {
            if (i >= deployableTiles.Count)
            {
                Debug.LogError("배치할 타일이 부족합니다.");
                return;
            }

            TileInfo targetTile = deployableTiles[i];
            targetTile.unit = units[i];

            GameObject unitPrefab = UnitPrefabManager.Instance.InstantiateUnitPrefab(units[i].basicStats.unitCode, 2.0f, false);
            unitPrefab.transform.position = targetTile.worldXY;
            unitPrefab.transform.SetParent(ActiveUnits.transform);

            targetTile.unitPrefab = unitPrefab;
            playerUnitPrefabs.Add(unitPrefab);
        }
    }

    private List<T> ShuffleList<T>(List<T> list)
    {
        System.Random random = new System.Random();
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = random.Next(i, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }
}
