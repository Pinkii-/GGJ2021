using System;
using System.Collections;
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
    [SerializeField] private MessageUI m_MessageUI;
    
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

        // TEMP TODO DELETE
        mode = GameManagerMode.WriteMode;
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
    }

    public void RemoveMessage(MessageItemScript item)
    {
        // Find message by it's item name
        string itemName = item.name;

        foreach (MessageScript m in messages) 
        {
            if (m.GetItemName() == itemName)
            {
                messages.Remove(m);
                Debug.Log("Message related to item " + itemName + " has been deleted.");
                break;
            }
        }
        Debug.LogWarning("There's no message attached to item " + itemName + ". No deletion is posssible.");
    }

    public void ResetManager()
    {
        mode = GameManagerMode.Default;
        messages.Clear();
        readMessages.Clear();

    }
    
    private const string MESSAGE_SEPARATOR = "omegalol";
    private const string FIELD_SEPARATOR = "hajaxa";
    
    private string GeneratePasswordFromMessages()
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
            m_MessageUI.InitAsWritableNote(item.m_MessageText, (newText) =>
            {
                if (string.IsNullOrEmpty(newText))
                {
                    messages.Remove(item);
                    //TODO reset message item visual feedback and state
                }
                else
                {
                    item.m_MessageText = newText;
                }
            });
        }
        else
        {
            m_MessageUI.InitAsWritableNote("", text =>
            {
                if (!string.IsNullOrEmpty(text))
                {
                    AddMessage(messageItemScript, text);
                    // TODO change message item to written
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
            // Mark message as read
            readMessages.Add(item);
            m_MessageUI.InitAsReadableNote(item.m_MessageText, _ =>
            {
                messageItemScript.MarkAsRead(true);
            });
        }
        else
        {
            messageItemScript.MarkAsRead(false);
        }
    }
}
