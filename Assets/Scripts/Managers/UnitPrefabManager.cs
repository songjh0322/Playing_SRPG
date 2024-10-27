using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPrefabManager : MonoBehaviour
{
    public static UnitPrefabManager Instance { get; private set; }

    public List<GameObject> allUnitPrefabs;     // ���� ������

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
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Hammering"));  // ������� unitCode = 8

        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Venomclaw"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Blade"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Rotfang"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/DevX"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Sharpshot"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Warhound"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Sting"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Xshade"));  // ������� unitCode = 16
        print(allUnitPrefabs.Count);
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
