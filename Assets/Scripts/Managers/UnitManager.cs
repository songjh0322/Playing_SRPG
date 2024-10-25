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

    public List<BasicStats> basicStatsList;     // ��� �⺻ �ɷ�ġ�� �����ϴ� ����Ʈ (����)
    public List<Unit> allUnits;    // ��� ������ ������ List (����, �ΰ��ӿ��� ������� ����)

    // �ΰ��ӿ��� �����Ǵ� ���
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

    // unitCode�� ���� �����Ǵ� Unit ��ü�� ���� (���� : �ΰ��ӿ��� ����ϸ� �ȵ�)
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

    // unitCode�� ���� player1Units���� Unit ��ü�� ���� (�ΰ��ӿ��� ���)
    public Unit GetPlayer1Unit(int unitCode)
    {
        foreach (Unit unit in player1Units)
        {
            if (unit.basicStats.unitCode == unitCode)
                return unit;
        }
        return null;
    }
}

