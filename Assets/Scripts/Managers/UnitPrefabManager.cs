using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPrefabManager : MonoBehaviour
{
    public List<GameObject> unitPrefabs;

    public static UnitPrefabManager Instance { get; private set; }

    private void Awake()
    {
        Debug.Log("UnitPrefabManager 생성됨");

        if (Instance == null)
        {
            Instance = this;
        }
    }

    public GameObject GetUnitPrefab(int unitCode)
    {
        return unitPrefabs[unitCode];
    }
}
