using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Runtime
{
    public class GameManager: BaseMonobehaviour
    {
        
        /*
         * Appel depuis ici 
         * Localization => A FAIRE
         */
        
        #region Publics
        
        public static GameManager Instance { get; private set; }
        
        public static FactDictionnary m_gameFacts;
        
        public bool IsOnPause
        {
            get => isOnPause;
            set => isOnPause = value;
        }

        public FactDictionnary Fact
        {
            get
            {
                return _fact;
            }
            set
            {
                _fact = value;
            }
        }

        public String Profile
        {
            get
            {
                return _profile;
            }
            set
            {
                _profile = value;
            }
        }

        public bool SaveFileExist => m_gameFacts.SaveFileExists();

        public bool CanPause
        {
            get => _canPause; 
            set => _canPause = value;
        }

        public string CurrentLanguage
        {
            get
            {
                return _currentLanguage;
            }
            set
            {
                _currentLanguage = value;
            }
        }

        public Dictionary<string, string> GetLocalTexts
        {
            get
            {
                return _localTexts;
            }
            set
            {
                _localTexts = value;
            }
        }
        
        #endregion


        #region Unity API

        private void Awake()
        {
            _sceneLoader = GetComponent<SceneLoader>();
            // Si une instance existe déjà et que ce n’est pas celle-ci, détruire ce GameObject
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            // Sinon, cette instance devient l’instance unique
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            m_gameFacts = new FactDictionnary();
            _localTexts = new Dictionary<string, string>();
            
            LoadFacts("GeneralSettings");
            CurrentLanguage = m_gameFacts.GetFact<string>("language");
            
            if (!m_gameFacts.FactExists<string>("language", out _))
            {
                m_gameFacts.SetFact("language", "fr", FactPersistence.Persistent);
            }
            
            LocalizationSystem.Instance.LoadLanguage();
        }

        #endregion


        #region Main Methods

        public void CreateObject(GameObject prefab, Vector3 position)
        {
            Instantiate(prefab, position, Quaternion.identity);
        }

        public void ReloadScene()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            _sceneLoader.LoadScene(sceneName);
        }

        #endregion


        #region Utils

        /* Fonctions privées utiles */

        #endregion


        #region Privates and Protected

        private bool isOnPause = false;
        private SceneLoader _sceneLoader;
        private FactDictionnary _fact;
        private bool _canPause;
        private string _profile;
        private string _currentLanguage = "en";
        private Dictionary<string, string> _localTexts;
        
        #endregion
    }
}