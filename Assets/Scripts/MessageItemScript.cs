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
        set { state = value; }
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
}
