using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int hp = 100;
    public int att = 20;
    public int def = 10;
    void Start()
    {

    }
    
    void Update()
    {
        if (hp <= 0)
        {
            Debug.Log(gameObject.name + "�� ����Ͽ����ϴ�.");

        }
    }
}