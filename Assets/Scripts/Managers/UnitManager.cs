using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// JSON ���Ͽ��� ���� ����� �ҷ����� ���� Ŭ���� (�̰������� ���)
[Serializable]
public class UnitStatsList
{
    public BasicStats[] units;  // BasicStats �迭
}

public class UnitManager
{
    public static UnitManager Instance { get; private set; }

    public const int maxUnits = 6;      // �ִ� ���� ���� ��

    public Dictionary<string, BasicStats> basicStatsData;  // �⺻ ���� �ɷ�ġ(JSON ������)�� ���� ��ųʸ�
    public List<Unit> activeUnits = new List<Unit>();       // �� �� �����ǰ�, ��ġ�� ������ ��� ����Ʈ
    public List<Unit> player1Units = new List<Unit>();      // Player1�� �ΰ��ӿ��� ����� ���� ����Ʈ
    public List<Unit> player2Units = new List<Unit>();      // Player2�� �ΰ��ӿ��� ����� ���� ����Ʈ

    // �̱��� �ν��Ͻ� ����
    private UnitManager()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // �̱��� �ν��Ͻ��� ��ȯ
    public static UnitManager GetInstance()
    {
        if (Instance == null)
        {
            Instance = new UnitManager();
        }
        return Instance;
    }

    // JSON �����͸� �ҷ����� �޼���
    public void LoadUnitDataFromJSON()
    {
        TextAsset jsonTextFile = Resources.Load<TextAsset>("CharacterStats");
        if (jsonTextFile != null)
        {
            UnitStatsList statsList = JsonUtility.FromJson<UnitStatsList>(jsonTextFile.text);

            basicStatsData = new Dictionary<string, BasicStats>();

            // JSON �����͸� ��ųʸ��� ��ȯ�Ͽ� ����
            foreach (BasicStats stats in statsList.units)
            {
                basicStatsData[stats.unitName] = stats;
            }

            Debug.Log("���������� JSON ������ �ε��߽��ϴ�.");
        }
        else
        {
            Debug.LogError("CharacterStats.json ������ ã�� �� �����ϴ�.");
        }
    }

    // ������ �����ϴ� �޼���
    public Unit CreateUnit(string unitName)
    {
        if (basicStatsData.ContainsKey(unitName))
        {
            Unit newUnit = new Unit(unitName, basicStatsData);
            activeUnits.Add(newUnit); // ������ ������ ����Ʈ�� �߰�
            Debug.Log(unitName + " ���� ���� �Ϸ�.");
            return newUnit;
        }
        else
        {
            Debug.LogError(unitName + " ������ JSON ���Ͽ� ���ǵǾ����� �ʽ��ϴ�.");
            return null;
        }
    }

    // ��� : [ĳ���� ������ ��� ���ƽ��ϴ�. ������ �����Ͻðڽ��ϱ�?] -> [��]�� Ŭ���� ��� ȣ��
    // �Ķ���� : UI���� ������ ���ֵ��� �̸��� ���� List
    // ��� : Player1�� ����� ���ֵ��� ���� ����
    public void ConfirmPlayer1Units(List<string> unitNames)
    {
        for (int i = 0; i < unitNames.Count; i++)
        {
            player1Units.Add(new Unit(unitNames[i], basicStatsData));
        }
    }

    // ���� �̻�� �Լ�
    public void ConfirmPlayer2Units()
    {

    }

    // ��� : [ĳ���� ������ ��� ���ƽ��ϴ�. ������ �����Ͻðڽ��ϱ�?] -> [��]�� Ŭ���� ��� ȣ��
    // ��� : Player2�� ����� ���ֵ��� �������� ���� (�ݴ� �������� ���Ƿ� ����)
    public void RandomizePlayer2Units()
    {

    }
}

