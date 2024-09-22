using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    void Start()
    {

        // 여기서부터 인게임

        // BasicMap 인스턴스 생성(현재 맵이 하나이므로 선택없이 즉시 생성)
        BasicMap map = new BasicMap();
    }

    private void Update()
    {
        
    }
}
