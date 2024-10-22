using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 유닛 배치를 관리하는 매니저
public class DeployManager : MonoBehaviour
{
    public static DeployManager Instance { get; private set; }

    private void Awake()
    {
        Debug.Log("DeployScene 로드");

        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        /*Debug.Log(UnitManager.Instance.player1Units[0].basicStats.unitName);
        Debug.Log(UnitManager.Instance.player1Units[1].basicStats.unitName);
        Debug.Log(UnitManager.Instance.player1Units[2].basicStats.unitName);
        Debug.Log(UnitManager.Instance.player1Units[3].basicStats.unitName);
        Debug.Log(UnitManager.Instance.player1Units[4].basicStats.unitName);*/
    }
}
