using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //protected UIManager uiManager = new UIManager();
    //protected TurnManager turnManager = new TurnManager();
    protected UnitManager unitManager;
    protected MapManager mapManager;

    void Start()
    {
        unitManager = new UnitManager();
        mapManager = new MapManager();

        unitManager.LoadUnitDataFromJSON();
        mapManager.LoadPrefabs();
        mapManager.CreateMap();

        Unit newUnit = unitManager.CreateUnit("ö��");
        if (newUnit != null)
        {
            Debug.Log("ö���� ���ݷ�: " + newUnit.attackPoint);
            Debug.Log("ö���� �ִ� ü��: " + newUnit.maxHealth);
            Debug.Log("ö���� ���� ü��: " + newUnit.currentHealth);
            Debug.Log("ö���� �нú� �̸�: " + newUnit.passiveName);
        }
        else
        {
            Debug.LogError("������ �����ϴ� �� �����߽��ϴ�.");
        }
    }

    private void Update()
    {
        
    }
}
