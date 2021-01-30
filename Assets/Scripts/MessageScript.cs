using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageScript
{
    private MessageItemScript m_Item;

    public MessageItemScript Item
    {
        get { return m_Item; }
        set { m_Item = value; }
    }

    public string m_MessageText = "";
    public int m_CreationOrder = -1;


    public MessageScript(MessageItemScript item, string messageText, int creationOrder)
    {
        m_Item = item;
        m_MessageText = messageText;
        m_CreationOrder = creationOrder;
    }
    
    public string GetItemName() 
    {
        if (m_Item != null)
            return m_Item.name;
        else
            return "misingit";
    }
}
