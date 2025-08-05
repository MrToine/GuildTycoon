using System;
using Quests.Runtime;
using UnityEngine;

namespace GameUI.Runtime
{
    public class QuestMini : MonoBehaviour
    {

        #region Publics

        public GameObject m_check;

        #endregion


        #region Unity API

        void Start()
        {
            QuestManager.OnQuestCompleted += HandleQuestCompleted;
        }

        #endregion


        #region Main Methods

        // 

        #endregion


        #region Utils

        /* Fonctions privées utiles */
        void HandleQuestCompleted(QuestClass quest)
        {
            if (quest.State == QuestStateEnum.Completed)
            {
                m_check.gameObject.SetActive(true);
            }
        }

        #endregion


        #region Privates and Protected

        // Variables privées

        #endregion
    }
}

