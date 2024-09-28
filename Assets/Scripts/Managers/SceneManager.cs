using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager
{
    public static SceneManager Instance { get; private set; }

    // 멤버

    // 싱글톤 인스턴스를 반환
    public static SceneManager GetInstance()
    {
        if (Instance == null)
        {
            Instance = new SceneManager();
        }
        return Instance;
    }

    // private 생성자 (외부에서 직접 생성하지 못하도록 함)
    private SceneManager()
    {
        // UI 관련 초기화
    }

    
}
