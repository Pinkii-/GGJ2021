﻿using System;
using SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

namespace GameplayUI
{
    public class GameplayUiController : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private FirstPersonController m_PersonController;
        [SerializeField] private CrosshairScript m_Crosshair;

        
        [SerializeField] private MessageUI m_MessageUI;

        [SerializeField] private GameObject m_OverlayGameObject;
        [SerializeField] private GameObject m_SendButton;
        [SerializeField] private FoundMessageCounter m_FoundMessageCounter;
    
        [SerializeField] private QrPopup m_QrPopup;

        [SerializeField] private GameObject m_AllMemoriesPrompt;
        
        [SerializeField] private GameObject m_ConfirmExit;

        public void RequestWritableMessageUi(string originalContent, Action<string> onSubmitButtonClicked)
        {
            SetGameplayUi(false);
            m_MessageUI.gameObject.SetActive(true);
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
        }
        
        public void RequestReadableMessageUi(string originalContent, Action<string> onSubmitButtonClicked)
        {
            SetGameplayUi(false);
            m_MessageUI.gameObject.SetActive(true);
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

        public void OnAmountOfMemoriesChange(int amount)
        {
            m_SendButton.SetActive(amount > 0);
            m_FoundMessageCounter.Refresh();
        }
        
        private void SetGameplayUi(bool b)
        {
            m_OverlayGameObject.SetActive(b);
            m_PersonController.enabled = b;
            m_Crosshair.enabled = b;
        }

        private void Update()
        {
            bool isInGameplayMode = m_OverlayGameObject.activeSelf;
            if (!isInGameplayMode) return;
            
            if (Input.GetKeyDown(KeyCode.Return) && m_SendButton.activeSelf)
            {
                OpenQrPopup();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                ShowConfirmExit();
            }
        }

        public void OnAmountOfViewedMemoriesChange()
        {
            m_FoundMessageCounter.Refresh();
        }

        public void OnAllMemoriesRead()
        {
            m_AllMemoriesPrompt.SetActive(true);
        }

        private void ShowConfirmExit()
        {
            SetGameplayUi(false);
            m_ConfirmExit.SetActive(true);
        }

        public void HideConfirmExit()
        {
            SetGameplayUi(true);
            m_ConfirmExit.SetActive(false);
        }
    }
}
