using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using static TileEnums;
using Random = System.Random;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

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
        Debug.Log($"Total unit codes available for selection: {aiFactionUnitCodes.Count}");

        aiFactionUnitCodes = UnitManager.Instance.GetAllUnitCodes(Faction.Seo);

        // 랜덤하게 섞고 앞에서 num개를 리스트로 반환
        List<int> randomSelectionUnitCodes = aiFactionUnitCodes
            .OrderBy(x => Guid.NewGuid())
            .Take(num)
            .ToList();

        Debug.Log($"Selected unit codes: {string.Join(", ", randomSelectionUnitCodes)}");

        // 정렬
        randomSelectionUnitCodes.Sort();

        foreach (int unitCode in randomSelectionUnitCodes)
        {
            UnitManager.Instance.player1UnitCodes.Add(unitCode);
            UnitManager.Instance.player1Units.Add(new(UnitManager.Instance.GetUnit(unitCode)));
            Debug.Log($"Unit added: Code={unitCode}, Name={UnitManager.Instance.GetUnit(unitCode).basicStats.unitName}");
        }

        foreach (Unit unit in UnitManager.Instance.player1Units)
        {
            unit.team = Team.Ally;
            Debug.Log($"Unit team set: Name={unit.basicStats.unitName}, Team={unit.team}");
        }
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
        Debug.Log($"Deploying {aiUnits.Count} units...");
        // TileInfo 업데이트
        List<TileInfo> deployableTileInfos = MapManager1.Instance.GetTileInfos(InitialDeployment.Player1);
        Debug.Log($"Deployable tiles count: {deployableTileInfos.Count}");
        for (int i = 0; i < deployableTileInfos.Count; i++)
            deployableTileInfos[i].unit = null;
        foreach (GameObject aiUnitPrefab in aiUnitPrefabs)
            Destroy(aiUnitPrefab);

        // AI 유닛을 배치할 타일을 뽑음
        List<TileInfo> targetTileInfos = deployableTileInfos
            .OrderBy(x => Guid.NewGuid())
            .Take(aiUnitNum)
            .ToList();
        Debug.Log($"Target tiles for deployment: {string.Join(", ", targetTileInfos.Select(t => $"({t.x}, {t.y})"))}");

        GameObject unitPrefab;
        for (int i = 0; i < aiUnitNum; i++)
        {
            // TileInfo 업데이트
            targetTileInfos[i].unit = aiUnits[i];
            Debug.Log($"Unit {aiUnits[i].basicStats.unitName} assigned to tile ({targetTileInfos[i].x}, {targetTileInfos[i].y})");

            // 시각적 업데이트
            unitPrefab = UnitPrefabManager.Instance.InstantiateUnitPrefab(aiUnitCodes[i], 2.0f, false);
            unitPrefab.transform.position = targetTileInfos[i].worldXY;
            targetTileInfos[i].unitPrefab = unitPrefab;     // 오류 나면 여기 볼것!!!!!!!!!!!
            aiUnitPrefabs.Add(unitPrefab);
        }
        Debug.Log("RandomDeploy");
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
        Debug.Log("RandomDeploy2");
    }

    public void OnAITurnStarted()
    {
        List<GameObject> aiUnitTiles = GetUnitTiles();

        if (aiUnitTiles.Count == 0)
        {
            Debug.Log("No AI units available for action.");
            return;
        }

        float maxReward = float.MinValue;
        GameObject bestUnitTile = null;
        GameObject bestTargetTile = null;
        bool isBestActionAttack = false;

        foreach (GameObject tile in aiUnitTiles)
        {
            Unit aiUnit = tile.GetComponent<TileInfo>().unit;

            // 공격 행동 평가
            List<GameObject> attackTargets = MapManager1.Instance.GetManhattanTiles(tile, aiUnit.currentAttackRange);
            attackTargets.RemoveAll(target => target.GetComponent<TileInfo>().unit == null || target.GetComponent<TileInfo>().unit.team == Team.Enemy);

            foreach (GameObject targetTile in attackTargets)
            {
                Unit targetUnit = targetTile.GetComponent<TileInfo>().unit;
                int potentialDamage = Mathf.Max(aiUnit.currentAttackPoint - targetUnit.currentDefensePoint, 0);
                float attackReward = potentialDamage;

                if (attackReward > maxReward)
                {
                    maxReward = attackReward;
                    bestUnitTile = tile;
                    bestTargetTile = targetTile;
                    isBestActionAttack = true;
                }
            }

            // 이동 행동 평가
            List<GameObject> moveTargets = MapManager1.Instance.GetManhattanTiles(tile, aiUnit.currentMoveRange);
            moveTargets.RemoveAll(moveTile => moveTile.GetComponent<TileInfo>().unit != null);

            foreach (GameObject moveTile in moveTargets)
            {
                float moveReward = EvaluateMoveReward(moveTile);

                if (moveReward > maxReward)
                {
                    maxReward = moveReward;
                    bestUnitTile = tile;
                    bestTargetTile = moveTile;
                    isBestActionAttack = false;
                }
            }
        }

        if (bestUnitTile != null && bestTargetTile != null)
        {
            if (isBestActionAttack)
            {
                Unit attacker = bestUnitTile.GetComponent<TileInfo>().unit;
                Unit target = bestTargetTile.GetComponent<TileInfo>().unit;
                AttackUnit(attacker, target);
                Debug.Log($"{attacker.basicStats.unitName} attacked {target.basicStats.unitName}");
            }
            else
            {
                MoveUnit(bestUnitTile, bestTargetTile);
                Debug.Log($"Unit moved to ({bestTargetTile.GetComponent<TileInfo>().x}, {bestTargetTile.GetComponent<TileInfo>().y})");
            }
        }
        else
        {
            Debug.Log("No optimal action found.");
        }
    }

    private float EvaluateMoveReward(GameObject moveTile)
    {
        // 이동 보상을 평가하는 간단한 기준 (예: 특정 지역으로 이동하면 보상 증가)
        // 필요에 따라 복잡한 기준 추가 가능
        return UnityEngine.Random.Range(0f, 1f);  // 임시로 랜덤 값
    }

    private void MoveUnit(GameObject fromTile, GameObject toTile)
    {
        Debug.Log("MoveUnit()");
        TileInfo fromTileInfo = fromTile.GetComponent<TileInfo>();
        TileInfo toTileInfo = toTile.GetComponent<TileInfo>();

        toTileInfo.unit = fromTileInfo.unit;
        toTileInfo.unitPrefab = fromTileInfo.unitPrefab;

        fromTileInfo.unitPrefab.transform.position = toTileInfo.worldXY;
        fromTileInfo.unit = null;
        fromTileInfo.unitPrefab = null;
        
        InGameManager1.Instance.EndTurn();
    }

    private void AttackUnit(Unit attacker, Unit target)
    {
        Debug.Log("AttackUnit()");
        int damage = Mathf.Max(attacker.currentAttackPoint - target.currentDefensePoint, 0);
        target.currentHealth -= damage;

        Debug.Log($"{attacker.basicStats.unitName} attacked {target.basicStats.unitName} for {damage} damage.");

        if (target.currentHealth <= 0)
        {
            Debug.Log($"{target.basicStats.unitName} die.");
            // target이 위치한 TileInfo를 가져오기
            TileInfo targetTileInfo = FindTileInfoByUnit(target);
            if (targetTileInfo != null)
            {
                targetTileInfo.unit = null;       // 타일의 유닛 정보 초기화

                if (targetTileInfo.unitPrefab != null)
                {
                    Destroy(targetTileInfo.unitPrefab); // 유닛 프리팹 삭제
                    targetTileInfo.unitPrefab = null;   // 프리팹 정보도 초기화
                }
            }
            if (target.team == Team.Ally)
            {
                InGameManager1.Instance.playerDeathCount++;
            }
            else if (target.team == Team.Enemy)
            {
                InGameManager1.Instance.aiDeathCount++;
            }
        }
        InGameManager1.Instance.EndTurn();
    }

    private TileInfo FindTileInfoByUnit(Unit unit)
    {
        // MapManager에서 모든 타일을 순회하여 해당 유닛이 있는 TileInfo를 반환
        foreach (GameObject tile in MapManager.Instance.allTiles)
        {
            TileInfo tileInfo = tile.GetComponent<TileInfo>();
            if (tileInfo.unit == unit)
            {
                return tileInfo;
            }
        }
        return null; // 유닛이 위치한 타일을 찾지 못한 경우
    }

    // public void ProcessEnemyAction(int moveAction, int attackAction)
    // {
    //     List<GameObject> enemyUnitTiles = GetUnitTiles();

    //     if (moveAction < enemyUnitTiles.Count)
    //     {
    //         GameObject selectedUnitTile = enemyUnitTiles[moveAction];
    //         Unit selectedUnit = selectedUnitTile.GetComponent<TileInfo>().unit;

    //         List<GameObject> possibleMoveTiles = MapManager1.Instance.GetManhattanTiles(selectedUnitTile, selectedUnit.currentMoveRange);
    //         possibleMoveTiles.RemoveAll(tile => tile.GetComponent<TileInfo>().unit != null);

    //         if (possibleMoveTiles.Count > 0)
    //         {
    //             MoveUnit(selectedUnitTile, possibleMoveTiles[0]);
    //             //Debug.Log($"Enemy unit moved to ({possibleMoveTiles[0].GetComponent<TileInfo>().x}, {possibleMoveTiles[0].GetComponent<TileInfo>().y})");
    //         }
    //     }

    //     if (attackAction < enemyUnitTiles.Count)
    //     {
    //         GameObject selectedUnitTile = enemyUnitTiles[attackAction];
    //         Unit selectedUnit = selectedUnitTile.GetComponent<TileInfo>().unit;

    //         List<GameObject> possibleAttackTiles = MapManager1.Instance.GetManhattanTiles(selectedUnitTile, selectedUnit.currentAttackRange);
    //         possibleAttackTiles.RemoveAll(tile => tile.GetComponent<TileInfo>().unit == null || tile.GetComponent<TileInfo>().unit.team == Team.Enemy);

    //         if (possibleAttackTiles.Count > 0)
    //         {
    //             AttackUnit(selectedUnit, possibleAttackTiles[0].GetComponent<TileInfo>().unit);
    //             Debug.Log($"Enemy unit attacked {possibleAttackTiles[0].GetComponent<TileInfo>().unit.basicStats.unitName}");
    //         }
    //     }
    // }
    // public void ProcessEnemyAction()
    // {
    //     Debug.Log("ProcessEnemyAction()");
    //     List<GameObject> aiUnitTiles = GetUnitTiles();
    //     if (aiUnitTiles.Count == 0) return;

    //     float maxReward = float.MinValue;
    //     GameObject bestUnitTile = null;
    //     GameObject bestTargetTile = null;
    //     bool isBestActionAttack = false;

    //     foreach (GameObject tile in aiUnitTiles)
    //     {
    //         Unit aiUnit = tile.GetComponent<TileInfo>().unit;

    //         // 공격 행동 평가
    //         List<GameObject> attackTargets = MapManager1.Instance.GetManhattanTiles(tile, aiUnit.currentAttackRange);
    //         attackTargets.RemoveAll(target => target.GetComponent<TileInfo>().unit == null || target.GetComponent<TileInfo>().unit.team == Team.Enemy);

    //         foreach (GameObject targetTile in attackTargets)
    //         {
    //             Unit targetUnit = targetTile.GetComponent<TileInfo>().unit;
    //             int potentialDamage = Mathf.Max(aiUnit.currentAttackPoint - targetUnit.currentDefensePoint, 0);
    //             float attackReward = potentialDamage;

    //             if (attackReward > maxReward)
    //             {
    //                 maxReward = attackReward;
    //                 bestUnitTile = tile;
    //                 bestTargetTile = targetTile;
    //                 isBestActionAttack = true;
    //             }
    //         }

    //         // 이동 행동 평가
    //         List<GameObject> moveTargets = MapManager1.Instance.GetManhattanTiles(tile, aiUnit.currentMoveRange);
    //         moveTargets.RemoveAll(moveTile => moveTile.GetComponent<TileInfo>().unit != null);

    //         foreach (GameObject moveTile in moveTargets)
    //         {
    //             float moveReward = EvaluateMoveReward(moveTile); // 이동 보상 계산
    //             if (moveReward > maxReward)
    //             {
    //                 maxReward = moveReward;
    //                 bestUnitTile = tile;
    //                 bestTargetTile = moveTile;
    //                 isBestActionAttack = false;
    //             }
    //         }
    //     }

    //     // 가장 높은 보상을 얻는 행동 수행
    //     if (bestUnitTile != null && bestTargetTile != null)
    //     {
    //         if (isBestActionAttack)
    //         {
    //             Unit attacker = bestUnitTile.GetComponent<TileInfo>().unit;
    //             Unit target = bestTargetTile.GetComponent<TileInfo>().unit;
    //             AttackUnit(attacker, target);
    //             Debug.Log($"{attacker.basicStats.unitName} attacked {target.basicStats.unitName}");
    //         }
    //         else
    //         {
    //             MoveUnit(bestUnitTile, bestTargetTile);
    //             Debug.Log($"Unit moved to ({bestTargetTile.GetComponent<TileInfo>().x}, {bestTargetTile.GetComponent<TileInfo>().y})");
    //         }
    //     }
    //     else
    //     {
    //         Debug.Log("No optimal action found.");
    //     }
    // }
    public void ProcessEnemyAction(int action)
    {
        Debug.Log("ProcessEnemyAction()");
       
        List<GameObject> unitTiles = GetUnitTiles();
        Debug.Log($"Initial unit tiles count: {unitTiles.Count}");

        unitTiles.RemoveAll(target => target.GetComponent<TileInfo>().unit == null || target.GetComponent<TileInfo>().unit.team == Team.Ally);
        if (unitTiles.Count == 0)
        {
            Debug.LogWarning("No AI units available for action.");
            return;
        }
        int unitIndex = action / 2; // 행동 인덱스의 첫 번째는 유닛 선택
        bool isAttack = (action % 2) == 1; // 나머지로 공격(1)/이동(0) 구분

        // if (unitIndex >= unitTiles.Count)
        // {
        //     Debug.LogWarning("Invalid action: Unit index out of range.");
        //     return;
        // }

        // GameObject selectedUnitTile = unitTiles[unitIndex];
        GameObject selectedUnitTile = unitTiles[0];
        Unit activeUnit = selectedUnitTile.GetComponent<TileInfo>().unit;

        if (isAttack)
        {
            // 공격 행동 처리
            List<GameObject> attackTargets = MapManager1.Instance.GetManhattanTiles(selectedUnitTile, activeUnit.currentAttackRange);
            attackTargets.RemoveAll(target => target.GetComponent<TileInfo>().unit == null || target.GetComponent<TileInfo>().unit.team == Team.Enemy);
            if (attackTargets.Count > 0)
            {
                GameObject targetTile = attackTargets[0]; // 간단히 첫 번째 대상 선택
                Unit targetUnit = targetTile.GetComponent<TileInfo>().unit;
                AttackUnit(activeUnit, targetUnit);
                Debug.Log($"{activeUnit.basicStats.unitName} attacked {targetUnit.basicStats.unitName}");
            }
            else
            {
                Debug.LogWarning("No valid targets for attack.");
            }
        }
        else
        {
            // 이동 행동 처리
            List<GameObject> moveTargets = MapManager1.Instance.GetManhattanTiles(selectedUnitTile, activeUnit.currentMoveRange);
            moveTargets.RemoveAll(moveTile => moveTile.GetComponent<TileInfo>().unit != null);

            if (moveTargets.Count > 0)
            {
                GameObject moveTile = moveTargets[0]; // 간단히 첫 번째 이동 가능한 타일 선택
                MoveUnit(selectedUnitTile, moveTile);
                Debug.Log($"Unit moved to ({moveTile.GetComponent<TileInfo>().x}, {moveTile.GetComponent<TileInfo>().y})");
            }
            else
            {
                Debug.LogWarning("No valid tiles for movement.");
            }
        }
    }

    public void ProcessEnemyAction2(int action)
    {
        Debug.Log("ProcessEnemyAction2()");
        
        List<GameObject> unitTiles = GetUnitTiles();
        Debug.Log($"Initial unit tiles count: {unitTiles.Count}");

        unitTiles.RemoveAll(target => target.GetComponent<TileInfo>().unit == null || target.GetComponent<TileInfo>().unit.team == Team.Enemy);
        if (unitTiles.Count == 0)
        {
            Debug.LogWarning("No AI units available for action.");
            return;
        }

        int unitIndex = action / 2; // 행동 인덱스의 첫 번째는 유닛 선택
        bool isAttack = (action % 2) == 1; // 나머지로 공격(1)/이동(0) 구분

        // if (unitIndex >= unitTiles.Count)
        // {
        //     Debug.LogWarning("Invalid action: Unit index out of range.");
        //     return;
        // }

        // GameObject selectedUnitTile = unitTiles[unitIndex];
        GameObject selectedUnitTile = unitTiles[0];
        Unit activeUnit = selectedUnitTile.GetComponent<TileInfo>().unit;

        if (isAttack)
        {
            // 공격 행동 처리
            List<GameObject> attackTargets = MapManager1.Instance.GetManhattanTiles(selectedUnitTile, activeUnit.currentAttackRange);
            attackTargets.RemoveAll(target => target.GetComponent<TileInfo>().unit == null || target.GetComponent<TileInfo>().unit.team == Team.Ally);
            if (attackTargets.Count > 0)
            {
                GameObject targetTile = attackTargets[0]; // 간단히 첫 번째 대상 선택
                Unit targetUnit = targetTile.GetComponent<TileInfo>().unit;
                AttackUnit(activeUnit, targetUnit);
                Debug.Log($"{activeUnit.basicStats.unitName} attacked {targetUnit.basicStats.unitName}");
            }
            else
            {
                Debug.LogWarning("No valid targets for attack.");
            }
        }
        else
        {
            // 이동 행동 처리
            List<GameObject> moveTargets = MapManager1.Instance.GetManhattanTiles(selectedUnitTile, activeUnit.currentMoveRange);
            moveTargets.RemoveAll(moveTile => moveTile.GetComponent<TileInfo>().unit != null);

            if (moveTargets.Count > 0)
            {
                GameObject moveTile = moveTargets[0]; // 간단히 첫 번째 이동 가능한 타일 선택
                MoveUnit(selectedUnitTile, moveTile);
                Debug.Log($"Unit moved to ({moveTile.GetComponent<TileInfo>().x}, {moveTile.GetComponent<TileInfo>().y})");
            }
            else
            {
                Debug.LogWarning("No valid tiles for movement.");
            }
        }
    }

    public List<GameObject> GetUnitTiles()
    {
        return MapManager1.Instance.allTiles
            .Where(tile => tile.GetComponent<TileInfo>().unit != null)
            .ToList();
    }
}
