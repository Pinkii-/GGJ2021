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

    public GameManagerMode Mode
    {
        get { return mode; }
        set { mode = value; }
    }

    // list of messages
    public MessageScript[] messages;

    // singleton
    public static GameManagerScript gameManagerRef;

    void Awake()
    {
        gameManagerRef = this;
        mode = GameManagerMode.Default;
    }

    // TODO: Change game mode read / write
    // TODO: Add message when writing
    // TODO: Remove message when writing
    // TODO: Populate messages when reading
    // TODO: Get all messages in the needed structure for creating the QR code
}
