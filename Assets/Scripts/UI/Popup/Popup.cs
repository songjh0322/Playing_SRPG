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
        if (characterName != null) 
        {
            CharacterSelectionManager selectionManager = gameObject.AddComponent<CharacterSelectionManager>();
            selectionManager.SelectedCharacters.Remove(characterName.text);
            popupUI.SetActive(false);
        }
        else
        {
            Destroy(transform.parent.gameObject);
        }

    }
}
