using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

// ���� �Ŵ����� ��� �Ŵ����� �����ϴ� �Ŵ��� (�÷��̾��� ���� ���� ����)
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerFaction playerFaction;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            this.AddComponent<UnitManager>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //GameObject unitManagerObject = new GameObject("@UnitManager");
        //unitManagerObject.AddComponent<UnitManager>();

        // �ʼ� ��� (�ʿ��� ���� ������, ������ �ҷ�����)
        UnitManager.Instance.LoadBasicStatsFromJSON();
        UnitManager.Instance.LoadAllUnits();
    }

    // ������
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            foreach (Unit unit in UnitManager.Instance.player1Units)
                Debug.Log($"{unit.basicStats.unitName}");
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            foreach (Unit unit in UnitManager.Instance.player2Units)
                Debug.Log($"{unit.basicStats.unitName}");
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(CharacterSelectionManager.Instance.selectedCharacters.Count);
            Debug.Log(CharacterSelectionManager.Instance.selectedCharacters[0]);
        }
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

public enum PlayerFaction
{
    Guwol,
    Seo,
}
