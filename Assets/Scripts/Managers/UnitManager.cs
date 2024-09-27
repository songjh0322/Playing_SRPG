using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// JSON 파일에서 유닛 목록을 불러오기 위한 클래스 (이곳에서만 사용)
[Serializable]
public class UnitStatsList
{
    public BasicStats[] units;  // BasicStats 배열
}

public class UnitManager
{
    public static UnitManager Instance { get; private set; }

    public const int maxUnits = 6;      // 최대 유닛 선택 수

    public Dictionary<string, BasicStats> basicStatsData;  // 기본 유닛 능력치(JSON 데이터)를 담을 딕셔너리

    public List<Unit> guwol = new List<Unit>();     
    public List<Unit> seo = new List<Unit>();

    public List<Unit> player1Units = new List<Unit>();      // Player1이 인게임에서 사용할 유닛 리스트 (참조)
    public List<Unit> player2Units = new List<Unit>();      // Player2가 인게임에서 사용할 유닛 리스트 (참조)

    // 싱글톤 인스턴스 설정
    private UnitManager()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // 싱글톤 인스턴스를 반환
    public static UnitManager GetInstance()
    {
        if (Instance == null)
        {
            Instance = new UnitManager();
        }
        return Instance;
    }

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

    // 사용 : [캐릭터 선택을 모두 마쳤습니다. 전투를 시작하시겠습니까?] -> [예] 버튼 클릭 시 호출
    // 파라미터 : UI에서 선택한 유닛들의 이름을 담은 List
    // 기능 : Player1이 사용할 유닛들을 최종 결정
    public void ConfirmPlayer1Units(List<string> unitNames)
    {
        for (int i = 0; i < unitNames.Count; i++)
        {
            player1Units.Add(new Unit(unitNames[i], basicStatsData));
        }
    }

    // 현재 미사용 함수
    public void ConfirmPlayer2Units()
    {

    }

    // 사용 : [캐릭터 선택을 모두 마쳤습니다. 전투를 시작하시겠습니까?] -> [예] 버튼 클릭 시 호출
    // 기능 : Player2가 사용할 유닛들을 랜덤으로 결정 (반대 진영에서 임의로 차출)
    public void RandomizePlayer2Units()
    {

    }

    // 사용 : [캐릭터를 배치하세요!] 화면 진입 시 가장 먼저 호출
    // 기능 : [캐릭터명] 버튼과 배치할 타일을 번갈아가며 클릭하여 캐릭터를 배치함
    public void InitiatePlayer1UnitPlacement()
    {
        
    }
}

