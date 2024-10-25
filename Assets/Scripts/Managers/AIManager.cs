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
        // Debug.Log("AIManager�� ������");

        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        aiUnitNum = 5;
        aiUnitPrefabs = new List<GameObject>();

        // �����ϰ� 5���� ����
        RandomSelection(aiUnitNum);
        aiUnitCodes = UnitManager.Instance.player2UnitCodes;
        aiUnits = UnitManager.Instance.player2Units;

        //RandomDeploy(); // �ݵ�� TileInfo�� Start()�� ��� ȣ��� ���Ŀ� �����ؾ� ��
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RandomDeploy();
        }
    }

    // player2Units�� ������ ������ �����ϰ� �߰�
    public void RandomSelection(int num)
    {
        List<int> aiFactionUnitCodes = new List<int>();

        if (GameManager.Instance.playerFaction == Faction.Guwol)
            aiFactionUnitCodes = UnitManager.Instance.GetUnitCodes(Faction.Seo);
        else if (GameManager.Instance.playerFaction == Faction.Seo)
            aiFactionUnitCodes = UnitManager.Instance.GetUnitCodes(Faction.Guwol);

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
    }

    // AI�� ������ �����ϰ� ��ġ
    private void RandomDeploy()
    {
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
            unitPrefab = UnitPrefabManager.Instance.InstantiateUnitPrefab(aiUnitCodes[i], 2.0f);
            unitPrefab.transform.position = targetTileInfos[i].worldXY;
            aiUnitPrefabs.Add(unitPrefab);
        }
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
