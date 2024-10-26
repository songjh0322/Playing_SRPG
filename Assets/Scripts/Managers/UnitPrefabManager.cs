using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPrefabManager : MonoBehaviour
{
    public List<GameObject> allUnitPrefabs; // ���� ������

    public static UnitPrefabManager Instance { get; private set; }

    private void Awake()
    {
        // Debug.Log("UnitPrefabManager ������");

        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        allUnitPrefabs = new List<GameObject>();

        // ���⿡ ������ �ڵ������ ������ �ֱ�
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/TempUnitPrefab"));   // unitCode�� 1���� ��ȿ��
        
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/GUARDIAN"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/MAGE"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/PALADIN"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/RANGER"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/ROBIN HOOD"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/THIEF"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/WARRIOR"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/AI1"));  // ������� unitCode = 8

        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/AI3"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/AI4"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/AI5"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/AI6"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/AI7"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/AI8"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/AI9"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/AI10"));  // ������� unitCode = 16
    }

/*    // ���� ������
    private GameObject GetUnitPrefab(int unitCode)
    {
        return allUnitPrefabs[unitCode];
    }*/

    // ���ο� ������(����)
    public GameObject InstantiateUnitPrefab(int unitCode, float scale, bool reverse)
    {
        GameObject transformedUnitPrefab = Instantiate(allUnitPrefabs[unitCode]);
        if (reverse)
            transformedUnitPrefab.transform.localScale = new Vector3(-scale, scale, scale);
        else
            transformedUnitPrefab.transform.localScale = new Vector3(scale, scale, scale);

        return transformedUnitPrefab;
    }
}
