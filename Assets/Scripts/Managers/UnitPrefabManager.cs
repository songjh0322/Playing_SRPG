using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPrefabManager : MonoBehaviour
{
    public List<GameObject> allUnitPrefabs; // 원본 프리팹

    public static UnitPrefabManager Instance { get; private set; }

    private void Awake()
    {
        // Debug.Log("UnitPrefabManager 생성됨");

        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        allUnitPrefabs = new List<GameObject>();

        // 여기에 유닛의 코드순으로 프리팹 넣기
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/TempUnitPrefab"));   // unitCode는 1부터 유효함
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/GUARDIAN"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/MAGE"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/PALADIN"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/RANGER"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/ROBIN HOOD"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/THIEF"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/WARRIOR"));
    }

    // 원본 프리팹
    private GameObject GetUnitPrefab(int unitCode)
    {
        return allUnitPrefabs[unitCode];
    }

    public GameObject InstantiateUnitPrefab(int unitCode, float scale)
    {
        GameObject transformedUnitPrefab = Instantiate(allUnitPrefabs[unitCode]);
        transformedUnitPrefab.transform.localScale = new Vector3(scale, scale, scale);

        return transformedUnitPrefab;
    }
}
