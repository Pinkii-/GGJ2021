using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class GameSceneManager : MonoBehaviour
    {
        public static GameSceneManager m_ThisSingletonMakesMeCry;
        
        [SerializeField] private GameObject m_MenuRoot;

        private const string DEFAULT_PASSWORD = "0hajaxaBallhajaxaFirst memory of my grandpa was when he taught my how to play football. Last one, a month after the pandemic. We couldn't say goodbye. Goodbye :)- Bomegalol1hajaxaPaintinghajaxaGoing together, the orange mountain. Growing together looking at the moon... - Gomegalol2hajaxaPCScreenhajaxaTechnology made this last year more bearable but, I'm eager to see these guys again and spend some time together. - Lomegalol3hajaxaWineBottlehajaxaWhen you're young, alcohol tastes horrible, you may force yourself to drink it in order to look cool. With time, you get used to the taste and you can actually get to enjoy it. Just be vigilant that it doesn't become a need. I have to be. -LM";
        private const string GAMEPLAY_SCENE = "GameScene";

        public bool m_ReadNeedTutorial = true;
        public bool m_WriteNeedTutorial = true;


        public void Awake()
        {
            m_ThisSingletonMakesMeCry = this;
        }
        public void Start()
        {
            AudioManager.audioManagerRef.PlaySound("MenuMusicLoop"); // menu music
        }

        public void PushDefaultFindGameplay()
        {
            PushFindGameplay(DEFAULT_PASSWORD);
        }
        
        public void PushFindGameplay(string password)
        {
            StartCoroutine(LoadGameplay(() => SetFindMode(password)));
        }

        public void PushWriteGameplay()
        {
            StartCoroutine(LoadGameplay(SetWriteMode));
        }
        
        IEnumerator LoadGameplay(Action callback)
        {
            m_MenuRoot.SetActive(false);
            
            //TODO: add a loading screen or somesing
            var asyncOperation = SceneManager.LoadSceneAsync(GAMEPLAY_SCENE, LoadSceneMode.Additive);
            while (!asyncOperation.isDone)
            {
                yield return null;
            }

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(GAMEPLAY_SCENE));

            // Change music and play ambience sound
            AudioManager.audioManagerRef.StopSound("MenuMusicLoop"); // menu music
            AudioManager.audioManagerRef.PlaySound("GameplayMusicLoop"); // gameplay music
            AudioManager.audioManagerRef.PlaySound("AmbientSpeak");

            callback?.Invoke();
        }

        private void SetFindMode(string password)
        {
            GameManagerScript.gameManagerRef.StartReadMode(password);
        }
        
        private void SetWriteMode()
        {
            GameManagerScript.gameManagerRef.StartWriteMode();
        }

        public void UnloadGameplayScene()
        {
            SceneManager.UnloadSceneAsync(GAMEPLAY_SCENE);
            m_MenuRoot.SetActive(true);

            // Stop ambience sound
            AudioManager.audioManagerRef.StopSound("AmbientSpeak");
            AudioManager.audioManagerRef.StopSound("GameplayMusicLoop"); // gameplay music
            AudioManager.audioManagerRef.PlaySound("MenuMusicLoop"); // menu music

        }
    }
}
