using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class AIManager : MonoBehaviour
{
    public static AIManager Instance { get; private set; }

    private void Awake()
    {
        Debug.Log("AIManager가 생성됨");

        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // 랜덤하게 5명을 추출
        RandomSelection(5);
        /*Debug.Log(UnitManager.Instance.player2Units[0].basicStats.unitName);
        Debug.Log(UnitManager.Instance.player2Units[1].basicStats.unitName);
        Debug.Log(UnitManager.Instance.player2Units[2].basicStats.unitName);
        Debug.Log(UnitManager.Instance.player2Units[3].basicStats.unitName);
        Debug.Log(UnitManager.Instance.player2Units[4].basicStats.unitName);*/
    }

    // player2Units에 랜덤한 유닛을 생성하고 추가
    public void RandomSelection(int num)
    {
        List<int> aiFactionUnitCodes = new List<int>();

        if (GameManager.Instance.playerFaction == Faction.Guwol)
            aiFactionUnitCodes = UnitManager.Instance.GetUnitCodes(Faction.Seo);
        else if (GameManager.Instance.playerFaction == Faction.Seo)
            aiFactionUnitCodes = UnitManager.Instance.GetUnitCodes(Faction.Guwol);

        List<int> randomSelectionUnitCodes = aiFactionUnitCodes
            .OrderBy(x => Guid.NewGuid())
            .Take(num)
            .ToList();
        randomSelectionUnitCodes.Sort();

        foreach (int unitCode in randomSelectionUnitCodes)
        {
            UnitManager.Instance.player2UnitCodes.Add(unitCode);
            UnitManager.Instance.player2Units.Add(new(UnitManager.Instance.GetUnit(unitCode)));
        }   
    }
}
