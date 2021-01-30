using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageItemScript : MonoBehaviour
{
    public enum MessageItemState
    { 
        Default,
        WriteHighlight,
        Written,
        ReadHighlight,
        ReadEmpty,
        ReadMessage
    }

    private MessageItemState state;

    public MessageItemState State
    {
        get { return state; }
        //set { state = value; }
    }

    public GameObject item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InCrosshair() 
    {
        switch (state)
        {
            case MessageItemState.Default :
                switch (GameManagerScript.gameManagerRef.Mode)
                {
                    case GameManagerScript.GameManagerMode.ReadMode:
                        state = MessageItemState.ReadHighlight;
                        // TODO: Add read highlight effect
                        // TEMP
                        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
                        break;
                    case GameManagerScript.GameManagerMode.WriteMode:
                        state = MessageItemState.WriteHighlight;
                        // TODO: Add write highlight effect
                        // TEMP
                        transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                        break;
                    default:
                        break;
                }
                break;
            default :
                break;
        }
    }

    public void OutCrosshair()
    {
        switch (state)
        {
            case MessageItemState.ReadHighlight:
            case MessageItemState.WriteHighlight:
                state = MessageItemState.Default;
                // TODO: remove highlight effect
                // TEMP
                transform.localScale = new Vector3(1f, 1f, 1f);
                break;
            default:
                // TODO: anything?
                break;
        }
    }

    public void Selected()
    {
        switch (state)
        {
            case MessageItemState.WriteHighlight:
            case MessageItemState.Written: // TIP: remove this state to prevent opening already written messages
            case MessageItemState.ReadHighlight:
            case MessageItemState.ReadMessage: // TIP: remove this state to prevent opening already read messages
                // Notify the Game Manager
                GameManagerScript.gameManagerRef.OnItemClicked(this);
                break;
            default:
                break;
        }
    }

    public void MarkAsRead(bool hasMessage)
    {

        if (hasMessage)
        {
            state = MessageItemState.ReadMessage;
        }
        else
            state = MessageItemState.ReadEmpty; // Dead end for this item

        // TODO remove the current highlight effect.
        // TEMP
        transform.localScale = new Vector3(1f, 1f, 1f);
        // TODO add the read highlight if ReadMessage. Golden?
    }

    public void MarkAsWritten()
    {
        state = MessageItemState.Written;

        // TODO remove the current highlight effect.
        // TEMP
        transform.localScale = new Vector3(1f, 1f, 1f);
        // TODO add the read highlight. Golden?
    }
}
