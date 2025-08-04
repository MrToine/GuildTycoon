using Core.Runtime;
using UnityEngine;

namespace MenuSystem.Runtime.Settings
{
    public class AudioMenuModule : BaseMonobehaviour, IMenuModule
    {
        #region Publics

        public string GetMenuName() => "Audio";
        public GameObject GetMenuPanel() => _audioPanel;

        #endregion


        #region Unity API

        void Start()
        {
            if (MenuManager.Instance != null)
            {
                MenuManager.Instance.RegisterMenu(this);
            }
        }

        #endregion


        #region Main Methods

        // 

        #endregion


        #region Utils

        /* Fonctions privées utiles */

        #endregion


        #region Privates and Protected

        [SerializeField] private GameObject _audioPanel;

        #endregion
    }
}

