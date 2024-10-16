using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapCharacterPlacement : MonoBehaviour
{
    public Tilemap tilemap; // 타일맵 참조
    private List<GameObject> characterPrefabs; // 동적으로 할당될 캐릭터 프리팹 리스트
    private GameObject currentCharacter; // 현재 선택된 캐릭터
    private bool isPlacing = false; // 캐릭터 배치 중인지 확인
    private HashSet<int> placedCharacterIndices = new HashSet<int>(); // 배치된 캐릭터 인덱스를 저장하는 HashSet

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        // GameManager2에서 선택된 캐릭터 프리팹을 가져옴
        characterPrefabs = GameManager2.instance.selectedCharacterPrefabs;
        if (characterPrefabs != null && characterPrefabs.Count > 0)
        {
            Debug.Log("CharacterPrefabs 리스트가 잘 할당되었습니다. 프리팹 개수: " + characterPrefabs.Count);
            for (int i = 0; i < characterPrefabs.Count; i++)
            {
                Debug.Log($"[{i}] 프리팹 이름: {characterPrefabs[i].name}");
            }
        }
        else
        {
            Debug.LogError("CharacterPrefabs 리스트가 null이거나 빈 상태입니다!");
        }
    }

    void Update()
    {
        if (isPlacing && currentCharacter != null)
        {
            // 마우스 위치에 캐릭터를 따라가게 함
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
            currentCharacter.transform.position = new Vector3(worldPos.x, worldPos.y, 0);  // 2D 기준으로 이동
        }

        // 마우스를 클릭하면 캐릭터를 타일맵에 배치
        if (isPlacing && Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);

            // 타일맵 좌표로 변환
            Vector3Int tilePosition = tilemap.WorldToCell(worldPos);
            Vector3 tileWorldPosition = tilemap.GetCellCenterWorld(tilePosition);

            // 타일 위치에 캐릭터 배치
            currentCharacter.transform.position = new Vector3(tileWorldPosition.x, tileWorldPosition.y, 0); // Z를 0으로 설정
            isPlacing = false;
            currentCharacter = null;
        }
    }

    // 캐릭터 버튼을 클릭하면 캐릭터를 선택하여 타일맵에 배치
    public void OnCharacterButtonClick(int characterIndex)
    {
        // 이미 배치된 캐릭터라면 다시 배치하지 않도록 막음
        if (placedCharacterIndices.Contains(characterIndex))
        {
            Debug.LogWarning("이미 배치된 캐릭터입니다: " + characterPrefabs[characterIndex].name);
            return;
        }

        if (currentCharacter != null)
        {
            Destroy(currentCharacter); // 기존 캐릭터 제거
            currentCharacter = null;   // null로 설정하여 중복 방지
        }

        currentCharacter = Instantiate(characterPrefabs[characterIndex]); // 캐릭터 인스턴스 생성
        currentCharacter.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f); // 크기 변경
        isPlacing = true; // 배치 모드 활성화

        // 캐릭터를 배치한 것으로 간주하고 인덱스를 저장
        placedCharacterIndices.Add(characterIndex);
    }
}
