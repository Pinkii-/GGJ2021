using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class GameSceneManager : MonoBehaviour
    {
        private const string GAMEPLAY_SCENE = "GameScene";
        
        public void PushFindGameplay(string password)
        {
            StartCoroutine(LoadGameplay());

        }

        IEnumerator LoadGameplay()
        {
            var asyncOperation = SceneManager.LoadSceneAsync(GAMEPLAY_SCENE, LoadSceneMode.Additive);

            while (!asyncOperation.isDone)
            {
                yield return null;
            }
        }
    }
}
