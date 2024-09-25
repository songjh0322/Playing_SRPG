using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BackToScene1 : MonoBehaviour
{
     public void BackToScene()
    {       
        SceneManager.LoadScene("Scene1");
    }

}
