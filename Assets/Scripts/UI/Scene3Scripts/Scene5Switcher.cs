using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Scene5Switcher : MonoBehaviour
{
     public void MoveToScene5()
    {       
        SceneManager.LoadScene("Scene5");
    }
}
