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

    // ������
    public List<GameObject> aiUnitPrefabs;

    private void Awake()
    {
        // Debug.Log("AIManager�� ������");

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
        Invoke("RandomDeploy", 0.1f);   // TileInfo ���� ���� ���� (�ӽ÷� �ذ�)
    }

    // ���⼭���� AI ���� ����
    public void OnAITurnStarted()
    {
        List<GameObject> aiUnitTiles = GetUnitTiles();    // AI ������ �����ϴ� Ÿ�� ������Ʈ ������Ʈ
        // List<GameObject> playerUnitTiles = new List<GameObject>();

        // AI ������ ��� ���ݷ��� ���� ������ ����
        aiUnitTiles.Sort((a, b) => b.GetComponent<TileInfo>().unit.currentAttackPoint.CompareTo(a.GetComponent<TileInfo>().unit.currentAttackPoint));

        foreach (GameObject tile in aiUnitTiles)
        {
            List<GameObject> targetTiles = MapManager.Instance.GetManhattanTiles(tile, tile.GetComponent<TileInfo>().unit.currentAttackRange);
            targetTiles.RemoveAll(tile => tile.GetComponent<TileInfo>().unit == null);
            targetTiles.RemoveAll(tile => tile.GetComponent<TileInfo>().unit.team == Team.Enemy);

            // ���� AI ������ ���� ���� ���� �� ������ ������ ���� AI ���� ����
            if (targetTiles.Count == 0)
                continue;

            targetTiles.Sort((a, b) => a.GetComponent<TileInfo>().unit.currentAttackPoint.CompareTo(b.GetComponent<TileInfo>().unit.currentHealth));  // Player ������ ��� ü���� ���� ������ ����
            
            // ���� ����
            Unit aiUnit = tile.GetComponent<TileInfo>().unit;
            Unit playerUnit = targetTiles[0].GetComponent<TileInfo>().unit;

            int realDamage = Mathf.Max(aiUnit.currentAttackPoint - playerUnit.currentDefensePoint, 0);
            playerUnit.currentHealth -= realDamage;
            
            Debug.Log($"{aiUnit.basicStats.unitName}��(��) {playerUnit.basicStats.unitName}��(��) ������");
            Debug.Log($"{realDamage}�� ���ظ� �԰� ü���� {playerUnit.currentHealth}��(��) ��");

            // ����� �״� ���
            if (playerUnit.currentHealth <= 0)
            {
                Debug.Log($"{playerUnit.basicStats.unitName}��(��) �����!");
                targetTiles[0].GetComponent<TileInfo>().unit = null;
                Destroy(targetTiles[0].GetComponent<TileInfo>().unitPrefab);

                InGameManager.Instance.playerDeathCount++;
                if (InGameManager.Instance.playerDeathCount == 5)
                        InGameManager.Instance.GameEnd();
            }
            return;
        }

        // ���� �տ��� ������ �������� �ʾҴٸ� �̵� ����

        // AI ������ ��� 1. ü��, 2. ������ ���� ������ ����
        aiUnitTiles.Sort((a, b) =>
        {
            int healthComparison = b.GetComponent<TileInfo>().unit.currentHealth.CompareTo(a.GetComponent<TileInfo>().unit.currentHealth);
            if (healthComparison == 0)
            {
                // ü���� ���� ��� ������ ���� ���� �켱
                return b.GetComponent<TileInfo>().unit.currentDefensePoint.CompareTo(a.GetComponent<TileInfo>().unit.currentDefensePoint);
            }
            return healthComparison;
        });

        foreach (GameObject tile in aiUnitTiles)
        {
            // ���� �������κ��� �̵� �Ÿ� �̳��� �̵� ������ Ÿ���� ����
            Unit aiUnit = tile.GetComponent<TileInfo>().unit;
            List<GameObject> possibleTiles = MapManager.Instance.GetManhattanTiles(tile, aiUnit.currentMoveRange);
            possibleTiles.RemoveAll(tile => tile.GetComponent<TileInfo>().unit != null);    // ������ �ִ� Ÿ���� ����Ʈ���� ����

            // ���� ������ �̵��� �� ���� ��� ���� ������ ����
            if (possibleTiles.Count == 0)
                continue;

            // �̵� ������ Ÿ�� �� �������� �̵�
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

            Debug.Log($"{aiUnit.basicStats.unitName}��(��) ({targetTile.GetComponent<TileInfo>().x},{targetTile.GetComponent<TileInfo>().y})�� �̵���");

            return;
        }
    }

    // player2Units�� ������ ������ �����ϰ� �߰�
    private void RandomSelection(int num)
    {
        List<int> aiFactionUnitCodes = new List<int>();

        if (GameManager.Instance.playerFaction == Faction.Guwol)
            aiFactionUnitCodes = UnitManager.Instance.GetAllUnitCodes(Faction.Seo);
        else if (GameManager.Instance.playerFaction == Faction.Seo)
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

    // AI�� ������ �����ϰ� ��ġ (TileInfo�� Start()�� ��� ȣ��� ���Ŀ� ����Ǿ� ��)
    public void RandomDeploy()
    {
        // �����ϰ� 5���� ����
        RandomSelection(aiUnitNum);
        aiUnitCodes = UnitManager.Instance.player2UnitCodes;
        aiUnits = UnitManager.Instance.player2Units;

        // �ʱ�ȭ
        Reset();

        // Player2�� ��ġ ������ Ÿ�� ������ ȹ��
        List<TileInfo> deployableTileInfos = MapManager.Instance.GetTileInfos(InitialDeployment.Player2);

        // AI ������ ��ġ�� Ÿ���� ����
        List<TileInfo> targetTileInfos = deployableTileInfos
            .OrderBy(x => Guid.NewGuid())
            .Take(aiUnitNum)
            .ToList();

        GameObject unitPrefab;
        for (int i = 0; i < aiUnitNum; i++)
        {
            // TileInfo ������Ʈ
            targetTileInfos[i].unit = aiUnits[i];
            // �ð��� ������Ʈ
            unitPrefab = UnitPrefabManager.Instance.InstantiateUnitPrefab(aiUnitCodes[i], 2.0f, false);
            unitPrefab.transform.position = targetTileInfos[i].worldXY;
            targetTileInfos[i].unitPrefab = unitPrefab;     // ���� ���� ���� ����!!!!!!!!!!!
            unitPrefab.transform.SetParent(InitialDeployManager.Instance.ActiveUnits.transform);
            aiUnitPrefabs.Add(unitPrefab);

        }

        isAllDeployed = true;
    }

    // Player2(AI)�� ��ġ ������ ������ ���� �ʱ�ȭ (TileInfo, �ð��� ������Ʈ)
    private void Reset()
    {
        // TileInfo ������Ʈ
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

    // AI ������ �����ϴ� Ÿ�� ������Ʈ ����Ʈ�� ��ȯ
    private List<GameObject> GetUnitTiles()
    {
        List<GameObject> tiles = new List<GameObject>();

        foreach (GameObject tile in MapManager.Instance.allTiles)
        {
            TileInfo tileInfo = tile.GetComponent<TileInfo>();
            // ������ �����ϸ鼭 �� ������ ���
            if (tileInfo.unit != null && tileInfo.unit.team == Team.Enemy)
                tiles.Add(tile);
        }
        return tiles;
    }
}
