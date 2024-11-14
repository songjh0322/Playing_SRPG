using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.U2D;

public class UnitPrefabManager : MonoBehaviour
{
    public static UnitPrefabManager Instance { get; private set; }

    public List<GameObject> allUnitPrefabs;     // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½
    public List<AnimatorController> allIdleAnimControllers; // ï¿½Ö´Ï¸ï¿½ï¿½Ì¼ï¿½ ï¿½ï¿½Æ®ï¿½Ñ·ï¿½ ï¿½ï¿½ï¿½ï¿½Æ®

    private void Awake()
    {
        // Debug.Log("UnitPrefabManager ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½");

        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        allUnitPrefabs = new List<GameObject>();
        allIdleAnimControllers = new List<AnimatorController>();

        // ï¿½ï¿½ï¿½â¿¡ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Úµï¿½ï¿½ï¿½ï¿½ï¿½ï¿? ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Ö±ï¿½
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/TempUnitPrefab"));   // unitCodeï¿½ï¿½ 1ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½È¿ï¿½ï¿½
        
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/GUARDIAN"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/MAGE"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/PALADIN"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/RANGER"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/ROBIN HOOD"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/THIEF"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/WARRIOR"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Hammering"));  // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿? unitCode = 8

        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Venomclaw"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Blade"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Rotfang"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/DevX"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Sharpshot"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Warhound"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Sting"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Xshade"));  // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿? unitCode = 16
        //print(allUnitPrefabs.Count);

        // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Æ® ï¿½ß°ï¿½

        //idleSprites.Add(Resources.Load<Sprite>("UnitSprites/SPUM_20241109144742553_IDLE0_0_idle_0_0"));
        //idleSprites.Add(Resources.Load<Sprite>("UnitSprites/Guardian_Idle/SPUM_20241109144742553_IDLE0_0_idle_0_0"));

        // idleSpriteAtlases.Add(Resources.Load<SpriteAtlas>("UnitSprites/Guardian_Idle"));

        // idleSpriteAtlases.Add(Resources.Load<SpriteAtlas>("UnitSprites/Guardian_Idle"));
        // idleSpriteAtlases.Add(Resources.Load<SpriteAtlas>("UnitSprites/Mage_Idle"));
        // idleSpriteAtlases.Add(Resources.Load<SpriteAtlas>("UnitSprites/Paladin_Idle"));
        // idleSpriteAtlases.Add(Resources.Load<SpriteAtlas>("UnitSprites/Ranger_Idle"));
        // idleSpriteAtlases.Add(Resources.Load<SpriteAtlas>("UnitSprites/Robin Hood_Idle"));
        // idleSpriteAtlases.Add(Resources.Load<SpriteAtlas>("UnitSprites/Thief_Idle"));
        // idleSpriteAtlases.Add(Resources.Load<SpriteAtlas>("UnitSprites/Warrior_Idle"));
        // idleSpriteAtlases.Add(Resources.Load<SpriteAtlas>("UnitSprites/Hammering_Idle"));
        // print(idleSprites.Count);

        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Guardian/Guardian_Idle")); // 0¹øÂ°´Â ÀÇ¹Ì¾øÀ½

        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Guardian/Guardian_Idle"));
        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Mage/Mage_Idle"));
        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Paladin/Paladin_Idle"));
        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Ranger/Ranger_Idle"));
        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Robin Hood/RobinHood_Idle"));
        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Thief/Thief_Idle"));
        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Warrior/Warrior_Idle"));
        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Hammering/Hammering_Idle"));

        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Venomclaw/Venomclaw_Idle"));
        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Blade/Blade_Idle"));
        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Rotfang/Rotfang_Idle"));
        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/DevX/DevX_Idle"));
        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Sharpshot/Sharpshot_Idle"));
        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Warhound/Warhound_Idle"));
        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Sting/Sting_Idle"));
        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Xshade/Xshade_Idle"));

    }

/*    // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½
    private GameObject GetUnitPrefab(int unitCode)
    {
        return allUnitPrefabs[unitCode];
    }*/

    // ï¿½ï¿½ï¿½Î¿ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½(ï¿½ï¿½ï¿½ï¿½)
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
