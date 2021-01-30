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

    private void removeEffects()
    {
        // TODO TEMP
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void addReadHighlightEffect()
    {
        // TODO TEMP
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    private void addWriteHighlightEffect()
    {
        // TODO TEMP
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    private void addReadEffect()
    {
        // TODO add the read highlight if ReadMessage. Golden?
        // TEMP
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
    private void addWrittenEffect()
    {
        // TODO add the read highlight. Golden?
        // TEMP
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
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
                        addReadHighlightEffect();
                        break;
                    case GameManagerScript.GameManagerMode.WriteMode:
                        state = MessageItemState.WriteHighlight;
                        addWriteHighlightEffect();
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
                removeEffects();
                break;
            default:
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
            state = MessageItemState.ReadEmpty; // Dead end for this message item

        removeEffects();
        addReadEffect();
    }

    public void MarkAsWritten()
    {
        state = MessageItemState.Written;
        removeEffects();
        addWrittenEffect();
    }

    public void ResetToDefault()
    {
        state = MessageItemState.Default;
        removeEffects();
    }
}
