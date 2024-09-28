using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public enum State
    {
        start,
        playerTurn,
        enemyTurn,
        win
    }

    public static State state;
    public bool isLive; // 적 생존 여부 

    void Awake()
    {
        state = State.start; // 전투 시작알림 
        BattleStart();
    }

    void BattleStart()
    {
        // 전투 시작시 캐릭터 등장 애니매이션 등의 효과

        // 플레이어나 적에게 턴 넘기기
        state = State.playerTurn;
    }

    // 공격 버튼
    public void PlayerAttackButton()
    {
        // 버튼이 계속 눌리는 것을 방지하기 위함
        if (state != State.playerTurn)
        {
            return;
        }
        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("player attack");
        // 공격 스킬, 데미지 등 코드 작성

        // 적이 죽으면 전투 종료 
        if (!isLive)
        {
            state = State.win;
            EndBattle();
        }
        else
        {
            state = State.enemyTurn;
            StartCoroutine(EnemyTurn());
        }
    }

    void EndBattle()
    {
        Debug.Log("Battle End");
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);

        Debug.Log("player attack");
        // 적 공격 코드

        // 적 공격이 끝나면 플레이어에게 턴 넘기기 
        state = State.playerTurn;
    }
}
