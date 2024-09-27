using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// JSON ���Ͽ��� ���� ����� �ҷ����� ���� Ŭ����
[Serializable]
public class UnitStatsList
{
    public BasicStats[] units;  // BasicStats �迭
}

public class UnitManager
{
    private Dictionary<string, BasicStats> basicStatsData;  // �⺻ ���� �ɷ�ġ(JSON ������)�� ���� ��ųʸ�
    public List<Unit> activeUnits = new List<Unit>();       // �� �� �����ǰ�, ��ġ�� ������ ��� ����Ʈ

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
            Debug.LogError(unitName + " ������ JSON �����Ϳ� �����ϴ�.");
            return null;
        }
    }

    // UI : [ĳ���� ������ ��� ���ƽ��ϴ�. ������ �����Ͻðڽ��ϱ�?]
    // [��]�� Ŭ���� ��� ȣ��
    public void ConfirmPlayer1Units()
    {

    }

    public void RandomizePlayer2Units()
    {

    }
}

