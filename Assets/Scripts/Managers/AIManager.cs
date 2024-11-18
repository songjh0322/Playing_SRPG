using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using static TileEnums;
using Random = System.Random;

public class AIManager : MonoBehaviour
{
    public static AIManager Instance { get; private set; }

    public bool isAllDeployed;
    public int aiUnitNum;
    public List<int> aiUnitCodes;
    public List<Unit> aiUnits;

    // 프리팹
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
        isAllDeployed = false;
        aiUnitNum = 5;
        aiUnitPrefabs = new List<GameObject>();
        Invoke("RandomDeploy", 0.1f);   // TileInfo 생성 순서 문제 (임시로 해결)
    }

    // 여기서부터 AI 로직 시작
    public void OnAITurnStarted()
    {
        List<GameObject> aiUnitTiles = GetUnitTiles();    // AI 유닛이 존재하는 타일 오브젝트 업데이트
        // List<GameObject> playerUnitTiles = new List<GameObject>();

        // AI 유닛의 경우 공격력이 높은 순으로 정렬
        aiUnitTiles.Sort((a, b) => b.GetComponent<TileInfo>().unit.currentAttackPoint.CompareTo(a.GetComponent<TileInfo>().unit.currentAttackPoint));

        foreach (GameObject tile in aiUnitTiles)
        {
            List<GameObject> targetTiles = MapManager.Instance.GetManhattanTiles(tile, tile.GetComponent<TileInfo>().unit.currentAttackRange);
            targetTiles.RemoveAll(tile => tile.GetComponent<TileInfo>().unit == null);
            targetTiles.RemoveAll(tile => tile.GetComponent<TileInfo>().unit.team == Team.Enemy);

            // 현재 AI 유닛의 공격 범위 내에 적 유닛이 없으면 다음 AI 유닛 조사
            if (targetTiles.Count == 0)
                continue;

            targetTiles.Sort((a, b) => a.GetComponent<TileInfo>().unit.currentAttackPoint.CompareTo(b.GetComponent<TileInfo>().unit.currentHealth));  // Player 유닛의 경우 체력이 낮은 순으로 정렬
            
            // 공격 수행
            Unit aiUnit = tile.GetComponent<TileInfo>().unit;
            Unit playerUnit = targetTiles[0].GetComponent<TileInfo>().unit;

            int realDamage = Mathf.Max(aiUnit.currentAttackPoint - playerUnit.currentDefensePoint, 0);
            playerUnit.currentHealth -= realDamage;
            
            Debug.Log($"{aiUnit.basicStats.unitName}이(가) {playerUnit.basicStats.unitName}을(를) 공격함");
            Debug.Log($"{realDamage}의 피해를 입고 체력이 {playerUnit.currentHealth}이(가) 됨");

            // 대상이 죽는 경우
            if (playerUnit.currentHealth <= 0)
            {
                Debug.Log($"{playerUnit.basicStats.unitName}이(가) 사망함!");
                targetTiles[0].GetComponent<TileInfo>().unit = null;
                Destroy(targetTiles[0].GetComponent<TileInfo>().unitPrefab);

                InGameManager.Instance.playerDeathCount++;
                if (InGameManager.Instance.playerDeathCount == 5)
                        InGameManager.Instance.GameEnd();
            }
            return;
        }

        // 만약 앞에서 공격을 수행하지 않았다면 이동 수행

        // AI 유닛의 경우 1. 체력, 2. 방어력이 높은 순으로 정렬
        aiUnitTiles.Sort((a, b) =>
        {
            int healthComparison = b.GetComponent<TileInfo>().unit.currentHealth.CompareTo(a.GetComponent<TileInfo>().unit.currentHealth);
            if (healthComparison == 0)
            {
                // 체력이 같은 경우 방어력이 높은 순을 우선
                return b.GetComponent<TileInfo>().unit.currentDefensePoint.CompareTo(a.GetComponent<TileInfo>().unit.currentDefensePoint);
            }
            return healthComparison;
        });

        foreach (GameObject tile in aiUnitTiles)
        {
            // 현재 유닛으로부터 이동 거리 이내의 이동 가능한 타일을 조사
            Unit aiUnit = tile.GetComponent<TileInfo>().unit;
            List<GameObject> possibleTiles = MapManager.Instance.GetManhattanTiles(tile, aiUnit.currentMoveRange);
            possibleTiles.RemoveAll(tile => tile.GetComponent<TileInfo>().unit != null);    // 유닛이 있는 타일은 리스트에서 제거

            // 현재 유닛이 이동할 수 없는 경우 다음 유닛을 조사
            if (possibleTiles.Count == 0)
                continue;

            // 이동 가능한 타일 중 랜덤으로 이동
            Random random = new Random();
            int randomIndex = random.Next(possibleTiles.Count);
            GameObject targetTile = possibleTiles[randomIndex];

            targetTile.GetComponent<TileInfo>().unit = aiUnit;
            targetTile.GetComponent<TileInfo>().unitPrefab = tile.GetComponent<TileInfo>().unitPrefab;
            // firstTileInfo.unitPrefab.transform.position = lastTileInfo.worldXY;
            tile.GetComponent<TileInfo>().unitPrefab.transform.position = targetTile.GetComponent<TileInfo>().worldXY;
            targetTile.GetComponent<TileInfo>().unitPrefab = tile.GetComponent<TileInfo>().unitPrefab;

            tile.GetComponent<TileInfo>().unit = null;
            tile.GetComponent<TileInfo>().unitPrefab = null;

            Debug.Log($"{aiUnit.basicStats.unitName}이(가) ({targetTile.GetComponent<TileInfo>().x},{targetTile.GetComponent<TileInfo>().y})로 이동함");

            return;
        }
    }

    // player2Units에 랜덤한 유닛을 생성하고 추가
    private void RandomSelection(int num)
    {
        List<int> aiFactionUnitCodes = new List<int>();

        if (GameManager.Instance.playerFaction == Faction.Guwol)
            aiFactionUnitCodes = UnitManager.Instance.GetAllUnitCodes(Faction.Seo);
        else if (GameManager.Instance.playerFaction == Faction.Seo)
            aiFactionUnitCodes = UnitManager.Instance.GetAllUnitCodes(Faction.Guwol);

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

        foreach (Unit unit in UnitManager.Instance.player2Units)
            unit.team = Team.Enemy;
    }

    // AI의 유닛을 랜덤하게 배치 (TileInfo의 Start()가 모두 호출된 이후에 실행되야 함)
    public void RandomDeploy()
    {
        // 랜덤하게 5명을 추출
        RandomSelection(aiUnitNum);
        aiUnitCodes = UnitManager.Instance.player2UnitCodes;
        aiUnits = UnitManager.Instance.player2Units;

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
            unitPrefab = UnitPrefabManager.Instance.InstantiateUnitPrefab(aiUnitCodes[i], 2.0f, false);
            unitPrefab.transform.position = targetTileInfos[i].worldXY;
            targetTileInfos[i].unitPrefab = unitPrefab;     // 오류 나면 여기 볼것!!!!!!!!!!!
            unitPrefab.transform.SetParent(InitialDeployManager.Instance.ActiveUnits.transform);
            aiUnitPrefabs.Add(unitPrefab);

        }

        isAllDeployed = true;
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

    private void TryMove()
    {
        
    }

    private void TryAttack()
    {

    }

/*    private void EndTurn()
    {
        InGameManager.Instance.isPlayerTurn = true;
    }*/

    // AI 유닛이 존재하는 타일 오브젝트 리스트를 반환
    private List<GameObject> GetUnitTiles()
    {
        List<GameObject> tiles = new List<GameObject>();

        foreach (GameObject tile in MapManager.Instance.allTiles)
        {
            TileInfo tileInfo = tile.GetComponent<TileInfo>();
            // 유닛이 존재하면서 적 유닛인 경우
            if (tileInfo.unit != null && tileInfo.unit.team == Team.Enemy)
                tiles.Add(tile);
        }
        return tiles;
    }
}
