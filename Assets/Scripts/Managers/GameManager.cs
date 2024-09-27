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

        Unit newUnit = unitManager.CreateUnit("Ã¶ºÀ");
        if (newUnit != null)
        {
            Debug.Log("Ã¶ºÀÀÇ °ø°Ý·Â: " + newUnit.attackPoint);
            Debug.Log("Ã¶ºÀÀÇ ÃÖ´ë Ã¼·Â: " + newUnit.maxHealth);
            Debug.Log("Ã¶ºÀÀÇ ÇöÀç Ã¼·Â: " + newUnit.currentHealth);
            Debug.Log("Ã¶ºÀÀÇ ÆÐ½Ãºê ÀÌ¸§: " + newUnit.passiveName);
        }
        else
        {
            Debug.LogError("À¯´ÖÀ» »ý¼ºÇÏ´Â µ¥ ½ÇÆÐÇß½À´Ï´Ù.");
        }
    }

    private void Update()
    {
        
    }
}
