using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Scene3Switcher : MonoBehaviour
{
     public void MoveToScene3()
    {       
        SceneManager.LoadScene("Scene3");

        GameManager gameManager = GameManager.Instance;
        gameManager.player1Camp = Player1Camp.Guwol;
    }
}
