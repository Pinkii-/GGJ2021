using System;
using System.Collections.Generic;
using System.Linq;
using GameplayUI;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public enum GameManagerMode
    {
        Default,
        ReadMode,
        WriteMode
    }

    [Header("References")] 
    [SerializeField] private GameplayUiController m_GameplaUiController;

    private GameManagerMode mode;
    // list of messages
    private List<MessageScript> messages;
    // list of read messages
    private List<MessageScript> readMessages;

    public GameManagerMode Mode
    {
        get { return mode; }
        set { mode = value; }
    }

    // singleton
    public static GameManagerScript gameManagerRef;

    void Awake()
    {
        gameManagerRef = this;
        mode = GameManagerMode.Default;
        messages = new List<MessageScript>();
        readMessages = new List<MessageScript>();
    }

    public void StartReadMode(string password)
    {
        mode = GameManagerMode.ReadMode;
        InitMessagesFromPassword(password);
    }

    public void StartWriteMode()
    {
        mode = GameManagerMode.WriteMode;
    }

    public void AddMessage(MessageItemScript item, string messageText) 
    {
        MessageScript m = new MessageScript(item, messageText, messages.Count);

        messages.Add(m);
        
        m_GameplaUiController.OnAmountOfMemoriesChange(messages.Count);
    }

    public void RemoveMessage(MessageItemScript item)
    {
        // Find message by it's item name
        string itemName = item.name;

        foreach (MessageScript m in messages) 
        {
            if (m.GetItemName() == itemName)
            {
                RemoveItem(m);
                Debug.Log("Message related to item " + itemName + " has been deleted.");
                break;
            }
        }
        Debug.LogWarning("There's no message attached to item " + itemName + ". No deletion is posssible.");
    }
    
    private void RemoveItem(MessageScript item)
    {
        messages.Remove(item);
        readMessages.Remove(item);
        
        m_GameplaUiController.OnAmountOfMemoriesChange(messages.Count);
    }

    public void ResetManager()
    {
        mode = GameManagerMode.Default;
        messages.Clear();
        readMessages.Clear();
        m_GameplaUiController.OnAmountOfMemoriesChange(messages.Count);
    }
    
    private const string MESSAGE_SEPARATOR = "omegalol";
    private const string FIELD_SEPARATOR = "hajaxa";
    
    public string GeneratePasswordFromMessages()
    {
        var password = "";

        foreach (var message in messages)
        {
            password += message.m_CreationOrder + FIELD_SEPARATOR + message.GetItemName() + FIELD_SEPARATOR + message.m_MessageText + MESSAGE_SEPARATOR;
        }
        
        return password;
    }
    
    private void InitMessagesFromPassword(string password)
    {
        var messageItems = FindObjectsOfType<MessageItemScript>().ToList();
        
        string[] rawMessages = password.Split(new string[] {MESSAGE_SEPARATOR}, StringSplitOptions.None);

        foreach (var rawMessage in rawMessages)
        {
            string[] messageContent = rawMessage.Split(new string[] {FIELD_SEPARATOR}, StringSplitOptions.None);

            int creationOrder = int.Parse(messageContent[0]);
            var itemName = messageContent[1];
            var messageText = messageContent[2];
            
            Debug.Log(creationOrder + " " + itemName + " " + messageText);

            var messageItem = messageItems.Find((script => script.name == itemName));
            if (messageItem != null)
            {
                messages.Add(new MessageScript(messageItem, messageText, creationOrder));
            }
        }
        
        m_GameplaUiController.OnAmountOfViewedMemoriesChange();
    }

    public void OnItemClicked(MessageItemScript messageItemScript)
    {
        switch (Mode)
        {
            case GameManagerMode.Default:
                throw new NotImplementedException();
                break;
            case GameManagerMode.ReadMode:
                OnItemClickedInReadMode(messageItemScript);
                break;
            case GameManagerMode.WriteMode:
                OnItemClickedInWriteMode(messageItemScript);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnItemClickedInWriteMode(MessageItemScript messageItemScript)
    {
        var item = messages.Find((message => message.Item == messageItemScript));
        if (item != null)
        {
            m_GameplaUiController.RequestWritableMessageUi(item.m_MessageText, (newText) =>
            {
                if (string.IsNullOrEmpty(newText))
                {
                    RemoveItem(item);
                    messageItemScript.ResetToDefault();
                }
                else
                {
                    item.m_MessageText = newText;
                }
            });
        }
        else
        {
            m_GameplaUiController.RequestWritableMessageUi("", text =>
            {
                if (!string.IsNullOrEmpty(text))
                {
                    AddMessage(messageItemScript, text);
                    messageItemScript.MarkAsWritten();
                }
            });
        }
    }

    private void OnItemClickedInReadMode(MessageItemScript messageItemScript)
    {
        var item = messages.Find(message => message.Item == messageItemScript && !readMessages.Contains(message));
        if (item != null)
        {
            m_GameplaUiController.RequestReadableMessageUi(item.m_MessageText, _ =>
            {
                MarkAsRead(item);
            });
        }
        else
        {
            messageItemScript.MarkAsRead(false);
        }
    }

    private void MarkAsRead(MessageScript message)
    {
        message.Item.MarkAsRead(true);
        
        if (!readMessages.Contains(message)) readMessages.Add(message);
        
        m_GameplaUiController.OnAmountOfViewedMemoriesChange();
    }

    public int GetTotalMessageCount()
    {
        return messages.Count;
    }

    public int GetViewedMessageCount()
    {
        switch (Mode)
        {
            case GameManagerMode.ReadMode:
                return readMessages.Count;
            case GameManagerMode.WriteMode:
                return GetTotalMessageCount();
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
