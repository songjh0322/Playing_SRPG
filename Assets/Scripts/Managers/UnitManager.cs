using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

// JSON ���Ͽ��� �⺻ �ɷ�ġ���� �ҷ����� ���� Ŭ���� (�̰������� ���)
public class UnitDataWrapper
{
    public List<BasicStats> statsWrapper;
}

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance { get; private set; }

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
            unitsList = new List<Unit>();
            player1Units = new List<Unit>();
            player2Units = new List<Unit>();
        }
    }

    public const int maxUnits = 6;      // �ִ� ���� ���� ��

    public List<BasicStats> basicStatsList;     // ��� �⺻ �ɷ�ġ�� ������ List (����)
    public List<Unit> unitsList;    // ��� ������ ������ List (����)

    public List<Unit> player1Units;      // Player1�� �ΰ��ӿ��� ����� ���� ����Ʈ (����)
    public List<Unit> player2Units;      // Player2�� �ΰ��ӿ��� ����� ���� ����Ʈ (����)

    private void Start()
    {
        
    }

    // ��� : JSON ���Ϸκ��� �����͸� basicStatsList�� ����
    // ���� : ���� ���� �� ���� 1ȸ�� ȣ��
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

    // ��� : basicStatsList�κ��� Unit ��ü�� ��� �����ϰ� unitsList�� ����
    // ���� : ���� ���� �� ���� 1ȸ�� ȣ��
    public void LoadAllUnits()
    {
        if (basicStatsList == null)
            Debug.LogError("basicStatsList�� �ʱ�ȭ���� �ʾҽ��ϴ�.");
        else
        {
            foreach (BasicStats basicStats in basicStatsList)
                unitsList.Add(new Unit(basicStats));
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
                Unit foundUnit = unitsList.Find(unit => unit.basicStats.unitName == unitName);

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
    public void RandomizePlayer2Units()
    {
        player2Units.Clear();

        if (GameManager.Instance.playerFaction == PlayerFaction.Guwol)
    }


}

