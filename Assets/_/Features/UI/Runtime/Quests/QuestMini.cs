using System;
using Core.Runtime;
using Quests.Runtime;
using UnityEngine;

namespace GameUI.Runtime
{
    public class QuestMini : BaseMonobehaviour
    {

        #region Publics

        public GameObject m_check;
        public GameObject m_hourglass;

        #endregion


        #region Unity API

        void Start()
        {
            QuestManager.OnQuestCompleted += HandleQuestCompleted;
        }
        
        void OnDestroy()
        {
            QuestManager.OnQuestCompleted -= HandleQuestCompleted;
        }

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
            if (!MatchesQuest(quest.Name)) return;
            switch (quest.State)
            {
                case QuestStateEnum.Disponible:
                    m_check.SetActive(false);
                    m_hourglass.SetActive(false);
                    break;
                case QuestStateEnum.Active:
                    m_check.SetActive(false);
                    m_hourglass.SetActive(true);
                    break;
                case QuestStateEnum.Completed:
                    m_check.SetActive(true);
                    m_hourglass.SetActive(false);
                    break;
            }
        }

        #endregion


        #region Privates and Protected

        // Variables privées
        string _questName;

        #endregion
    }
}
