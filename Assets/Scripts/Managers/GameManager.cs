using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ���� �Ŵ����� ��� �Ŵ����� �����ϴ� �Ŵ�����, �����ϰ� MonoBehaviour�� ��ӹ޴� �Ŵ���
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    protected UIManager uiManager;
    protected TurnManager turnManager;
    protected UnitManager unitManager;
    protected MapManager mapManager;
    protected DeployManager deployManager;

    public Player1Camp player1Camp;     // Player1�� ������ ����
    public Player1Camp player2Camp;     // Player2�� ������ ����
    

    private void Awake()
    {
        // �̱��� �ν��Ͻ� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);      // Scene�� ����Ǿ �Ŵ������� ������
        }
        else
        {
            Destroy(gameObject);
        }

        unitManager = new UnitManager();

        // ���� ���α׷��� ����Ǹ� ��� �Ŵ����� �ҷ���
        uiManager = UIManager.Instance;
        turnManager = TurnManager.Instance;
        //unitManager = UnitManager.Instance;
        mapManager = MapManager.Instance;
        deployManager = DeployManager.Instance;
    }

    void Start()
    {

        print("hello");
        // �ʼ� ��� (�ʿ��� ���� ������, ������ �ҷ�����)
        //unitManager.LoadBasicStatsFromJSON();
        
        // �ӽ� �Ҵ� (Ư�� ������ �ٷ� �׽�Ʈ�� �ϱ� ����)
        player1Camp = Player1Camp.Guwol;    
        player1Camp = Player1Camp.Seo;

        // !! �Ʒ��� �ڵ���� �Ϸ��� ȣ�� ������ !!

        // ���� - ������ ���۵Ǹ� ������� ȣ�� (���ڴ� UI���� ����, ����� ���Ƿ� ����)
        //unitManager.ConfirmPlayer1Units(new List<string> { "ö��", "����", "����", "����", "ȯ��", "�ޱ���"});
        //unitManager.RandomizePlayer2Units(); // �Ǵ� unitManager.ConfirmPlayer2Units(...)
        //mapManager.CreateMap();

        // ���� ��ġ �ܰ� ���� ��
        // ��ġ �ܰ迡�� �̹� ��ġ�� �Ϸ��� ĳ������ ���, �ش� ĳ������ ��ư�� ��Ȱ��ȭ�ϴ� ���� UI �������� ����
        // �ϳ��� ������ ��ġ�� ������ ȭ�� ������Ʈ�� �����Ƿ� List�� ����
        //deployManager.StartDeploy();

        // [��ġ �Ϸ�] ��ư Ŭ�� ��

    }

    private void Update()
    {
        
    }

/*    // Ÿ�� Ŭ�� �� ȣ��Ǵ� �޼���
    public void OnTileClicked(Tile clickedTile)
    {
        // MapManager�� GetReachableTiles() �Լ��� ȣ���Ͽ� ����ư �Ÿ� 3 �̳��� Ÿ���� ������
        List<Tile> reachableTiles = MapManager.Instance.GetReachableTiles(3, clickedTile);

        // ������ Ÿ�ϵ��� ������ �ʷϻ����� ����
        foreach (Tile tile in reachableTiles)
        {
            tile.ChangeColor(Color.green);
        }
    }*/
}

public enum Player1Camp
{
    Guwol,
    Seo,
}
