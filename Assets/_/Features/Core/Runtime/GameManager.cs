using System;
using System.Collections;
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

        public int CurrentGameTime
        {
            get
            {
                return _currentGameTime;
            }
            set
            {
                _currentGameTime = value;
            }
        }

        public bool LaunchedTime
        {
            get
            {
                return _launchedTime;
            }
            set
            {
                _launchedTime = value;
            }
        }
        
        public static event Action<int> OnTimeAdvanced;
        
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
            _fact = m_gameFacts;
            CurrentLanguage = m_gameFacts.GetFact<string>("language");

            if (!m_gameFacts.FactExists<string>("language", out _))
            {
                m_gameFacts.SetFact("language", "fr", FactPersistence.Persistent);
            }
            LocalizationSystem.Instance.LoadLanguage();
        }

        #endregion


        #region Main Methods

        public void ReloadScene()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            _sceneLoader.LoadScene(sceneName);
        }

        public void UpdateGameTime(GameTime gameTime)
        {
            _gameTime = gameTime;
        }
        
        // Time Gestion
        public void AdvanceTime(int secondes)
        {
            _gameTime.Advance(secondes);
            OnTimeAdvanced?.Invoke(_gameTime.TotalSeconds);
        }

        public void LaunchGameTime()
        {
            if (LaunchedTime) return;
            LaunchedTime = true;
            StartCoroutine(TimeTickLoop());
        }

        #endregion


        #region Utils

        /* Fonctions privées utiles */
        private IEnumerator TimeTickLoop()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(1f);
                AdvanceTime(1);
                _currentGameTime++;
            }
        }

        #endregion


        #region Privates and Protected

        private bool isOnPause = false;
        private SceneLoader _sceneLoader;
        private FactDictionnary _fact;
        private bool _canPause;
        private string _profile;
        private string _currentLanguage = "en";
        private Dictionary<string, string> _localTexts;
        bool _launchedTime;
        int _currentGameTime = 0;
        GameTime _gameTime;

        #endregion
    }
}