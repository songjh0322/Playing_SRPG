using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ���� �Ŵ����� ��� �Ŵ����� �����ϴ� �Ŵ�����, �����ϰ� MonoBehaviour�� ��ӹ޴� �Ŵ���
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    protected UIManager uiManager;
    protected TurnManager turnManager;
    protected UnitManager unitManager;
    protected MapManager mapManager;

    private void Awake()
    {
        // �̱��� �ν��Ͻ� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);      // Scene�� ����Ǿ �Ŵ������� ������
        }
        else
        {
            Destroy(gameObject);
        }

        // �� �Ŵ����� �ʱ�ȭ
        uiManager = UIManager.GetInstance();
        turnManager = TurnManager.GetInstance();
        unitManager = UnitManager.GetInstance();
        mapManager = MapManager.GetInstance();
    }

    void Start()
    {
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
