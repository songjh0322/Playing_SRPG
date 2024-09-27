using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ���� �Ŵ����� ��� �Ŵ����� ���� �Ŵ�����, �����ϰ� MonoBehaviour�� ��ӹ޴� �Ŵ�����
public class GameManager : MonoBehaviour
{
    // ��� �Ŵ������� �̱������� ������ ����

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

    }

    private void Update()
    {
        
    }

    // Ÿ�� Ŭ�� �� ȣ��Ǵ� �޼���
    public void OnTileClicked(Tile clickedTile)
    {
        // ����ư �Ÿ� 3 �̳��� Ÿ���� ã�� ���� ����
        List<Tile> reachableTiles = mapManager.GetReachableTiles(3, clickedTile);
        foreach (Tile tile in reachableTiles)
        {
            tile.ChangeColor(Color.green); // �ʷϻ����� ����
        }
    }
}
