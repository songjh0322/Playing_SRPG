using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject tilePrefab; // Inspector에서 할당할 프리팹
    private MapManager mapManager;

    void Start()
    {
        // MapManager 초기화
        mapManager = new MapManager(tilePrefab);

        // 맵 생성
        mapManager.CreateMap();
    }

    private void Update()
    {
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
    }
}
