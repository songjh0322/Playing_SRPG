using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
public class Scene2Switcher : MonoBehaviour
{
    public void MoveToScene2()
    {       
        SceneManager.LoadScene("Scene2");
    }

}
