using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using static TileEnums;
using Random = System.Random;

public class AIManager1 : MonoBehaviour
{
    public static AIManager1 Instance { get; private set; }

    public int aiUnitNum;
    public List<int> aiUnitCodes;
    public List<Unit> aiUnits;
    public List<GameObject> aiUnitPrefabs;

    public int aiUnitNum2;
    public List<int> aiUnitCodes2;
    public List<Unit> aiUnits2;
    public List<GameObject> aiUnitPrefabs2;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        aiUnitNum = 5;
        aiUnitNum2 = 5;

        aiUnitPrefabs = new List<GameObject>();
        aiUnitPrefabs2 = new List<GameObject>();

        Invoke("RandomDeploy", 0.1f);
        Invoke("RandomDeploy2", 0.1f);

    }

    // player1Units에 랜덤한 유닛을 생성하고 추가
    public void RandomSelection(int num)
    {
        List<int> aiFactionUnitCodes = new List<int>();

        aiFactionUnitCodes = UnitManager.Instance.GetAllUnitCodes(Faction.Seo);

        // 랜덤하게 섞고 앞에서 num개를 리스트로 반환
        List<int> randomSelectionUnitCodes = aiFactionUnitCodes
            .OrderBy(x => Guid.NewGuid())
            .Take(num)
            .ToList();

        // 정렬
        randomSelectionUnitCodes.Sort();

        foreach (int unitCode in randomSelectionUnitCodes)
        {
            UnitManager.Instance.player1UnitCodes.Add(unitCode);
            UnitManager.Instance.player1Units.Add(new(UnitManager.Instance.GetUnit(unitCode)));
        }

        foreach (Unit unit in UnitManager.Instance.player1Units)
            unit.team = Team.Ally;
    }

    // player2Units에 랜덤한 유닛을 생성하고 추가
    public void RandomSelection2(int num)
    {
        List<int> aiFactionUnitCodes = new List<int>();

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


    public void RandomDeploy()
    {
        // 랜덤하게 5명을 추출
        RandomSelection(aiUnitNum);
        
        aiUnitCodes = UnitManager.Instance.player1UnitCodes;
        aiUnits = UnitManager.Instance.player1Units;

        // TileInfo 업데이트
        List<TileInfo> deployableTileInfos = MapManager1.Instance.GetTileInfos(InitialDeployment.Player1);
        for (int i = 0; i < deployableTileInfos.Count; i++)
            deployableTileInfos[i].unit = null;
        foreach (GameObject aiUnitPrefab in aiUnitPrefabs)
            Destroy(aiUnitPrefab);

        // AI 유닛을 배치할 타일을 뽑음
        List<TileInfo> targetTileInfos = deployableTileInfos
            .OrderBy(x => Guid.NewGuid())
            .Take(aiUnitNum)
            .ToList();
        foreach (TileInfo tile in targetTileInfos)
        {
            Debug.Log($"Selected Tile: ({tile.x}, {tile.y})");
        }
        GameObject unitPrefab;
        for (int i = 0; i < aiUnitNum; i++)
        {
            // TileInfo 업데이트
            targetTileInfos[i].unit = aiUnits[i];
            // 시각적 업데이트
            unitPrefab = UnitPrefabManager.Instance.InstantiateUnitPrefab(aiUnitCodes[i], 2.0f, false);
            unitPrefab.transform.position = targetTileInfos[i].worldXY;
            targetTileInfos[i].unitPrefab = unitPrefab;     // 오류 나면 여기 볼것!!!!!!!!!!!
            aiUnitPrefabs.Add(unitPrefab);

        }
    }

    public void RandomDeploy2()
    {
        // 랜덤하게 5명을 추출
        RandomSelection2(aiUnitNum2);
        
        aiUnitCodes2 = UnitManager.Instance.player2UnitCodes;
        aiUnits2 = UnitManager.Instance.player2Units;

        // TileInfo 업데이트
        List<TileInfo> deployableTileInfos = MapManager1.Instance.GetTileInfos(InitialDeployment.Player2);
        for (int i = 0; i < deployableTileInfos.Count; i++)
            deployableTileInfos[i].unit = null;
        foreach (GameObject aiUnitPrefab2 in aiUnitPrefabs2)
            Destroy(aiUnitPrefab2);
        
        // AI 유닛을 배치할 타일을 뽑음
        List<TileInfo> targetTileInfos = deployableTileInfos
            .OrderBy(x => Guid.NewGuid())
            .Take(aiUnitNum2)
            .ToList();
        foreach (TileInfo tile in targetTileInfos)
        {
            Debug.Log($"Selected Tile2: ({tile.x}, {tile.y})");
        }
        GameObject unitPrefab;
        for (int i = 0; i < aiUnitNum2; i++)
        {
            // TileInfo 업데이트
            targetTileInfos[i].unit = aiUnits2[i];
            // 시각적 업데이트
            unitPrefab = UnitPrefabManager.Instance.InstantiateUnitPrefab(aiUnitCodes2[i], 2.0f, false);
            unitPrefab.transform.position = targetTileInfos[i].worldXY;
            targetTileInfos[i].unitPrefab = unitPrefab;     // 오류 나면 여기 볼것!!!!!!!!!!!
            aiUnitPrefabs2.Add(unitPrefab);

        }
    }

    public void OnAITurnStarted()
    {
        List<GameObject> aiUnitTiles = GetUnitTiles();
        aiUnitTiles.Sort((a, b) => b.GetComponent<TileInfo>().unit.currentAttackPoint.CompareTo(a.GetComponent<TileInfo>().unit.currentAttackPoint));

        foreach (GameObject tile in aiUnitTiles)
        {
            List<GameObject> targetTiles = MapManager1.Instance.GetManhattanTiles(tile, tile.GetComponent<TileInfo>().unit.currentAttackRange);
            targetTiles.RemoveAll(t => t.GetComponent<TileInfo>().unit == null || t.GetComponent<TileInfo>().unit.team == Team.Enemy);

            if (targetTiles.Count == 0) continue;

            targetTiles.Sort((a, b) => a.GetComponent<TileInfo>().unit.currentHealth.CompareTo(b.GetComponent<TileInfo>().unit.currentHealth));

            Unit aiUnit = tile.GetComponent<TileInfo>().unit;
            Unit playerUnit = targetTiles[0].GetComponent<TileInfo>().unit;

            AttackUnit(aiUnit, playerUnit);

            if (playerUnit.currentHealth <= 0)
            {
                Debug.Log($"{playerUnit.basicStats.unitName} is dead!");
                targetTiles[0].GetComponent<TileInfo>().unit = null;
                Destroy(targetTiles[0].GetComponent<TileInfo>().unitPrefab);
                InGameManager1.Instance.playerDeathCount++;
                if (InGameManager1.Instance.playerDeathCount == 5)
                    InGameManager1.Instance.GameEnd();
            }
            return;
        }

        aiUnitTiles.Sort((a, b) =>
        {
            int healthComparison = b.GetComponent<TileInfo>().unit.currentHealth.CompareTo(a.GetComponent<TileInfo>().unit.currentHealth);
            return healthComparison != 0 ? healthComparison : b.GetComponent<TileInfo>().unit.currentDefensePoint.CompareTo(a.GetComponent<TileInfo>().unit.currentDefensePoint);
        });

        foreach (GameObject tile in aiUnitTiles)
        {
            Unit aiUnit = tile.GetComponent<TileInfo>().unit;
            List<GameObject> possibleTiles = MapManager1.Instance.GetManhattanTiles(tile, aiUnit.currentMoveRange);
            possibleTiles.RemoveAll(t => t.GetComponent<TileInfo>().unit != null);

            if (possibleTiles.Count == 0) continue;

            GameObject targetTile = possibleTiles[0];
            MoveUnit(tile, targetTile);

            Debug.Log($"{aiUnit.basicStats.unitName} moved to ({targetTile.GetComponent<TileInfo>().x}, {targetTile.GetComponent<TileInfo>().y})");
            return;
        }
    }

    public void ProcessEnemyAction(int moveAction, int attackAction)
    {
        List<GameObject> enemyUnitTiles = GetUnitTiles();

        if (moveAction < enemyUnitTiles.Count)
        {
            GameObject selectedUnitTile = enemyUnitTiles[moveAction];
            Unit selectedUnit = selectedUnitTile.GetComponent<TileInfo>().unit;

            List<GameObject> possibleMoveTiles = MapManager1.Instance.GetManhattanTiles(selectedUnitTile, selectedUnit.currentMoveRange);
            possibleMoveTiles.RemoveAll(tile => tile.GetComponent<TileInfo>().unit != null);

            if (possibleMoveTiles.Count > 0)
            {
                MoveUnit(selectedUnitTile, possibleMoveTiles[0]);
                Debug.Log($"Enemy unit moved to ({possibleMoveTiles[0].GetComponent<TileInfo>().x}, {possibleMoveTiles[0].GetComponent<TileInfo>().y})");
            }
        }

        if (attackAction < enemyUnitTiles.Count)
        {
            GameObject selectedUnitTile = enemyUnitTiles[attackAction];
            Unit selectedUnit = selectedUnitTile.GetComponent<TileInfo>().unit;

            List<GameObject> possibleAttackTiles = MapManager1.Instance.GetManhattanTiles(selectedUnitTile, selectedUnit.currentAttackRange);
            possibleAttackTiles.RemoveAll(tile => tile.GetComponent<TileInfo>().unit == null || tile.GetComponent<TileInfo>().unit.team == Team.Enemy);

            if (possibleAttackTiles.Count > 0)
            {
                AttackUnit(selectedUnit, possibleAttackTiles[0].GetComponent<TileInfo>().unit);
                Debug.Log($"Enemy unit attacked {possibleAttackTiles[0].GetComponent<TileInfo>().unit.basicStats.unitName}");
            }
        }
    }

    private void MoveUnit(GameObject fromTile, GameObject toTile)
    {
        TileInfo fromTileInfo = fromTile.GetComponent<TileInfo>();
        TileInfo toTileInfo = toTile.GetComponent<TileInfo>();

        toTileInfo.unit = fromTileInfo.unit;
        toTileInfo.unitPrefab = fromTileInfo.unitPrefab;

        fromTileInfo.unitPrefab.transform.position = toTileInfo.worldXY;
        fromTileInfo.unit = null;
        fromTileInfo.unitPrefab = null;
    }

    private void AttackUnit(Unit attacker, Unit target)
    {
        int damage = Mathf.Max(attacker.currentAttackPoint - target.currentDefensePoint, 0);
        target.currentHealth -= damage;

        Debug.Log($"{attacker.basicStats.unitName} attacked {target.basicStats.unitName} for {damage} damage.");
    }

    public List<GameObject> GetUnitTiles()
    {
        return MapManager1.Instance.allTiles
            .Where(tile => tile.GetComponent<TileInfo>().unit != null && tile.GetComponent<TileInfo>().unit.team == Team.Enemy)
            .ToList();
    }
}
