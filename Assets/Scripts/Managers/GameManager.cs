using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    protected UIManager uiManager = new UIManager();
    protected TurnManager turnManager = new TurnManager();

    // MapManager를 선언하지만 인스턴스화는 자식 클래스에서 진행
    protected MapManager mapManager;

    void Start()
    {
        // 맵을 생성하고 Scene에 표시
        mapManager = new MapManager(); // MapManager 인스턴스화
        mapManager.LoadPrefabs(); // 프리팹 로드
        mapManager.CreateMap();
    }

    private void Update()
    {
        
    }
}
