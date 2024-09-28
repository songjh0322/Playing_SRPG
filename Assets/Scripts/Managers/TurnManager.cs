using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnPlayer
{
    Player1,    // �÷����ϴ� ���
    Player2     // ����� AI
}

public class TurnManager
{
    public static TurnManager Instance { get; private set; }

    public int turnCount;           // ���� ���� �� (1���� ����)
    public TurnPlayer turnPlayer;   // ���� �� �÷��̾�

    // �̱��� �ν��Ͻ� ����
    private TurnManager()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        turnCount = 1;
    }

    // �̱��� �ν��Ͻ��� ��ȯ
    public static TurnManager GetInstance()
    {
        if (Instance == null)
        {
            Instance = new TurnManager();
        }
        return Instance;
    }

    // ���� �� �÷��̾ Ư�� �÷��̾�� ����
    public void SetFirstTurnPlayer(TurnPlayer turnPlayer)
    {
        this.turnPlayer = turnPlayer;
        Debug.Log($"{turnPlayer}�� ���Դϴ�.");
    }

    // �������� Player1 �Ǵ� Player2�� ����
    public void SetFirstTurnPlayerRandom()
    {
        turnPlayer = (Random.Range(0, 2) == 0) ? TurnPlayer.Player1 : TurnPlayer.Player2;
        Debug.Log($"{turnPlayer}�� ���Դϴ�.");
    }

    // ��� : �� ������ �̵��� �Ϸ� Ȥ�� ��ų ������ ������ �� ���������� ȣ��
    // ��� : �� ���� 1 ������Ű�� �� �÷��̾ ��ȯ
    public void TurnEnd()
    {
        turnCount++;
        turnPlayer = (turnPlayer == TurnPlayer.Player1) ? TurnPlayer.Player2 : TurnPlayer.Player1;
    }
}
