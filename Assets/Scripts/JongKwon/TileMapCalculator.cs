using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapCalculator : MonoBehaviour
{
    public Tilemap tilemap;

    void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null)
            {
                Vector3Int tilePos = tilemap.WorldToCell(hit.point);
                Vector3 tileCenterPos = tilemap.GetCellCenterWorld(tilePos);

                Debug.Log("Tile Center Position: " + tileCenterPos);
            }
        }
    }
}
