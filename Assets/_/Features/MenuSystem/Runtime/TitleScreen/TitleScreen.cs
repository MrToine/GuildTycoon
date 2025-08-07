using System.Collections.Generic;
using Core.Runtime;
using DG.Tweening;
using Shared.Runtime;
using TMPro;
using UnityEngine;

namespace MenuSystem.Runtime.TitleScreen
{
    public class TitleScreen : BaseMonobehaviour
    {

        #region Publics

        //

        #endregion


        #region Unity API

        void Start()
        {
            LocalizationSystem.Instance.LocalizeAllTextsIn(_menuPanel.transform);
            LocalizationSystem.Instance.LocalizeAllTextsIn(_newGamePanel.transform);
            LocalizationSystem.Instance.LocalizeAllTextsIn(_settingsPanel.transform);
            LocalizationSystem.Instance.LocalizeAllTextsIn(_loadPanel.transform);
        }
        private void Update()
        {
            //GameManager.Instance.CanPause = false;
            if (!GameManager.Instance.SaveFileExist)
            {
                _continueButton.SetActive(false);
                _loadButton.SetActive(false);
            }
        }

        #endregion


        #region Main Methods

        public void Continue()
        {
            GameManager.Instance.Profile = "continue";
            Info($"On charge le faux profile continue : {GameManager.Instance.Profile}");
            LoadFacts();
            string profileName = GetFact<string>("profile");
            Info($"On charge le vrai profile : {profileName}");
            GameManager.Instance.Profile = profileName;
            LoadFacts();
            SceneLoader.Instance.LoadScene("Game");
        }

        public void New()
        {
            NewGame();
        }

        public void ReturnHome()
        {
            ReturnToMenu();
        }

        public void GoSettings()
        {
            Settings();
        }
        
        public void LoadGame()
        {
            Load();
        }

        #endregion


        #region Utils

        private void NewGame()
        {
            _seq = DOTween.Sequence();
            Slide(_menuPanel, "out");
            Slide(_newGamePanel);
        }

        private void Settings()
        {
            _seq = DOTween.Sequence();
            Slide(_menuPanel, "out");
            Slide(_settingsPanel, "in");
        }
        
        private void Load()
        {
            _seq = DOTween.Sequence();
            Slide(_menuPanel, "out");
            Slide(_loadPanel, "in");
        }
        
        private void ReturnToMenu()
        {
            _seq = DOTween.Sequence();
            if (_newGamePanel.transform.position.x == Screen.width / 2f)
            {
                Slide(_newGamePanel, "out");
            }
            
            if (_settingsPanel.transform.position.x == Screen.width / 2f)
            {
                Slide(_settingsPanel, "out");
            }
            
            if (_loadPanel.transform.position.x == Screen.width / 2f)
            {
                Slide(_loadPanel, "out");
            }
            Slide(_menuPanel, "in");
        }
        
        private void Slide(GameObject gameObject, string state = "in")
        {
            if(state == "in")
            {
                //_panel.position = new Vector2(200, _panel.position.y);
                _seq.Append(gameObject.transform.DOMove(new Vector3(Screen.width / 2f, gameObject.transform.position.y, 0), 0.3f).SetEase(Ease.Linear).SetUpdate(true));

            }
            else
            {
                //_panel.position = new Vector2(-200, _panel.position.y);
                _seq.Append(gameObject.transform.DOMove(new Vector3(-1135, gameObject.transform.position.y, 0), 0.3f).SetEase(Ease.Linear));
            }
        }

        #endregion


        #region Privates and Protected
        
        [Header("Panels")]
        [SerializeField] private GameObject _newGamePanel;
        [SerializeField] private GameObject _menuPanel;
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private GameObject _loadPanel;
        
        [Header("Buttons")]
        [SerializeField] private GameObject _continueButton;
        [SerializeField] private GameObject _loadButton;
        
        private List<GameObject> _panels = new List<GameObject>();
        private Sequence _seq;

        #endregion
    }
}

