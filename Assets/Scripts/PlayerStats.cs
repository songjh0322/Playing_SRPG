using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    int hp = 100;
    [SerializeField]
    int att = 20;
    [SerializeField]
    int def = 10;

    void Start()
    {

    }
    
    void Update()
    {
        if (hp <= 0)
        {
            Debug.Log(gameObject.name + "이 사망하였습니다.");

        }
    }
}
