using System.Collections;
using System.Collections.Generic;
using GameplayUI;
using UnityEngine;

public class GameplayUiController : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private MessageUI m_MessageUI;

    [SerializeField] private GameObject m_OverlayGameObject;
    [SerializeField] private GameObject m_SendButtonController;
    
    [SerializeField] private QrPopup m_QrPopup;

    public void RequestMessageUi()
    {
        
    }

    public void OpenQrPopup()
    {
        m_QrPopup.gameObject.SetActive(true);
        
        m_OverlayGameObject.SetActive(false);
    }

    public void CloseQrPopup()
    {
        m_QrPopup.gameObject.SetActive(false);
        
        m_OverlayGameObject.SetActive(true);
    }
}
