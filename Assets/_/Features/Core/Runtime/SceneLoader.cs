using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Core.Runtime
{
    public class SceneLoader : BaseMonobehaviour
    {

        #region Publics

        public static event Action<Scene> OnSceneLoaded;
        public static string CurrentSceneName => SceneManager.GetActiveScene().name;
        public static SceneLoader Instance { get; private set; }

        #endregion


        #region Unity API

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            SceneManager.sceneLoaded += HandleSceneLoaded;
        }

        void OnDestroy()
        {
            SceneManager.sceneLoaded -= HandleSceneLoaded;
        }

        #endregion


        #region Main Methods

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void FakeLoading(string nextSceneName)
        {
            if (SceneExists("Loading"))
            {
                LoadScene("Loading");
                StartCoroutine(WaitingLoading(nextSceneName));
            }
        }

        #endregion


        #region Utils

        private IEnumerator WaitingLoading(string nextSceneName)
        {
            int time = Random.Range(3, 6);
            yield return new WaitForSeconds(time);
            LoadScene(nextSceneName);
        }

        private void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (FactExists<GameTime>("game_time", out _))
            {
                GameTime gameTime = GetFact<GameTime>("game_time");
                GameManager.Instance.UpdateGameTime(gameTime);
                GameManager.Instance.CurrentGameTime = gameTime.TotalSeconds;
                GameManager.Instance.LaunchGameTime();
            }
            else
            {
                // Debug supprimé
            }
            
            OnSceneLoaded?.Invoke(scene);
        }
        
        private bool SceneExists(string sceneName)
        {
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                string path = SceneUtility.GetScenePathByBuildIndex(i);
                string name = System.IO.Path.GetFileNameWithoutExtension(path);
                if (name == sceneName)
                    return true;
            }
            return false;
        }
        
        #endregion


        #region Privates and Protected

        //

        #endregion
    }
}

