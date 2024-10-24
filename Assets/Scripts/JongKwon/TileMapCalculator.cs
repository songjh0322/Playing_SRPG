using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapCalculator : MonoBehaviour
{
    public Tilemap tilemap; // Tilemap 컴포넌트 참조

    void Awake()
    {
        // 스크립트가 붙은 게임 오브젝트에 있는 Tilemap 컴포넌트를 자동으로 가져옴
        tilemap = GetComponent<Tilemap>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 클릭 감지
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 클릭한 화면 좌표를 월드 좌표로 변환
            Vector2 mousePos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y); // 2D 공간에서 사용할 좌표

            // 클릭한 위치에 있는 Collider 감지 (TilemapCollider2D 포함)
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null)
            {
                // 타일맵에서 클릭된 위치의 셀 좌표 얻기
                Vector3Int tilePos = tilemap.WorldToCell(hit.point);

                // 셀의 중앙 좌표 얻기
                Vector3 tileCenterPos = tilemap.GetCellCenterWorld(tilePos);

                Debug.Log("Tile Center Position: " + tileCenterPos);
            }
        }
    }
}
