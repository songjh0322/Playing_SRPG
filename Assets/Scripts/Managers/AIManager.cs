using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using static TileEnums;
using Unity.VisualScripting;
using Unity.Collections;

public class AIManager : MonoBehaviour
{
    public static AIManager Instance { get; private set; }

    public int aiUnitNum;
    public List<int> aiUnitCodes;
    public List<Unit> aiUnits;

    public List<GameObject> aiUnitPrefabs;

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
        aiUnitNum = 5;
        aiUnitPrefabs = new List<GameObject>();

        // 랜덤하게 5명을 추출
        RandomSelection(aiUnitNum);
        aiUnitCodes = UnitManager.Instance.player2UnitCodes;
        aiUnits = UnitManager.Instance.player2Units;

        //RandomDeploy(); // 반드시 TileInfo의 Start()가 모두 호출된 이후에 실행해야 함
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RandomDeploy();
        }
    }

    // player2Units에 랜덤한 유닛을 생성하고 추가
    public void RandomSelection(int num)
    {
        List<int> aiFactionUnitCodes = new List<int>();

        if (GameManager.Instance.playerFaction == Faction.Guwol)
            aiFactionUnitCodes = UnitManager.Instance.GetUnitCodes(Faction.Seo);
        else if (GameManager.Instance.playerFaction == Faction.Seo)
            aiFactionUnitCodes = UnitManager.Instance.GetUnitCodes(Faction.Guwol);

        // 랜덤하게 섞고 앞에서 num개를 리스트로 반환
        List<int> randomSelectionUnitCodes = aiFactionUnitCodes
            .OrderBy(x => Guid.NewGuid())
            .Take(num)
            .ToList();

        // 정렬
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
        // 초기화
        Reset();

        // Player2가 배치 가능한 타일 정보만 획득
        List<TileInfo> deployableTileInfos = MapManager.Instance.GetTileInfos(InitialDeployment.Player2);

        // AI 유닛을 배치할 타일을 뽑음
        List<TileInfo> targetTileInfos = deployableTileInfos
            .OrderBy(x => Guid.NewGuid())
            .Take(aiUnitNum)
            .ToList();

        GameObject unitPrefab;
        for (int i = 0; i < aiUnitNum; i++)
        {
            // TileInfo 업데이트
            targetTileInfos[i].unit = aiUnits[i];
            // 시각적 업데이트
            unitPrefab = UnitPrefabManager.Instance.InstantiateUnitPrefab(aiUnitCodes[i], 2.0f);
            unitPrefab.transform.position = targetTileInfos[i].worldXY;
            aiUnitPrefabs.Add(unitPrefab);
        }
    }

    // Player2(AI)가 배치 가능한 구역을 전부 초기화 (TileInfo, 시각적 업데이트)
    private void Reset()
    {
        // TileInfo 업데이트
        List<TileInfo> deployableTileInfos = MapManager.Instance.GetTileInfos(InitialDeployment.Player2);
        for (int i = 0; i < deployableTileInfos.Count; i++)
            deployableTileInfos[i].unit = null;
        foreach (GameObject aiUnitPrefab in aiUnitPrefabs)
            Destroy(aiUnitPrefab);
    }
}
