using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 instance;  // 싱글톤 패턴으로 모든 씬에서 GamaManager에 쉽게 접근할 수 있도록 함.
    private void Awake()
    {
        //싱글톤 패턴
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //씬이 바뀌어도 GameManager가 파괴되지 않도록..

        }
        else
        {
            Destroy(gameObject);
        }
    }
    //씬 전환 함수
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
        

}
