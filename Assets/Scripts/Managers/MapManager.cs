using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static TileEnums;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    // ÇÁ¸®ÆÕ °ü¸® (Inspector¿¡¼­ ÇÒ´ç)
    public GameObject tilePrefab;
    public Sprite middleZone;

    // ÇöÀç ¸Ê Á¤º¸ °ü¸®
    public List<TileInfo> tileInfos;

    private void Awake()
    {
        Debug.Log("MapManager »ý¼ºµÊ");

        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        tileInfos = new List<TileInfo>();
        CreateTestMap();
    }

    // ¸Ê ÇÁ¸®ÆÕ »ý¼º¿ë (10 by 12)
    public void CreateTestMap()
    {
        GameObject testMap = new GameObject("TestMap");
        testMap.transform.position = new Vector3(-20, 0, 0);

        for (int y = 0; y < 12; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                Vector3 position = new Vector3((x + y) * 2, y - x, 0);
                GameObject createdTile = Instantiate(tilePrefab, position, Quaternion.identity);
                createdTile.transform.SetParent(testMap.transform, false);
                TileInfo tileInfo = createdTile.GetComponent<TileInfo>();
                if (y < 5)
                    tileInfo.Initialize(x, y, TileType.Normal, InitialDeployment.Player1);
                else if (y < 7)
                {
                    tileInfo.Initialize(x, y, TileType.Normal, InitialDeployment.None);
                    SpriteRenderer sr = tileInfo.GetComponent<SpriteRenderer>();
                    sr.sprite = middleZone;
                }
                else
                    tileInfo.Initialize(x, y, TileType.Normal, InitialDeployment.Player2);
            }
        }
    }

}
