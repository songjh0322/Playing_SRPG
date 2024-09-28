using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnPlayer
{
    Player1,    // 플레이하는 사람
    Player2     // 현재는 AI
}

public class TurnManager
{
    public static TurnManager Instance { get; private set; }

    public int turnCount;           // 현재 턴의 수 (1부터 시작)
    public TurnPlayer turnPlayer;   // 현재 턴 플레이어

    // 싱글톤 인스턴스 설정
    private TurnManager()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        turnCount = 1;
    }

    // 싱글톤 인스턴스를 반환
    public static TurnManager GetInstance()
    {
        if (Instance == null)
        {
            Instance = new TurnManager();
        }
        return Instance;
    }

    // 시작 턴 플레이어를 특정 플레이어로 지정
    public void SetFirstTurnPlayer(TurnPlayer turnPlayer)
    {
        this.turnPlayer = turnPlayer;
        Debug.Log($"{turnPlayer}의 턴입니다.");
    }

    // 무작위로 Player1 또는 Player2를 선택
    public void SetFirstTurnPlayerRandom()
    {
        turnPlayer = (Random.Range(0, 2) == 0) ? TurnPlayer.Player1 : TurnPlayer.Player2;
        Debug.Log($"{turnPlayer}의 턴입니다.");
    }

    // 사용 : 한 유닛이 이동을 완료 혹은 스킬 시전이 끝났을 때 마지막으로 호출
    // 기능 : 턴 수를 1 증가시키고 턴 플레이어를 전환
    public void TurnEnd()
    {
        turnCount++;
        turnPlayer = (turnPlayer == TurnPlayer.Player1) ? TurnPlayer.Player2 : TurnPlayer.Player1;
    }
}
