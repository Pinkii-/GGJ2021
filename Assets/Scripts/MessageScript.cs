using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageScript : MonoBehaviour
{
    private MessageItemScript item;

    public MessageItemScript Item 
    {
        get { return item; }
        set { item = value; }
    }

    public string messageText;
    public string tempMessageText;
    public int creationOrder;

    // Start is called before the first frame update
    void Awake()
    {
        messageText = "";
        tempMessageText = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
