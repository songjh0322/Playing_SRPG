using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
<<<<<<< HEAD
    public GameObject tilePrefab; // Inspector���� �Ҵ��� ������
    private MapManager mapManager;

    void Start()
    {
        // MapManager �ʱ�ȭ
        mapManager = new MapManager(tilePrefab);

        // �� ����
        mapManager.CreateMap();
=======
    private void Awake()
    {
        // �ʰ� UI �ʱ�ȭ
        MapManager.Instance.InitializeMap();
        UIManager.Instance.InitializeUI();
>>>>>>> 9e268010859d94b39b3f0e05584f489f7eddac52
    }

    private void Start()
    {
        // ������ ���۵� �� �ʿ��� �߰� �۾� ����
        Debug.Log("GameManager: ���� ����");
    }

    // Update �Լ��� �ʿ��� �� ���
    private void Update()
    {
<<<<<<< HEAD
        /*if (Input.GetMouseButtonDown(0)) // ��Ŭ�� ����
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null) // Ÿ���� Ŭ������ ���
            {
                Tile clickedTile = hit.collider.GetComponent<Tile>();
                if (clickedTile != null)
                {
                    // Ÿ���� ��� ���� ���
                    Debug.Log($"Clicked Tile - Row: {clickedTile.row}, Col: {clickedTile.col}");
                }
            }
        }*/
=======
        // ���� ���� ������Ʈ
>>>>>>> 9e268010859d94b39b3f0e05584f489f7eddac52
    }
}

