using System;
using TMPro;
using UnityEngine;

namespace GameplayUI
{
    public class MessageUI : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private TMP_InputField m_InputField;

        private Action<string> m_OnSaveButtonClicked;
        private Action m_OnCancelButtonClicked;

        public void InitAsWritableNote(string originalContent, Action<string> onSubmitButtonClicked, Action onCancelButtonClicked)
        {
            m_InputField.interactable = true;
            m_InputField.text = originalContent;
            m_OnSaveButtonClicked = onSubmitButtonClicked;
            m_OnCancelButtonClicked = onCancelButtonClicked;
            m_InputField.ActivateInputField();
        }

        public void InitAsReadableNote(string content, Action<string> onSubmitButtonClicked, Action onCancelButtonClicked)
        {
            m_InputField.interactable = false;
            m_InputField.text = content;
            m_OnSaveButtonClicked = onSubmitButtonClicked;
            m_OnCancelButtonClicked = onCancelButtonClicked;
            m_InputField.ActivateInputField();
        }
        
        public void OnCancelButtonPressed()
        {
            AudioManager.audioManagerRef.PlaySound("PaperSend");
            
            m_OnCancelButtonClicked?.Invoke();
        }

        public void OnSaveButtonPressed()
        {
            AudioManager.audioManagerRef.PlaySoundWithRandomPitch("PaperSend");
            
            m_OnSaveButtonClicked?.Invoke(m_InputField.text.Trim(' ', '\r', '\n'));
        }

        private void Update()
        {
            if (m_InputField.isFocused) return;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                OnSaveButtonPressed();
            }
        }
    }
}
