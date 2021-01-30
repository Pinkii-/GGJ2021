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

    public void StartReadMode()
    {
        mode = GameManagerMode.ReadMode;
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

    // TODO: Get all messages in the needed structure for creating the QR code
    // TODO: Populate messages when reading
}
