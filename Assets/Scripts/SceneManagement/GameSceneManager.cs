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

        private const string DEFAULT_PASSWORD = "0hajaxaSofahajaxahey guys!";
        private const string GAMEPLAY_SCENE = "GameScene";

        public bool m_ReadNeedTutorial = true;
        public bool m_WriteNeedTutorial = true;


        public void Awake()
        {
            m_ThisSingletonMakesMeCry = this;
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
        }
    }
}
