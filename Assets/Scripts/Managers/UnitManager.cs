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

    public Dictionary<string, BasicStats> guwol_basicStatsData;  // ������ ��ܼ� �⺻ �ɷ�ġ ��ųʸ�
    public Dictionary<string, BasicStats> seo_basicStatsData;    // ���� ���� �⺻ �ɷ�ġ ��ųʸ�

    public List<Unit> player1Units = new List<Unit>();      // Player1�� �ΰ��ӿ��� ����� ���� ����Ʈ (����)
    public List<Unit> player2Units = new List<Unit>();      // Player2�� �ΰ��ӿ��� ����� ���� ����Ʈ (����)

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
    public void LoadBasicStatsFromJSON()
    {
        TextAsset jsonTextFile1 = Resources.Load<TextAsset>("GuwolStats");
        TextAsset jsonTextFile2 = Resources.Load<TextAsset>("SeoStats");

        if (jsonTextFile1 != null && jsonTextFile2 != null)
        {
            UnitStatsList statsList1 = JsonUtility.FromJson<UnitStatsList>(jsonTextFile1.text);
            UnitStatsList statsList2 = JsonUtility.FromJson<UnitStatsList>(jsonTextFile2.text);

            guwol_basicStatsData = new Dictionary<string, BasicStats>();
            seo_basicStatsData = new Dictionary<string, BasicStats>();

            // JSON �����͸� ��ųʸ��� ��ȯ�Ͽ� ����
            foreach (BasicStats stats in statsList1.units)
            {
                guwol_basicStatsData[stats.unitName] = stats;
            }
            foreach (BasicStats stats in statsList2.units)
            {
                seo_basicStatsData[stats.unitName] = stats;
            }

            Debug.Log("���������� JSON ������ �ε��߽��ϴ�.");
        }
        else
        {
            Debug.LogError("CharacterStats.json ������ ã�� �� �����ϴ�.");
        }
    }

    // ��� : [ĳ���� ������ ��� ���ƽ��ϴ�. ������ �����Ͻðڽ��ϱ�?] -> [��] ��ư Ŭ�� �� ȣ��
    // �Ķ���� : UI���� ������ ���ֵ��� �̸��� ���� List
    // ��� : Player1�� ����� ���ֵ��� ���� ����
    // ���� ������ ������ ��ܼ��� �����Ǿ� ����!
    public void ConfirmPlayer1Units(List<string> unitNames)
    {
        for (int i = 0; i < unitNames.Count; i++)
        {
            player1Units.Add(new Unit(unitNames[i], guwol_basicStatsData));
        }
    }

    // ���� �̻�� �Լ�
    public void ConfirmPlayer2Units()
    {
        
    }

    // ��� : [ĳ���� ������ ��� ���ƽ��ϴ�. ������ �����Ͻðڽ��ϱ�?] -> [��] ��ư Ŭ�� �� ȣ��
    // ��� : Player2�� ����� ���ֵ��� �������� ���� (�ݴ� �������� ���Ƿ� ����)
    public void RandomizePlayer2Units()
    {

    }
}

