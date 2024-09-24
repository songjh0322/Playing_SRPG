using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIManager();
            }
            return instance;
        }
    }

    private UIManager() { }

    public void InitializeUI()
    {
        Debug.Log("UIManager: UI 초기화");
        // UI 초기화 로직 추가
    }
}

