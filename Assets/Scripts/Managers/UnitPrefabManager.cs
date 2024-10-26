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
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/AI1"));  // 여기까지 unitCode = 8

        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/AI3"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/AI4"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/AI5"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/AI6"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/AI7"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/AI8"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/AI9"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/AI10"));  // 여기까지 unitCode = 16
    }

/*    // 원본 프리팹
    private GameObject GetUnitPrefab(int unitCode)
    {
        return allUnitPrefabs[unitCode];
    }*/

    // 새로운 프리팹(복제)
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
