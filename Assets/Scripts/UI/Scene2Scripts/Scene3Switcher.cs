using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Scene3Switcher : MonoBehaviour
{
     public void MoveToScene3()
    {       
        SceneManager.LoadScene("Scene3");

        GameManager.Instance.playerFaction = PlayerFaction.Guwol;
    }
}
