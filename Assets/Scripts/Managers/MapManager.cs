using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static TileEnums;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    public GameObject tilePrefab;

    private void Awake()
    {
        Debug.Log("MapManager 积己凳");

        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        CreateTestMap();
    }

    // 甘 橇府普 积己侩
    public void CreateTestMap()
    {
        GameObject testMap = new GameObject("TestMap");
        testMap.transform.position = new Vector3(-18, 0, 0);

        for (int j = 0; j < 10; j++)
        {
            for (int i = 0; i < 10; i++)
            {
                Vector3 position = new Vector3((i + j) * 2, j - i, 0);
                GameObject createdTile = Instantiate(tilePrefab, position, Quaternion.identity);
                createdTile.transform.SetParent(testMap.transform, false);
                TileInfo tileInfo = createdTile.GetComponent<TileInfo>();
                tileInfo.Initialize(i, j, TileType.Normal, InitialDeployment.Player1);
            }
        }
    }

}
