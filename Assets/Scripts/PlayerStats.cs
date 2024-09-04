using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private int hp = 100;
    [SerializeField]
    private int att = 20;
    [SerializeField]
    private int def = 10;

    public int CurrentHP { get { return hp; } }
    public int AttackPower { get { return att; } }
    public int Defense { get { return def; } }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp < 0)
            hp = 0;
    }
}