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
        GameManager.Instance.gameState = GameState.InGame;

        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        isPlayerTurn = true;
        playerDeathCount = 0;
        aiDeathCount = 0;

        turnText.SetActive(true);
        StartCoroutine(StartTrainingLoop());
    }

    private IEnumerator StartTrainingLoop()
    {
        while (true)
        {
            if (playerDeathCount == 5 || aiDeathCount == 5)
            {
                GameEnd();
                yield break; // 게임 종료 시 루프 종료
            }

            if (isPlayerTurn)
            {
                turnText.GetComponent<TMP_Text>().text = "Player's Turn";
                PlayerAgent.Instance.RequestDecision();
            }
            else
            {
                turnText.GetComponent<TMP_Text>().text = "AI's Turn";
                EnemyAgent.Instance.RequestDecision();
            }

            yield return new WaitForSeconds(2f); // 턴 간 대기
            isPlayerTurn = !isPlayerTurn;
        }
    }

    public void GameEnd()
    {
        turnText.SetActive(false);
        gameResultPopup.SetActive(true);

        if (aiDeathCount == 5)
            gameResultText.GetComponent<Text>().text = "YOU WIN !";
        else
            gameResultText.GetComponent<Text>().text = "YOU LOSE !";
    }
}
