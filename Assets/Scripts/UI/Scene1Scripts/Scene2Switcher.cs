using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
public class Scene2Switcher : MonoBehaviour
{
    public void MoveToScene2()
    {
        GameObject characterSelectionManager = GameObject.Find("@CharacterSelectionManager");

        if (characterSelectionManager != null)
            Destroy(characterSelectionManager);

        SceneManager.LoadScene("Scene2");
    }

}
