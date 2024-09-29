using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Scene5Switcher : MonoBehaviour
{
    GameManager gameManager;
    UnitManager unitManager;
    CharacterSelectionManager characterSelectionManager;
    
    public void MoveToScene5()
    {
        gameManager = GameManager.Instance;
        unitManager = UnitManager.Instance;
        characterSelectionManager = CharacterSelectionManager.Instance;

        //SceneManager.LoadScene("Scene5");
        // �ΰ��� ȭ������ ������
        SceneManager.LoadScene("InGameScene");
    }
}
