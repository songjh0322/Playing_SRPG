using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

// 게임 매니저는 모든 매니저를 관리하는 매니저 (플레이어의 정보 또한 저장)
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerFaction playerFaction;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            this.AddComponent<UnitManager>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //GameObject unitManagerObject = new GameObject("@UnitManager");
        //unitManagerObject.AddComponent<UnitManager>();

        // 필수 요소 (필요한 유닛 데이터, 프리팹 불러오기)
        UnitManager.Instance.LoadBasicStatsFromJSON();
        UnitManager.Instance.LoadAllUnits();
    }

    // 디버깅용
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            foreach (Unit unit in UnitManager.Instance.player1Units)
                Debug.Log($"{unit.basicStats.unitName}");
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            foreach (Unit unit in UnitManager.Instance.player2Units)
                Debug.Log($"{unit.basicStats.unitName}");
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(CharacterSelectionManager.Instance.selectedCharacters.Count);
            Debug.Log(CharacterSelectionManager.Instance.selectedCharacters[0]);
        }
    }

    /*    // 타일 클릭 시 호출되는 메서드
        public void OnTileClicked(Tile clickedTile)
        {
            // MapManager의 GetReachableTiles() 함수를 호출하여 맨해튼 거리 3 이내의 타일을 가져옴
            List<Tile> reachableTiles = MapManager.Instance.GetReachableTiles(3, clickedTile);

            // 가져온 타일들의 색상을 초록색으로 변경
            foreach (Tile tile in reachableTiles)
            {
                tile.ChangeColor(Color.green);
            }
        }*/
}

public enum PlayerFaction
{
    Guwol,
    Seo,
}
