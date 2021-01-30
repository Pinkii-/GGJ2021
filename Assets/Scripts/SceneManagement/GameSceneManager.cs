using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class GameSceneManager : MonoBehaviour
    {
        [SerializeField] private GameObject m_MenuRoot;
        
        private const string GAMEPLAY_SCENE = "LogicTest";
        
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
    }
}
