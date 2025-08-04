using Adventurer.Runtime;
using Core.Runtime;
using EventSystem.Runtime;
using UnityEngine;

namespace GameUI.Runtime
{
    public class AdventurerUIController : BaseMonobehaviour
    {

        #region Publics

        //

        #endregion


        #region Unity API

        void Start()
        {
            AdventurerSignals.OnInfoAdventurerPanel += HandleInfoPanel;
            _uiManager = GetComponent<UIManager>();
        }

        
        #endregion


        #region Main Methods

        // 

        #endregion


        #region Utils

        /* Fonctions privées utiles */
        void HandleInfoPanel(AdventurerClass adventurer)
        {
            _uiManager.ShowPanel(_infoAdventurerPanel);
            _infoAdventurerPanel.GetComponent<InfoAdventurerPanel>().ShowInfo(adventurer);
        }

        #endregion


        #region Privates and Protected

        [SerializeField] private GameObject _infoAdventurerPanel;
        UIManager _uiManager;

        #endregion
    }
}

