using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace GameplayUI
{
    public class GameplayUiController : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private FirstPersonController m_PersonController;
        [SerializeField] private MessageUI m_MessageUI;

        [SerializeField] private GameObject m_OverlayGameObject;
        [SerializeField] private GameObject m_SendButtonController;
    
        [SerializeField] private QrPopup m_QrPopup;

        public void RequestWritableMessageUi(string originalContent, Action<string> onSubmitButtonClicked)
        {
            SetGameplayUi(false);
            m_MessageUI.InitAsWritableNote(originalContent, 
                (password) =>
                {
                    onSubmitButtonClicked.Invoke(password);
                    m_MessageUI.gameObject.SetActive(false);
                    SetGameplayUi(true);
                }, () =>
                {
                    m_MessageUI.gameObject.SetActive(false);
                    SetGameplayUi(true);
                });
            m_MessageUI.gameObject.SetActive(true);
        }
        
        public void RequestReadableMessageUi(string originalContent, Action<string> onSubmitButtonClicked)
        {
            SetGameplayUi(false);
            m_MessageUI.InitAsReadableNote(originalContent, 
                (password) =>
                {
                    onSubmitButtonClicked.Invoke(password);
                    m_MessageUI.gameObject.SetActive(false);
                    SetGameplayUi(true);
                }, () =>
                {
                    m_MessageUI.gameObject.SetActive(false);
                    SetGameplayUi(true);
                });
            m_MessageUI.gameObject.SetActive(true);
        }

        public void OpenQrPopup()
        {
            m_QrPopup.gameObject.SetActive(true);

            SetGameplayUi(false);
        }

        public void CloseQrPopup()
        {
            m_QrPopup.gameObject.SetActive(false);

            SetGameplayUi(true);
        }

        private void SetGameplayUi(bool b)
        {
            m_OverlayGameObject.SetActive(b);
            m_PersonController.enabled = b;
        }
    }
}
