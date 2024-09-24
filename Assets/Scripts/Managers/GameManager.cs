using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        // 맵과 UI 초기화
        MapManager.Instance.InitializeMap();
        UIManager.Instance.InitializeUI();
    }

    private void Start()
    {
        // 게임이 시작될 때 필요한 추가 작업 수행
        Debug.Log("GameManager: 게임 시작");
    }

    // Update 함수는 필요할 때 사용
    private void Update()
    {
        // 게임 상태 업데이트
    }
}

