using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerInitializer : MonoBehaviour
{
    void Awake()
    {
        GameObject existingManager = GameObject.Find("@GameManager");

        if (existingManager == null)
        {
            GameObject managerObject = new GameObject("@GameManager");
            managerObject.AddComponent<GameManager>();
        }
    }
}
