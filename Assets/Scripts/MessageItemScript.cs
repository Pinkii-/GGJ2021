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
    private GameObject m_highlightParticleSystem;

    public string itemSound;
    public GameObject particleSystemPrefab;
    public GameObject particlePivot;

    public MessageItemState State
    {
        get { return m_state; }
    }

    void Awake()
    {
        m_child = transform.GetChild(0).gameObject;
    }

    private void removeEffects()
    {
        if (m_highlightParticleSystem != null) 
        {

         m_highlightParticleSystem.SetActive(false);

        }
        //m_child.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void addReadHighlightEffect()
    {
        enableParticleEffects();
    }

    private void addWriteHighlightEffect()
    {
        enableParticleEffects();
    }

    private void addReadEffect()
    {
        // TODO add the read highlight. Golden?
        enableParticleEffects();
        ParticleSystem.MainModule settings = m_highlightParticleSystem.GetComponent<ParticleSystem>().main;
        settings.startColor = new ParticleSystem.MinMaxGradient(new Color(1, 0.8f, 0, 1f));
        settings.startSizeMultiplier = 0.25f;
    }
    private void addWrittenEffect()
    {
        // TODO add the read highlight. Golden?
        enableParticleEffects();
        ParticleSystem.MainModule settings = m_highlightParticleSystem.GetComponent<ParticleSystem>().main;
        settings.startColor = new ParticleSystem.MinMaxGradient(new Color(1, 0.8f, 0, 1f));
        settings.startSizeMultiplier = 0.25f;
    }

    private void enableParticleEffects() 
    {
        Vector3 position;

        if (particlePivot != null)
            position = particlePivot.transform.position;
        else
            position = m_child.transform.position;

        if (m_highlightParticleSystem == null && particleSystemPrefab != null)
        {
            m_highlightParticleSystem = Instantiate(particleSystemPrefab, position, Quaternion.identity);
            m_highlightParticleSystem.transform.parent = transform;
        }

        if(m_highlightParticleSystem != null)
            m_highlightParticleSystem.SetActive(true);
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
                if(itemSound != null && itemSound != "")
                    AudioManager.audioManagerRef.PlaySoundWithRandomPitch(itemSound);
                break;
            default:
                break;
        }
    }

    public void MarkAsRead(bool hasMessage)
    {
        removeEffects();

        if (hasMessage)
        {
            m_state = MessageItemState.ReadMessage;
            addReadEffect();
        }
        else
        {
            m_state = MessageItemState.ReadEmpty; // Dead end for this message item
            AudioManager.audioManagerRef.PlaySoundWithRandomPitch("Checkmark");
        }

    }

    public void MarkAsWritten()
    {
        removeEffects();
        m_state = MessageItemState.Written;
        addWrittenEffect();
    }

    public void ResetToDefault()
    {
        m_state = MessageItemState.Default;
        removeEffects();
    }
}
