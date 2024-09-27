using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Popup : MonoBehaviour
{
    public GameObject popupUI;
    public TMP_Text characterName;

    public void OnExitButtonClicked()
    {
        Destroy(transform.parent.gameObject);

    }
}
