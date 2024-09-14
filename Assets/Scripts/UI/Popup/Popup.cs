using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public GameObject popupUI;

    public void OnExitButtonClicked()
    {
        Destroy(transform.parent.gameObject);
    }
}
