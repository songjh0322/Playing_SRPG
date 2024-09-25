using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �̿����� ����
// �Ը� Ŀ���� ���� ����� ���ɼ� ����
public class PrefabManager : MonoBehaviour
{
    // UI ����
    // ���� : public GameObject buttonPrefab;

    // �ΰ��� ��� (���̳� Ÿ��, ĳ���� ��)
    public GameObject tilePrefab;
    // public GameObject characterPrefab;

    private void Awake()
    {
        LoadPrefabs();
    }

    // ���� ��ο��� ������ �ε�
    private void LoadPrefabs()
    {
        // buttonPrefab = Resources.Load<GameObject>("Prefabs/ButtonPrefab");
        tilePrefab = Resources.Load<GameObject>("Prefabs/Tiles/basic_tile");
        // characterPrefab = Resources.Load<GameObject>("Prefabs/CharacterPrefab");
    }
}
