using System;
using Core.Runtime;
using EventSystem.Runtime;
using Quests.Runtime;
using UnityEngine;

namespace GameUI.Runtime
{
    public class QuestUIController : BaseMonobehaviour
    {

        #region Publics

        //

        #endregion


        #region Unity API

        void Start()
        {
            QuestSignals.OnInfoQuestPanel += HandleInfoPanel;
            _uiManager = GetComponent<UIManager>();
        }

        #endregion


        #region Main Methods

        // 

        #endregion


        #region Utils

        /* Fonctions privées utiles */
        void HandleInfoPanel(QuestClass quest)
        {
            _uiManager.ShowPanel(_infoQuestPanel);
            _infoQuestPanel.GetComponent<InfoQuestPanel>().ShowInfo(quest);
        }

        #endregion


        #region Privates and Protected

        UIManager _uiManager;
        [SerializeField] GameObject _infoQuestPanel;

        #endregion
    }
}

