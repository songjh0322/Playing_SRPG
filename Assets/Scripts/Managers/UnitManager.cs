using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

// JSON 파일에서 기본 능력치들을 불러오기 위한 클래스 (이곳에서만 사용)
public class UnitDataWrapper
{
    public List<BasicStats> statsWrapper;
}

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance { get; private set; }

    public List<BasicStats> basicStatsList;     // 모든 기본 능력치를 저장하는 리스트 (원본)
    public List<Unit> allUnits;    // 모든 유닛을 저장할 List (원본, 인게임에서 사용하지 않음)

    // 인게임에서 참조되는 요소
    public List<int> player1UnitCodes;
    public List<int> player2UnitCodes;
    public List<Unit> player1Units;      // Player1이 인게임에서 실제로 사용할 유닛 리스트 (참조)
    public List<Unit> player2Units;      // Player2가 인게임에서 실제로 사용할 유닛 리스트 (참조)

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            basicStatsList = new List<BasicStats>();
            allUnits = new List<Unit>();
            player1UnitCodes = new List<int>();
            player2UnitCodes = new List<int>();
            player1Units = new List<Unit>();
            player2Units = new List<Unit>();
        }
    }
    private void Start()
    {
        
    }

    // 기능 : JSON 파일로부터 데이터를 basicStatsList에 저장
    // 주의 : 게임 실행 시 최초 1회만 호출해야 함
    public void LoadBasicStatsFromJSON()
    {
        // JSON 파일 경로
        string jsonFilePath = Path.Combine(Application.dataPath, "Resources/BasicStats.json");

        // JSON 파일을 읽기
        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);

            // JSON 데이터를 UnitDataWrapper로 변환
            UnitDataWrapper unitDataWrapper = JsonUtility.FromJson<UnitDataWrapper>(jsonData);

            // 변환된 데이터를 List에 할당
            basicStatsList = unitDataWrapper.statsWrapper;
        }
        else
        {
            Debug.LogError("BasicStats.json 파일을 찾을 수 없습니다.");
        }
    }

    // 기능 : basicStatsList로부터 Unit 객체를 모두 생성하고 allUnits에 저장
    // 주의 : 게임 실행 시 최초 1회만 호출해야 함
    public void LoadAllUnits()
    {
        if (basicStatsList == null)
            Debug.LogError("basicStatsList가 초기화되지 않았습니다.");
        else
        {
            foreach (BasicStats basicStats in basicStatsList)
                allUnits.Add(new Unit(basicStats));
        }
    }

    // unitCode를 통해 대응되는 Unit 객체를 얻음 (주의 : 인게임에서 사용하면 안됨)
    public Unit GetUnit(int unitCode)
    {
        foreach (Unit unit in allUnits)
        {
            if (unitCode == unit.basicStats.unitCode)
                return unit;
        }

        return null;
    }

    // 진영 정보를 통해 해당 유닛들의 리스트를 얻음
    public List<Unit> GetUnits(Faction faction)
    {
        List<Unit> factionUnits = new List<Unit>();
        foreach (Unit unit in allUnits)
        {
            if (faction == unit.basicStats.faction)
                factionUnits.Add(unit);
        }
        return factionUnits;
    }

    public List<int> GetUnitCodes(Faction faction)
    {
        List<int> unitCodes = new List<int>();
        foreach (Unit unit in allUnits)
        {
            if (faction == unit.basicStats.faction)
                unitCodes.Add(unit.basicStats.unitCode);
        }
        return unitCodes;
    }

    // unitCode를 통해 player1Units에서 Unit 객체를 얻음 (인게임에서 사용)
    public Unit GetPlayer1Unit(int unitCode)
    {
        foreach (Unit unit in player1Units)
        {
            if (unit.basicStats.unitCode == unitCode)
                return unit;
        }
        return null;
    }
}

