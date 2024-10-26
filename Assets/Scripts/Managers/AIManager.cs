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
    }

    private void Update()
    {

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
}
