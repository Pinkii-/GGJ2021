using SceneManagement;
using UnityEngine;

namespace GameplayUI
{
    public class ExitToMenu : MonoBehaviour
    {
        public void ExitToMenuWithSound()
        {
            AudioManager.audioManagerRef.PlaySound("DoorClosed");
            GameSceneManager.m_ThisSingletonMakesMeCry.UnloadGameplayScene();
        }
    }
}
