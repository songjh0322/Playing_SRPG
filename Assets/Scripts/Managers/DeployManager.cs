using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 유닛 배치를 관리하는 매니저
public class DeployManager
{
    public static DeployManager Instance { get; private set; }

    // 싱글톤 인스턴스 설정
    private DeployManager()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // 싱글톤 인스턴스를 반환
    public static DeployManager GetInstance()
    {
        if (Instance == null)
        {
            Instance = new DeployManager();
        }
        return Instance;
    }

    // 사용 : 배치 시작 시 가장 먼저 호출
    // 기능 : 
    public void StartDeploy()
    {
        
    }

    // 사용 : 배치 완료 시 마지막으로 호출
    // 기능 : 
    public void CompleteDeploy()
    {

    }
}
