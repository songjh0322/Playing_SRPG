using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

// JSON ���Ͽ��� �⺻ �ɷ�ġ���� �ҷ����� ���� Ŭ���� (�̰������� ���)
public class UnitDataWrapper
{
    public List<BasicStats> statsWrapper;
}

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance { get; private set; }

    public const int maxUnits = 5;      // �ִ� ���� ���� ��

    public List<BasicStats> basicStatsList;     // ��� �⺻ �ɷ�ġ�� ������ List (����)
    public List<Unit> allUnits;    // ��� ������ ������ List (����, �ΰ��ӿ��� ������� ����)

    public List<int> player1UnitCodes;
    public List<int> player2UnitCodes;
    public List<Unit> player1Units;      // Player1�� �ΰ��ӿ��� ������ ����� ���� ����Ʈ (����)
    public List<Unit> player2Units;      // Player2�� �ΰ��ӿ��� ������ ����� ���� ����Ʈ (����)

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            basicStatsList = new List<BasicStats>();
            allUnits = new List<Unit>();
            player1UnitCodes = new List<int>();
            player2UnitCodes = new List<int>();
            player1Units = new List<Unit>();
            player2Units = new List<Unit>();
        }
    }
    private void Start()
    {
        
    }

    // ��� : JSON ���Ϸκ��� �����͸� basicStatsList�� ����
    // ���� : ���� ���� �� ���� 1ȸ�� ȣ���ؾ� ��
    public void LoadBasicStatsFromJSON()
    {
        // JSON ���� ���
        string jsonFilePath = Path.Combine(Application.dataPath, "Resources/BasicStats.json");

        // JSON ������ �б�
        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);

            // JSON �����͸� UnitDataWrapper�� ��ȯ
            UnitDataWrapper unitDataWrapper = JsonUtility.FromJson<UnitDataWrapper>(jsonData);

            // ��ȯ�� �����͸� List�� �Ҵ�
            basicStatsList = unitDataWrapper.statsWrapper;
        }
        else
        {
            Debug.LogError("BasicStats.json ������ ã�� �� �����ϴ�.");
        }
    }

    // ��� : basicStatsList�κ��� Unit ��ü�� ��� �����ϰ� allUnits�� ����
    // ���� : ���� ���� �� ���� 1ȸ�� ȣ���ؾ� ��
    public void LoadAllUnits()
    {
        if (basicStatsList == null)
            Debug.LogError("basicStatsList�� �ʱ�ȭ���� �ʾҽ��ϴ�.");
        else
        {
            foreach (BasicStats basicStats in basicStatsList)
                allUnits.Add(new Unit(basicStats));
        }
    }

    // ��� : [ĳ���� ������ ��� ���ƽ��ϴ�. ������ �����Ͻðڽ��ϱ�?] -> [��] ��ư Ŭ�� �� ȣ��
    // �Ķ���� : UI���� ������ ���ֵ��� �̸��� ���� List
    // ��� : Player1�� ����� ���ֵ��� ĳ���� �̸��� ���� ���� ����
    public void ConfirmPlayer1Units(List<string> unitNames)
    {
        player1Units.Clear();

        if (unitNames == null)
            Debug.LogError("ConfirmPlayer1Units�� �Ķ���Ͱ� null�Դϴ�.");
        else
        {
            foreach (string unitName in unitNames)
            {
                Unit foundUnit = allUnits.Find(unit => unit.basicStats.unitName == unitName);

                if (foundUnit != null)
                {
                    Unit newUnit = new Unit(foundUnit);     // ������ �ƴ� ����
                    player1Units.Add(newUnit);
                }
            }
        }
    }

    // ���� �̻�� �Լ�
    public void ConfirmPlayer2Units()
    {
        
    }

    // ��� : [ĳ���� ������ ��� ���ƽ��ϴ�. ������ �����Ͻðڽ��ϱ�?] -> [��] ��ư Ŭ�� �� ȣ��
    // ��� : Player2�� ����� ���ֵ��� �������� ���� (�ݴ� �������� ���Ƿ� ����)
    // ���� ��� �Ұ�
    public void RandomizePlayer2Units()
    {
        player2Units.Clear();
        List<Unit> allUnitsInFaction = new List<Unit>();

        // ���� ������ ������ �����ϰ� ����
        Random random = new Random();
        List<Unit> shuffledUnits = allUnitsInFaction.OrderBy(x => random.Next()).ToList();

        // maxUnits��ŭ �߰�
        for (int i = 0; i < maxUnits; i++)
        {
            Unit originalUnit = shuffledUnits[i];
            Unit newUnit = new Unit(originalUnit);
            player2Units.Add(newUnit);
        }
    }

    // unitCode�� ���� �����Ǵ� Unit ��ü�� ����
    public Unit GetUnit(int unitCode)
    {
        foreach (Unit unit in allUnits)
        {
            if (unitCode == unit.basicStats.unitCode)
                return unit;
        }

        return null;
    }

    // ���� ������ ���� �ش� ���ֵ��� ����Ʈ�� ����
    public List<Unit> GetUnits(Faction faction)
    {
        List<Unit> factionUnits = new List<Unit>();
        foreach (Unit unit in allUnits)
        {
            if (faction == unit.basicStats.faction)
                factionUnits.Add(unit);
        }
        return factionUnits;
    }

    public List<int> GetUnitCodes(Faction faction)
    {
        List<int> unitCodes = new List<int>();
        foreach (Unit unit in allUnits)
        {
            if (faction == unit.basicStats.faction)
                unitCodes.Add(unit.basicStats.unitCode);
        }
        return unitCodes;
    }
}

