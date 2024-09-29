using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

// 게임 매니저는 모든 매니저를 관리하는 매니저로, 유일하게 MonoBehaviour를 상속받는 매니저
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    protected UIManager uiManager;
    protected TurnManager turnManager;
    protected UnitManager unitManager;
    protected MapManager mapManager;
    protected CharacterSelectionManager characterSelectionManager;
    protected DeployManager deployManager;

    public Player1Camp player1Camp;     // Player1이 선택한 진영
    public Player1Camp player2Camp;     // Player2가 선택한 진영
    

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

        unitManager = new UnitManager();

        // 게임 프로그램이 실행되면 모든 매니저를 불러옴
        uiManager = UIManager.Instance;
        turnManager = TurnManager.Instance;
        //unitManager = UnitManager.Instance;
        mapManager = MapManager.Instance;
        characterSelectionManager = CharacterSelectionManager.Instance;
        deployManager = DeployManager.Instance;
    }

    void Start()
    {
        // 필수 요소 (필요한 유닛 데이터, 프리팹 불러오기)
        //unitManager.LoadBasicStatsFromJSON();
        
        // 임시 할당 (특정 씬에서 바로 테스트를 하기 위함)
        player1Camp = Player1Camp.Guwol;    
        player1Camp = Player1Camp.Seo;

        // !! 아래의 코드들은 일련의 호출 예시임 !!

        // 예시 - 전투가 시작되면 순서대로 호출 (인자는 UI에서 받음, 현재는 임의로 넣음)
        //unitManager.ConfirmPlayer1Units(new List<string> { "철봉", "딱쇠", "서빈", "갑이", "환조", "달구지"});
        //unitManager.RandomizePlayer2Units(); // 또는 unitManager.ConfirmPlayer2Units(...)
        //mapManager.CreateMap();

        // 유닛 배치 단계 진입 시
        // 배치 단계에서 이미 배치를 완료한 캐릭터의 경우, 해당 캐릭터의 버튼을 비활성화하는 것은 UI 관리에서 수행
        // 하나의 유닛을 배치할 때마다 화면 업데이트가 있으므로 List로 관리
        //deployManager.StartDeploy();

        // [배치 완료] 버튼 클릭 시
    }

    // 디버깅용
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (Unit unit in unitManager.player2Units)
                Debug.Log($"{unit.unitName}");
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            //Debug.Log($"{unitManager.player1Units[0].unitName}");
            foreach (Unit unit in unitManager.player1Units)
                Debug.Log($"{unit.unitName}");
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

public enum Player1Camp
{
    Guwol,
    Seo,
}
