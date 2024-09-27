using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 게임 매니저는 모든 매니저의 상위 매니저로, 유일하게 MonoBehaviour를 상속받는 매니저임
public class GameManager : MonoBehaviour
{
    // 모든 매니저들을 싱글톤으로 수정할 예정

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
