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

    // player1Units�� ������ ������ �����ϰ� �߰�
    public void RandomSelection(int num)
    {
        List<int> aiFactionUnitCodes = new List<int>();
        Debug.Log($"Total unit codes available for selection: {aiFactionUnitCodes.Count}");

        aiFactionUnitCodes = UnitManager.Instance.GetAllUnitCodes(Faction.Seo);

        // �����ϰ� ���� �տ��� num���� ����Ʈ�� ��ȯ
        List<int> randomSelectionUnitCodes = aiFactionUnitCodes
            .OrderBy(x => Guid.NewGuid())
            .Take(num)
            .ToList();

        Debug.Log($"Selected unit codes: {string.Join(", ", randomSelectionUnitCodes)}");

        // ����
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

    // player2Units�� ������ ������ �����ϰ� �߰�
    public void RandomSelection2(int num)
    {
        List<int> aiFactionUnitCodes = new List<int>();

        aiFactionUnitCodes = UnitManager.Instance.GetAllUnitCodes(Faction.Guwol);

        // �����ϰ� ���� �տ��� num���� ����Ʈ�� ��ȯ
        List<int> randomSelectionUnitCodes = aiFactionUnitCodes
            .OrderBy(x => Guid.NewGuid())
            .Take(num)
            .ToList();

        // ����
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
        // �����ϰ� 5���� ����
        RandomSelection(aiUnitNum);
        
        aiUnitCodes = UnitManager.Instance.player1UnitCodes;
        aiUnits = UnitManager.Instance.player1Units;
        Debug.Log($"Deploying {aiUnits.Count} units...");
        // TileInfo ������Ʈ
        List<TileInfo> deployableTileInfos = MapManager1.Instance.GetTileInfos(InitialDeployment.Player1);
        Debug.Log($"Deployable tiles count: {deployableTileInfos.Count}");
        for (int i = 0; i < deployableTileInfos.Count; i++)
            deployableTileInfos[i].unit = null;
        foreach (GameObject aiUnitPrefab in aiUnitPrefabs)
            Destroy(aiUnitPrefab);

        // AI ������ ��ġ�� Ÿ���� ����
        List<TileInfo> targetTileInfos = deployableTileInfos
            .OrderBy(x => Guid.NewGuid())
            .Take(aiUnitNum)
            .ToList();
        Debug.Log($"Target tiles for deployment: {string.Join(", ", targetTileInfos.Select(t => $"({t.x}, {t.y})"))}");

        GameObject unitPrefab;
        for (int i = 0; i < aiUnitNum; i++)
        {
            // TileInfo ������Ʈ
            targetTileInfos[i].unit = aiUnits[i];
            Debug.Log($"Unit {aiUnits[i].basicStats.unitName} assigned to tile ({targetTileInfos[i].x}, {targetTileInfos[i].y})");

            // �ð��� ������Ʈ
            unitPrefab = UnitPrefabManager.Instance.InstantiateUnitPrefab(aiUnitCodes[i], 2.0f, false);
            unitPrefab.transform.position = targetTileInfos[i].worldXY;
            targetTileInfos[i].unitPrefab = unitPrefab;     // ���� ���� ���� ����!!!!!!!!!!!
            aiUnitPrefabs.Add(unitPrefab);
        }
        Debug.Log("RandomDeploy");
    }

    public void RandomDeploy2()
    {
        // �����ϰ� 5���� ����
        RandomSelection2(aiUnitNum2);
        
        aiUnitCodes2 = UnitManager.Instance.player2UnitCodes;
        aiUnits2 = UnitManager.Instance.player2Units;

        // TileInfo ������Ʈ
        List<TileInfo> deployableTileInfos = MapManager1.Instance.GetTileInfos(InitialDeployment.Player2);
        for (int i = 0; i < deployableTileInfos.Count; i++)
            deployableTileInfos[i].unit = null;
        foreach (GameObject aiUnitPrefab2 in aiUnitPrefabs2)
            Destroy(aiUnitPrefab2);
        
        // AI ������ ��ġ�� Ÿ���� ����
        List<TileInfo> targetTileInfos = deployableTileInfos
            .OrderBy(x => Guid.NewGuid())
            .Take(aiUnitNum2)
            .ToList();
       
        GameObject unitPrefab;
        for (int i = 0; i < aiUnitNum2; i++)
        {
            // TileInfo ������Ʈ
            targetTileInfos[i].unit = aiUnits2[i];
            // �ð��� ������Ʈ
            unitPrefab = UnitPrefabManager.Instance.InstantiateUnitPrefab(aiUnitCodes2[i], 2.0f, false);
            unitPrefab.transform.position = targetTileInfos[i].worldXY;
            targetTileInfos[i].unitPrefab = unitPrefab;     // ���� ���� ���� ����!!!!!!!!!!!
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

            // ���� �ൿ ��
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

            // �̵� �ൿ ��
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
        // �̵� ������ ���ϴ� ������ ���� (��: Ư�� �������� �̵��ϸ� ���� ����)
        // �ʿ信 ���� ������ ���� �߰� ����
        return UnityEngine.Random.Range(0f, 1f);  // �ӽ÷� ���� ��
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
            // target�� ��ġ�� TileInfo�� ��������
            TileInfo targetTileInfo = FindTileInfoByUnit(target);
            if (targetTileInfo != null)
            {
                targetTileInfo.unit = null;       // Ÿ���� ���� ���� �ʱ�ȭ

                if (targetTileInfo.unitPrefab != null)
                {
                    Destroy(targetTileInfo.unitPrefab); // ���� ������ ����
                    targetTileInfo.unitPrefab = null;   // ������ ������ �ʱ�ȭ
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
        // MapManager���� ��� Ÿ���� ��ȸ�Ͽ� �ش� ������ �ִ� TileInfo�� ��ȯ
        foreach (GameObject tile in MapManager.Instance.allTiles)
        {
            TileInfo tileInfo = tile.GetComponent<TileInfo>();
            if (tileInfo.unit == unit)
            {
                return tileInfo;
            }
        }
        return null; // ������ ��ġ�� Ÿ���� ã�� ���� ���
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

    //         // ���� �ൿ ��
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

    //         // �̵� �ൿ ��
    //         List<GameObject> moveTargets = MapManager1.Instance.GetManhattanTiles(tile, aiUnit.currentMoveRange);
    //         moveTargets.RemoveAll(moveTile => moveTile.GetComponent<TileInfo>().unit != null);

    //         foreach (GameObject moveTile in moveTargets)
    //         {
    //             float moveReward = EvaluateMoveReward(moveTile); // �̵� ���� ���
    //             if (moveReward > maxReward)
    //             {
    //                 maxReward = moveReward;
    //                 bestUnitTile = tile;
    //                 bestTargetTile = moveTile;
    //                 isBestActionAttack = false;
    //             }
    //         }
    //     }

    //     // ���� ���� ������ ��� �ൿ ����
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
        int unitIndex = action / 2; // �ൿ �ε����� ù ��°�� ���� ����
        bool isAttack = (action % 2) == 1; // �������� ����(1)/�̵�(0) ����

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
            // ���� �ൿ ó��
            List<GameObject> attackTargets = MapManager1.Instance.GetManhattanTiles(selectedUnitTile, activeUnit.currentAttackRange);
            attackTargets.RemoveAll(target => target.GetComponent<TileInfo>().unit == null || target.GetComponent<TileInfo>().unit.team == Team.Enemy);
            if (attackTargets.Count > 0)
            {
                GameObject targetTile = attackTargets[0]; // ������ ù ��° ��� ����
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
            // �̵� �ൿ ó��
            List<GameObject> moveTargets = MapManager1.Instance.GetManhattanTiles(selectedUnitTile, activeUnit.currentMoveRange);
            moveTargets.RemoveAll(moveTile => moveTile.GetComponent<TileInfo>().unit != null);

            if (moveTargets.Count > 0)
            {
                GameObject moveTile = moveTargets[0]; // ������ ù ��° �̵� ������ Ÿ�� ����
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

        int unitIndex = action / 2; // �ൿ �ε����� ù ��°�� ���� ����
        bool isAttack = (action % 2) == 1; // �������� ����(1)/�̵�(0) ����

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
            // ���� �ൿ ó��
            List<GameObject> attackTargets = MapManager1.Instance.GetManhattanTiles(selectedUnitTile, activeUnit.currentAttackRange);
            attackTargets.RemoveAll(target => target.GetComponent<TileInfo>().unit == null || target.GetComponent<TileInfo>().unit.team == Team.Ally);
            if (attackTargets.Count > 0)
            {
                GameObject targetTile = attackTargets[0]; // ������ ù ��° ��� ����
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
            // �̵� �ൿ ó��
            List<GameObject> moveTargets = MapManager1.Instance.GetManhattanTiles(selectedUnitTile, activeUnit.currentMoveRange);
            moveTargets.RemoveAll(moveTile => moveTile.GetComponent<TileInfo>().unit != null);

            if (moveTargets.Count > 0)
            {
                GameObject moveTile = moveTargets[0]; // ������ ù ��° �̵� ������ Ÿ�� ����
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
