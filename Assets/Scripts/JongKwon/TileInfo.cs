using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using static TileEnums;

public class TileInfo : MonoBehaviour
{
    // 게임 로직용
    public int x, y;
    public Unit unit;
    public TileType tileType;
    public InitialDeployment initialDeployment;

    // 시각적 표시용
    public Vector3 worldXY;
    public GameObject unitPrefab;

    private void Start()
    {
        unit = null;
        worldXY = transform.position;
    }

    private void OnMouseDown()
    {
        if (IsPointerOverUIObject())
            return;

        // 디버그용
        Debug.Log($"({x},{y}) 타일 클릭됨");
        if (unit != null)
            Debug.Log($"현재 {unit.basicStats.unitName}이(가) 위치하고 있습니다.");
        if (unitPrefab != null)
            Debug.Log("unitPrefab;이 무언가로 채워져 있습니다.");

        // 배치 중, 턴 시작 이후
        if (GameManager.Instance.gameState == GameState.InitialDeployment)
            InitialDeployManager.Instance.OnTileClicked(this);
        else if (GameManager.Instance.gameState == GameState.InGame)
            InGameManager.Instance.OnTileClicked(this);
    }

    public void Initialize(int x, int y, TileType tileType, InitialDeployment initialDeployment)
    {
        this.x = x;
        this.y = y;
        this.tileType = tileType;
        this.initialDeployment = initialDeployment;
    }

    // (GPT)
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        // 결과 목록에 UI 요소가 있으면 true 반환
        return results.Count > 0;
    }

    /*public Vector3 GetCenterWorldPosition()
    {
        return transform.position;
    }*/
}
