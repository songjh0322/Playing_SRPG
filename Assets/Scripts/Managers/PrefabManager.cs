using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 현재 이용하지 않음
// 규모 커지면 따로 사용할 가능성 있음
public class PrefabManager : MonoBehaviour
{
    // UI 관련
    // 예시 : public GameObject buttonPrefab;

    // 인게임 요소 (맵이나 타일, 캐릭터 등)
    public GameObject tilePrefab;
    // public GameObject characterPrefab;

    private void Awake()
    {
        LoadPrefabs();
    }

    // 지정 경로에서 프리팹 로딩
    private void LoadPrefabs()
    {
        // buttonPrefab = Resources.Load<GameObject>("Prefabs/ButtonPrefab");
        tilePrefab = Resources.Load<GameObject>("Prefabs/Tiles/basic_tile");
        // characterPrefab = Resources.Load<GameObject>("Prefabs/CharacterPrefab");
    }
}
