using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Scene5Switcher : MonoBehaviour
{
    // 인게임 화면으로 진입함
    public void MoveToScene5()
    {       
        //SceneManager.LoadScene("Scene5");

        SceneManager.LoadScene("InGameScene");
    }
}
