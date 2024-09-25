using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UIManager uiManager = new UIManager();
    private MapManager mapManager = new MapManager();
    private TurnManager turnManager = new TurnManager();

    void Start()
    {
        // 맵을 생성하고 Scene에 표시
        mapManager.CreateMap();
    }

    private void Update()
    {
        
    }
}
