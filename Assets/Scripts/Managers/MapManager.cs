using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static TileEnums;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    // ������ ����
    public GameObject tilePrefab;

    // ���� �� ���� ����
    public TileInfo[][] tileInfos;

    private void Awake()
    {
        Debug.Log("MapManager ������");

        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        CreateTestMap();
    }

    // �� ������ ������
    public void CreateTestMap()
    {
        GameObject testMap = new GameObject("TestMap");
        testMap.transform.position = new Vector3(-18, 0, 0);

        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                Vector3 position = new Vector3((x + y) * 2, y - x, 0);
                GameObject createdTile = Instantiate(tilePrefab, position, Quaternion.identity);
                createdTile.transform.SetParent(testMap.transform, false);
                TileInfo tileInfo = createdTile.GetComponent<TileInfo>();
                if (y < 5)
                    tileInfo.Initialize(x, y, TileType.Normal, InitialDeployment.Player1);
                else
                    tileInfo.Initialize(x, y, TileType.Normal, InitialDeployment.Player2);
            }
        }
    }

}
