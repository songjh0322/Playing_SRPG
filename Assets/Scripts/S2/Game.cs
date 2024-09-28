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
    public bool isLive; // �� ���� ���� 

    void Awake()
    {
        state = State.start; // ���� ���۾˸� 
        BattleStart();
    }

    void BattleStart()
    {
        // ���� ���۽� ĳ���� ���� �ִϸ��̼� ���� ȿ��

        // �÷��̾ ������ �� �ѱ��
        state = State.playerTurn;
    }

    // ���� ��ư
    public void PlayerAttackButton()
    {
        // ��ư�� ��� ������ ���� �����ϱ� ����
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
        // ���� ��ų, ������ �� �ڵ� �ۼ�

        // ���� ������ ���� ���� 
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
        // �� ���� �ڵ�

        // �� ������ ������ �÷��̾�� �� �ѱ�� 
        state = State.playerTurn;
    }
}
