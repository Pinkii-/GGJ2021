using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public enum GameManagerMode
    {
        Default,
        ReadMode,
        WriteMode
    }

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
        MessageScript m = new MessageScript();
        m.messageText = messageText;
        m.Item = item;

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
    
    
    // TODO: Get all messages in the needed structure for creating the QR code
    private string GeneratePasswordFromMessages()
    {
        var password = "";

        foreach (var message in messages)
        {
            password += message.creationOrder + FIELD_SEPARATOR + message.GetItemName() + FIELD_SEPARATOR + message.messageText + MESSAGE_SEPARATOR;
        }
        
        return password;
    }
    
    // TODO: Populate messages when reading
    private void InitMessagesFromPassword(string password)
    {
        string[] rawMessages = password.Split(new string[] {MESSAGE_SEPARATOR}, StringSplitOptions.None);

        foreach (var rawMessage in rawMessages)
        {
            string[] messageContent = rawMessage.Split(new string[] {FIELD_SEPARATOR}, StringSplitOptions.None);

            int creationOrder = int.Parse(messageContent[0]);
            var itemName = messageContent[1];
            var messageText = messageContent[2];
            
            //TODO: Generate message
            Debug.Log(creationOrder + " " + itemName + " " + messageText);
        }
    }
}
