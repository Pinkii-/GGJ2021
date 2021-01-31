using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlaySoundOnButtonClick : MonoBehaviour
{
    public string m_Sound = "ButtonClick";

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(PlaySound);
    }

    private void OnDestroy()
    { 
        GetComponent<Button>().onClick.RemoveListener(PlaySound);
    }

    public void PlaySound()
    {
        AudioManager.audioManagerRef.PlaySound(m_Sound);
    }
}
