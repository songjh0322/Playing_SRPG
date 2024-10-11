using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 instance;  // 싱글톤 패턴으로 모든 씬에서 GamaManager에 쉽게 접근할 수 있도록 함.

    public Text characterNameText; //캐릭터 이름을 출력할 텍스트 UI
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

    //캐릭터 이름을 출력하는 함수
    public void SetCharacterName(string characterName)
    {
        if (characterNameText != null)
        {
            characterNameText.text = characterName; //해당 UI에 캐릭터 이름 출력
            Debug.Log("선택된 캐릭터: " + characterName);
        }
    }


}
