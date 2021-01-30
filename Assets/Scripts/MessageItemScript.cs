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

    private MessageItemState m_state;
    private GameObject m_child;

    public MessageItemState State
    {
        get { return m_state; }
        //set { state = value; }
    }

    void Awake()
    {
        m_child = transform.GetChild(0).gameObject;
    }

    private void removeEffects()
    {
        // TODO TEMP
        m_child.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void addReadHighlightEffect()
    {
        // TODO TEMP
        m_child.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    private void addWriteHighlightEffect()
    {
        // TODO TEMP
        m_child.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    private void addReadEffect()
    {
        // TODO add the read highlight if ReadMessage. Golden?
        // TEMP
        m_child.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
    private void addWrittenEffect()
    {
        // TODO add the read highlight. Golden?
        // TEMP
        m_child.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }


    public void InCrosshair() 
    {
        switch (m_state)
        {
            case MessageItemState.Default :
                switch (GameManagerScript.gameManagerRef.Mode)
                {
                    case GameManagerScript.GameManagerMode.ReadMode:
                        m_state = MessageItemState.ReadHighlight;
                        addReadHighlightEffect();
                        break;
                    case GameManagerScript.GameManagerMode.WriteMode:
                        m_state = MessageItemState.WriteHighlight;
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
        switch (m_state)
        {
            case MessageItemState.ReadHighlight:
            case MessageItemState.WriteHighlight:
                m_state = MessageItemState.Default;
                removeEffects();
                break;
            default:
                break;
        }
    }

    public void Selected()
    {
        switch (m_state)
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
            m_state = MessageItemState.ReadMessage;
        }
        else
            m_state = MessageItemState.ReadEmpty; // Dead end for this message item

        removeEffects();
        addReadEffect();
    }

    public void MarkAsWritten()
    {
        m_state = MessageItemState.Written;
        removeEffects();
        addWrittenEffect();
    }

    public void ResetToDefault()
    {
        m_state = MessageItemState.Default;
        removeEffects();
    }
}
