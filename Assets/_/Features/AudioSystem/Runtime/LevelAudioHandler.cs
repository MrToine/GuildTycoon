using System;
using UnityEngine.SceneManagement;

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

        #endregion


        #region Utils

        private void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
        {
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

