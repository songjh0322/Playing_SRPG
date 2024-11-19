using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using static TileEnums;

public class TileInfo : MonoBehaviour
{
    // ���� ������
    public int x, y;
    public Unit unit;
    public TileType tileType;
    public InitialDeployment initialDeployment;

    // �ð��� ǥ�ÿ�
    public Vector3 worldXY;
    public GameObject unitPrefab;

    private void Start()
    {
        unit = null;
        worldXY = transform.position;
    }

    private void OnMouseDown()
    {
        // �ش� Ÿ�� ���� UI�� ������ �ƹ��͵� ���� ����
        if (IsPointerOverUIObject())
            return;

        // ����׿�
        Debug.Log($"({x},{y}) Ÿ�� Ŭ����");
        if (unit != null)
            Debug.Log($"���� {unit.basicStats.unitName}��(��) ��ġ�ϰ� �ֽ��ϴ�.");

        // ��ġ ��, �� ���� ����
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

    // (GPT) Ÿ�� ���� UI�� �ִ��� Ȯ���ϴ� �Լ�
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        // ��� ��Ͽ� UI ��Ұ� ������ true ��ȯ
        return results.Count > 0;
    }

    /*public Vector3 GetCenterWorldPosition()
    {
        return transform.position;
    }*/
}
