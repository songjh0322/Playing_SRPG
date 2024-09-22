using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPopup : MonoBehaviour
{
    public GameObject settingsPopupPrefab;
    private GameObject currentPopup;

    public void OnSettingsButtonClicked()
    {
        if (currentPopup != null)
        {
            // �˾��� �̹� �� ������ �˾��� ����
            Destroy(currentPopup);
            currentPopup = null;
        }
        else
        {
            // �˾��� ������ �˾��� ����
            currentPopup = Instantiate(settingsPopupPrefab);

            //Popup popupScript = currentPopup.GetComponent<Popup>();
            //if (popupScript != null)
            //{
            //    popupScript.SetPopupReference(currentPopup);
            //}
        }
    }
}