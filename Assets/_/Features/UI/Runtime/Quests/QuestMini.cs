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

        //

        #endregion


        #region Main Methods

        public void SetQuestName(string name)
        {
            _questName = name;
        }
        
        public bool MatchesQuest(string questNameToCheck)
        {
            return _questName == questNameToCheck;
        }

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
        string _questName;

        #endregion
    }
}
