using Core.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MenuSystem.Runtime
{
    public class SettingsMenu : BaseMonobehaviour
    {

        #region Publics

        //

        #endregion


        #region Unity API

        private void Start()
        {
            if (_settingsPanel)
            {
                foreach (var btn in _settingsPanel.GetComponentsInChildren<TMP_Text>())
                {
                    btn.text = LocalizationSystem.Instance.GetLocalizedText(btn.text);
                }
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

        [SerializeField] private GameObject _settingsPanel;

        #endregion

    }
}

