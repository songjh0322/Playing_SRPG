using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// JSON 파일에서 유닛 목록을 불러오기 위한 클래스
[Serializable]
public class UnitStatsList
{
    public BasicStats[] units;  // BasicStats 배열
}

public class UnitManager
{
    private Dictionary<string, BasicStats> basicStatsData;  // 기본 유닛 능력치(JSON 데이터)를 담을 딕셔너리
    public List<Unit> activeUnits = new List<Unit>();       // 맵 상에 생성되고, 배치할 유닛을 담는 리스트

    // JSON 데이터를 불러오는 메서드
    public void LoadUnitDataFromJSON()
    {
        TextAsset jsonTextFile = Resources.Load<TextAsset>("CharacterStats");
        if (jsonTextFile != null)
        {
            UnitStatsList statsList = JsonUtility.FromJson<UnitStatsList>(jsonTextFile.text);

            basicStatsData = new Dictionary<string, BasicStats>();

            // JSON 데이터를 딕셔너리로 변환하여 저장
            foreach (BasicStats stats in statsList.units)
            {
                basicStatsData[stats.unitName] = stats;
            }

            Debug.Log("성공적으로 JSON 파일을 로드했습니다.");
        }
        else
        {
            Debug.LogError("CharacterStats.json 파일을 찾을 수 없습니다.");
        }
    }

    // 유닛을 생성하는 메서드
    public Unit CreateUnit(string unitName)
    {
        if (basicStatsData.ContainsKey(unitName))
        {
            Unit newUnit = new Unit(unitName, basicStatsData);
            activeUnits.Add(newUnit); // 생성된 유닛을 리스트에 추가
            Debug.Log(unitName + " 유닛 생성 완료.");
            return newUnit;
        }
        else
        {
            Debug.LogError(unitName + " 유닛은 JSON 데이터에 없습니다.");
            return null;
        }
    }

    // UI : [캐릭터 선택을 모두 마쳤습니다. 전투를 시작하시겠습니까?]
    // [예]를 클릭한 경우 호출
    public void ConfirmPlayer1Units()
    {

    }

    public void RandomizePlayer2Units()
    {

    }
}

