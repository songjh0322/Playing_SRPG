using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameManager1 : MonoBehaviour
{
    public static InGameManager1 Instance { get; private set; }

    // 게임 상태 관련 변수
    public bool isPlayerTurn;
    public int playerDeathCount;
    public int aiDeathCount;

    public GameObject turnText;
    public GameObject gameResultPopup;
    public GameObject gameResultText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        ResetGame();
        PlayerAgent.Instance.RequestDecision();
    }

    private void ResetGame()
    {
        isPlayerTurn = true;
        playerDeathCount = 0;
        aiDeathCount = 0;

        turnText.SetActive(true);
        gameResultPopup.SetActive(false);
    }

    public void EndTurn()
    {
        Debug.Log("EndTurn");
        // 게임 종료 조건 확인
        if (playerDeathCount >= 5 || aiDeathCount >= 5)
        {
            GameEnd();
            return; // 게임 종료 시 추가 행동을 막음
        }

        // 턴 전환
        isPlayerTurn = !isPlayerTurn;

        if (isPlayerTurn)
        {
            PlayerAgent.Instance.RequestDecision(); // 플레이어의 행동 요청
        }
        else
        {
            EnemyAgent.Instance.RequestDecision(); // AI의 행동 요청
        }
    }

    public void GameEnd()
    {
        turnText.SetActive(false);
        gameResultPopup.SetActive(true);

        if (aiDeathCount == 5)
        {
            gameResultText.GetComponent<Text>().text = "YOU WIN !";
            PlayerAgent.Instance.SetReward(1.0f);
            EnemyAgent.Instance.SetReward(-1.0f);
        }
        else
        {
            gameResultText.GetComponent<Text>().text = "YOU LOSE !";
            PlayerAgent.Instance.SetReward(-1.0f);
            EnemyAgent.Instance.SetReward(1.0f);
        }
        ResetGame();
        
        Debug.Log("End Episode");
        PlayerAgent.Instance.EndEpisode();
        EnemyAgent.Instance.EndEpisode();
    }
}
