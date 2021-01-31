using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundMessageCounter : MonoBehaviour
{
    [SerializeField] private GameObject m_MemoryElementPrefab;
    
    void Start()
    {
        m_MemoryElementPrefab.SetActive(false);
    }

    public void Refresh()
    {
        for (int i = 1; i < transform.childCount; ++i)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        int totalMessageCount = GameManagerScript.gameManagerRef.GetTotalMessageCount();
        int viewedMessages = GameManagerScript.gameManagerRef.GetViewedMessageCount();

        for (int i = 0; i < totalMessageCount; ++i)
        {
            var go = Instantiate(m_MemoryElementPrefab, transform);
            go.SetActive(true);
            if (i < viewedMessages) go.transform.Find("Full Slot").gameObject.SetActive(true);
            else go.transform.Find("Empty Slot").gameObject.SetActive(true);
        }
    }
}
