using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.U2D;

public class UnitPrefabManager : MonoBehaviour
{
    public static UnitPrefabManager Instance { get; private set; }

    public List<GameObject> allUnitPrefabs;     // 원본 프리팹
    public List<AnimatorController> allIdleAnimControllers; // 애니메이션 컨트롤러 리스트

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
        allIdleAnimControllers = new List<AnimatorController>();

        // 여기에 유닛의 코드순으로 프리팹 넣기
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/TempUnitPrefab"));   // unitCode는 1부터 유효함
        
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/GUARDIAN"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/MAGE"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/PALADIN"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/RANGER"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/ROBIN HOOD"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/THIEF"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/WARRIOR"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Hammering"));  // 여기까지 unitCode = 8

        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Venomclaw"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Blade"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Rotfang"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/DevX"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Sharpshot"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Warhound"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Sting"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Xshade"));  // 여기까지 unitCode = 16
        //print(allUnitPrefabs.Count);

        // 스프라이트 추가

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

        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Guardian/Guardian_Idle")); // 0번은 무시

        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Guardian/Guardian_Idle"));
        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Mage/Mage_Idle"));
        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Paladin/Paladin_Idle"));
        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Ranger/Ranger_Idle"));
        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/RobinHood/RobinHood_Idle"));
        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Thief/Thief_Idle"));
        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Warrior/Warrior_Idle"));
        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Hammering/Hammering_Idle"));

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
