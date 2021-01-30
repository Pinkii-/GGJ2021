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

        public void InitAsWritableNote(string originalContent, Action<string> onSubmitButtonClicked)
        {
            m_InputField.interactable = true;
            m_InputField.text = originalContent;
            m_OnSaveButtonClicked = onSubmitButtonClicked;
            gameObject.SetActive(true);
        }

        public void InitAsReadableNote(string content, Action<string> onSubmitButtonClicked)
        {
            m_InputField.interactable = false;
            m_InputField.text = content;
            m_OnSaveButtonClicked = onSubmitButtonClicked;
            gameObject.SetActive(true);
        }
        
        public void OnCancelButtonPressed()
        {
            // TODO: Animation? sound?
            gameObject.SetActive(false);
        }

        public void OnSaveButtonPressed()
        {
            // TODO: Animation? sound?
            gameObject.SetActive(false);
            
            m_OnSaveButtonClicked?.Invoke(m_InputField.text);
        }

        private void Update()
        {
            if (m_InputField.isFocused) return;

            if (Input.GetKey(KeyCode.Escape))
            {
                OnCancelButtonPressed();
            }
            else if (Input.GetKey(KeyCode.Return))
            {
                OnSaveButtonPressed();
            }
        }
    }
}
