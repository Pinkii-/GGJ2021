using System;
using TMPro;
using UnityEngine;

namespace GameplayUI
{
    public class MessageUI : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private TMP_InputField m_InputField;

        public void InitAsWritableNote(string originalContent)
        {
            m_InputField.interactable = true;
            m_InputField.text = originalContent;
        }

        public void InitAsReadableNote(string content)
        {
            m_InputField.interactable = false;
            m_InputField.text = content;
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
            
            //TODO: set text to message
            //GameManagerScript.gameManagerRef
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
