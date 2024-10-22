using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Scene4Switcher : MonoBehaviour
{
     public void MoveToScene4()
    {       
        SceneManager.LoadScene("Scene4");

        GameManager.Instance.playerFaction = Faction.Seo;
    }
}
