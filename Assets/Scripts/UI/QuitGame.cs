using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        // Unity 에디터에서 실행 중인지 확인하고 메시지 출력
#if UNITY_EDITOR
        // 에디터에서는 실제로 종료되지 않으므로, 메시지만 출력
        Debug.Log("게임 종료");
#else
        Application.Quit();
#endif
    }
}