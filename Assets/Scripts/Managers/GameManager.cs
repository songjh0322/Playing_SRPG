using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
<<<<<<< HEAD
    public GameObject tilePrefab; // Inspector에서 할당할 프리팹
    private MapManager mapManager;

    void Start()
    {
        // MapManager 초기화
        mapManager = new MapManager(tilePrefab);

        // 맵 생성
        mapManager.CreateMap();
=======
    private void Awake()
    {
        // 맵과 UI 초기화
        MapManager.Instance.InitializeMap();
        UIManager.Instance.InitializeUI();
>>>>>>> 9e268010859d94b39b3f0e05584f489f7eddac52
    }

    private void Start()
    {
        // 게임이 시작될 때 필요한 추가 작업 수행
        Debug.Log("GameManager: 게임 시작");
    }

    // Update 함수는 필요할 때 사용
    private void Update()
    {
<<<<<<< HEAD
        /*if (Input.GetMouseButtonDown(0)) // 좌클릭 감지
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null) // 타일을 클릭했을 경우
            {
                Tile clickedTile = hit.collider.GetComponent<Tile>();
                if (clickedTile != null)
                {
                    // 타일의 행과 열을 출력
                    Debug.Log($"Clicked Tile - Row: {clickedTile.row}, Col: {clickedTile.col}");
                }
            }
        }*/
=======
        // 게임 상태 업데이트
>>>>>>> 9e268010859d94b39b3f0e05584f489f7eddac52
    }
}

