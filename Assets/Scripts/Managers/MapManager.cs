using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager
{
    private static MapManager instance;
    public static MapManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MapManager();
            }
            return instance;
        }
    }

    private MapManager() { }

    public void InitializeMap()
    {
        Debug.Log("MapManager: 맵 초기화");
        // 맵 생성 로직 추가
    }
}

