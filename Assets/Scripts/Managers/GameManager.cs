using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 게임 매니저는 모든 매니저를 관리하는 매니저로, 유일하게 MonoBehaviour를 상속받는 매니저
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    protected UIManager uiManager;
    protected TurnManager turnManager;
    protected UnitManager unitManager;
    protected MapManager mapManager;

    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);      // Scene이 변경되어도 매니저들은 유지됨
        }
        else
        {
            Destroy(gameObject);
        }

        // 각 매니저를 초기화
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

    // 타일 클릭 시 호출되는 메서드
    public void OnTileClicked(Tile clickedTile)
    {
        // 맨하튼 거리 3 이내의 타일을 찾고 색상 변경
        List<Tile> reachableTiles = mapManager.GetReachableTiles(3, clickedTile);
        foreach (Tile tile in reachableTiles)
        {
            tile.ChangeColor(Color.green); // 초록색으로 변경
        }
    }
}
