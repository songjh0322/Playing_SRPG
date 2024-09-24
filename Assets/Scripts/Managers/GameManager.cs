using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject tilePrefab; // Inspector���� �Ҵ��� ������
    private MapManager mapManager;

    void Start()
    {
        // MapManager �ʱ�ȭ
        mapManager = new MapManager(tilePrefab);

        // �� ����
        mapManager.CreateMap();
    }

    private void Update()
    {
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
    }
}
