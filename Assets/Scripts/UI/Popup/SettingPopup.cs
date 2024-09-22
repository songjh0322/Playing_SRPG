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
            // 팝업이 이미 떠 있으면 팝업을 닫음
            Destroy(currentPopup);
            currentPopup = null;
        }
        else
        {
            // 팝업이 없으면 팝업을 생성
            currentPopup = Instantiate(settingsPopupPrefab);

            //Popup popupScript = currentPopup.GetComponent<Popup>();
            //if (popupScript != null)
            //{
            //    popupScript.SetPopupReference(currentPopup);
            //}
        }
    }
}