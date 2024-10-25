using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class AIManager : MonoBehaviour
{
    public static AIManager Instance { get; private set; }

    public int aiUnitNum;
    public List<int> aiUnitCodes;
    public List<Unit> aiUnits;

    private void Awake()
    {
        // Debug.Log("AIManager가 생성됨");

        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // 랜덤하게 5명을 추출
        RandomSelection(5);

        aiUnitNum = 5;
        aiUnitCodes = UnitManager.Instance.player2UnitCodes;
        aiUnits = UnitManager.Instance.player2Units;

        RandomDeploy();
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

    // AI의 유닛을 랜덤하게 배치
    private void RandomDeploy()
    {

    }
}
