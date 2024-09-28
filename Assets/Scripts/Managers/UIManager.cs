using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public static UIManager Instance { get; private set; }

    // 싱글톤 인스턴스를 반환
    public static UIManager GetInstance()
    {
        if (Instance == null)
        {
            Instance = new UIManager();
        }
        return Instance;
    }

    // private 생성자 (외부에서 직접 생성하지 못하도록 함)
    private UIManager()
    {
        // UI 관련 초기화
    }
}
