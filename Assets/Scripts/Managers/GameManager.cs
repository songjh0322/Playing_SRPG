using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    protected UIManager uiManager = new UIManager();
    protected TurnManager turnManager = new TurnManager();

    // MapManager�� ���������� �ν��Ͻ�ȭ�� �ڽ� Ŭ�������� ����
    protected MapManager mapManager;

    void Start()
    {
        // ���� �����ϰ� Scene�� ǥ��
        mapManager = new MapManager(); // MapManager �ν��Ͻ�ȭ
        mapManager.LoadPrefabs(); // ������ �ε�
        mapManager.CreateMap();
    }

    private void Update()
    {
        
    }
}
