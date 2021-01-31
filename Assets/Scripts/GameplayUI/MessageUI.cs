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
            m_InputField.Select();
        }

        public void InitAsReadableNote(string content, Action<string> onSubmitButtonClicked, Action onCancelButtonClicked)
        {
            m_InputField.interactable = false;
            m_InputField.text = content;
            m_OnSaveButtonClicked = onSubmitButtonClicked;
            m_OnCancelButtonClicked = onCancelButtonClicked;
            m_InputField.Select();
        }
        
        public void OnCancelButtonPressed()
        {
            // TODO: Animation? sound?
            m_OnCancelButtonClicked?.Invoke();
        }

        public void OnSaveButtonPressed()
        {
            // TODO: Animation? sound?
            
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
