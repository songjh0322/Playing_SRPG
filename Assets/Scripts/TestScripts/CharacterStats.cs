using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStats : MonoBehaviour
{
    public string characterName;
    public int health;
    public int att;
    public int def;
    public int moveRange;
    public int skillRange;

    public CharacterStats(string name, int health, int att, int def,int moveRange,int skillRange)
    {
        this.characterName = name;
        this.health = health;
        this.att = att;
        this.def = def;
        this.moveRange = moveRange;
        this.skillRange = skillRange;
    }
}
