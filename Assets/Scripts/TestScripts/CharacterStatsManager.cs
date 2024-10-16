using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class CharacterStats
{
    public string characterName;
    public int health;
    public int attackPower;
    public int defense;
    public int moveRange;
    public int skillRange;

    public CharacterStats(string name, int hp, int att, int def, int move, int skill)
    {
        characterName = name;
        health = hp;
        attackPower = att;
        defense = def;
        moveRange = move;
        skillRange = skill;

    }
}
public class CharacterStatsManager : MonoBehaviour
{
    public static CharacterStatsManager instance;
    
    private Dictionary<string, CharacterStats> characterStatsDatabase = new Dictionary<string, CharacterStats>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeStats();
    }
    private void InitializeStats()
    {
        characterStatsDatabase.Add("Ranger", new CharacterStats("Ranger", 100, 20, 10, 2, 3));
        characterStatsDatabase.Add("Mage", new CharacterStats("Mage", 80, 25, 10, 2, 3));
        characterStatsDatabase.Add("Robin Hood", new CharacterStats("RoBin Hood", 150, 30, 20, 1, 2));
        characterStatsDatabase.Add("Paladin", new CharacterStats("Paladin", 200, 40, 30, 1, 2));
        characterStatsDatabase.Add("Warrior", new CharacterStats("Warrior", 200, 40, 30, 1, 2));
        characterStatsDatabase.Add("Thief", new CharacterStats("Thief", 100, 50, 0, 3, 3));
        characterStatsDatabase.Add("Guardian", new CharacterStats("Guardian", 250, 30, 30, 1, 2));
    }
    public CharacterStats GetCharacterStats(string characterName)
    {
        if (characterStatsDatabase.ContainsKey(characterName))
        {
            return characterStatsDatabase[characterName];
        }
        else
        {
            Debug.Log("해당 캐릭터의 스탯이 없습니다: " + characterName);
            return null;
        }
    }
}
