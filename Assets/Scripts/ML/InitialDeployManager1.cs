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
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        deployedUnitsCodes = new List<int>();
        playerUnitPrefabs = new List<GameObject>();

    
    }

    private void OnCompleteButtonClicked()
    {
        inGameManager.SetActive(true);
        gameObject.SetActive(false);
    }

}
