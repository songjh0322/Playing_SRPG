using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AIRandomPlacement : MonoBehaviour
{
    public Tilemap tilemap;  // 타일맵 참조
    public GameObject[] aiPrefabs;  // AI 캐릭터 프리팹 배열
    private List<Vector3Int> availableTiles = new List<Vector3Int>();  // 유효한 타일 좌표 리스트

    private void Start()
    {
        // 타일맵 상의 모든 타일 좌표를 가져옴
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        // 유효한 타일 좌표를 저장 (타일이 존재하는 좌표)
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);
                if (tilemap.HasTile(tilePos))
                {
                    availableTiles.Add(tilePos);  // 유효한 타일을 리스트에 추가
                }
            }
        }

        // AI 캐릭터를 랜덤 위치에 배치
        PlaceAIs();
    }

    // AI 캐릭터를 랜덤한 위치에 배치하는 함수
    private void PlaceAIs()
    {
        foreach (GameObject aiPrefab in aiPrefabs)
        {
            if (availableTiles.Count > 0)
            {
                // 랜덤한 타일 좌표 선택
                int randomIndex = Random.Range(0, availableTiles.Count);
                Vector3Int tilePos = availableTiles[randomIndex];

                // 타일맵 상의 월드 좌표로 변환
                Vector3 worldPos = tilemap.CellToWorld(tilePos) + tilemap.cellSize / 2;  // 중앙에 배치되도록 조정

                // AI 캐릭터 생성 및 위치 설정
                Instantiate(aiPrefab, worldPos, Quaternion.identity);

                // 사용한 타일 좌표는 리스트에서 제거
                availableTiles.RemoveAt(randomIndex);
            }
        }
    }
}
