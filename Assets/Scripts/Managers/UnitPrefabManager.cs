using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.U2D;

public class UnitPrefabManager : MonoBehaviour
{
    public static UnitPrefabManager Instance { get; private set; }

    public List<GameObject> allUnitPrefabs;     // ���� ������
    public List<AnimatorController> allIdleAnimControllers; // �ִϸ��̼� ��Ʈ�ѷ� ����Ʈ

    private void Awake()
    {
        // Debug.Log("UnitPrefabManager ������");

        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        allUnitPrefabs = new List<GameObject>();
        allIdleAnimControllers = new List<AnimatorController>();

        // ���⿡ ������ �ڵ������ ������ �ֱ�
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/TempUnitPrefab"));   // unitCode�� 1���� ��ȿ��
        
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/GUARDIAN"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/MAGE"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/PALADIN"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/RANGER"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/ROBIN HOOD"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/THIEF"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/WARRIOR"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Hammering"));  // ������� unitCode = 8

        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Venomclaw"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Blade"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Rotfang"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/DevX"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Sharpshot"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Warhound"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Sting"));
        allUnitPrefabs.Add(Resources.Load<GameObject>("UnitPrefabs/Xshade"));  // ������� unitCode = 16
        //print(allUnitPrefabs.Count);

        // ��������Ʈ �߰�

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

        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Guardian/Guardian_Idle")); // 0���� ����

        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Guardian/Guardian_Idle"));
        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Mage/Mage_Idle"));
        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Paladin/Paladin_Idle"));
        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Ranger/Ranger_Idle"));
        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Robin Hood/RobinHood_Idle"));
        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Thief/Thief_Idle"));
        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Warrior/Warrior_Idle"));
        allIdleAnimControllers.Add(Resources.Load<AnimatorController>("Animations/Hammering/Hammering_Idle"));

    }

/*    // ���� ������
    private GameObject GetUnitPrefab(int unitCode)
    {
        return allUnitPrefabs[unitCode];
    }*/

    // ���ο� ������(����)
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
