using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AIRandomPlacement : MonoBehaviour
{
    public Tilemap tilemap;  // 타일맵 참조
    public GameObject[] aiPrefabs;  // AI 캐릭터 프리팹 배열
    private List<Vector3Int> availableTiles = new List<Vector3Int>();  // 유효한 타일 좌표 리스트

    // 작은 네모 선의 좌표 범위 설정 
    public int xMin = 5;
    public int xMax = 9;
    public int yMin = -2;
    public int yMax = 7;

    private void Start()
    {
        // 타일맵 상의 모든 타일 좌표를 가져옴
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);


        for (int x = xMin; x <= xMax; x++)
        {
            for (int y = yMin; y <= yMax; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);
                if (tilemap.HasTile(tilePos))  // 타일이 존재하는지 확인
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
                Vector3 tileWorldPosition = tilemap.GetCellCenterWorld(tilePos); // 타일 중앙 좌표로 변환


                // AI 캐릭터 생성 및 위치 설정
                GameObject aiInstance = Instantiate(aiPrefab, tileWorldPosition, Quaternion.identity);
                aiInstance.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f); //크기 변경

                // 사용한 타일 좌표는 리스트에서 제거
                availableTiles.RemoveAt(randomIndex);
            }
        }
    }
}
