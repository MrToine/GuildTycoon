using System;
using Core.Runtime;
using Quests.Runtime;
using TMPro;
using UnityEngine;

namespace GameUI.Runtime.Events
{
    public class EventsDisplayUI : BaseMonobehaviour
    {

        #region Publics

        public TextMeshProUGUI m_questNameText;
        public TextMeshProUGUI m_questDescriptionText;

        #endregion


        #region Unity API

        void Start()
        {
            QuestManager.OnEventReceived += ShowEvent;
            QuestManager.OnEventFromQuest += ShowQuestName;
            m_questNameText.text = "";
            m_questDescriptionText.text = "";
        }

        void OnDestroy()
        {
            QuestManager.OnEventReceived -= ShowEvent;
            QuestManager.OnEventFromQuest -= ShowQuestName;
        }

        #endregion


        #region Main Methods

        // 

        #endregion


        #region Utils

        /* Fonctions privées utiles */
        void ShowQuestName(QuestClass quest)
        {
            m_questNameText.text = LocalizationSystem.Instance.GetLocalizedText(quest.Name);
        }
        
        void ShowEvent(QuestEvent questEvent)
        {
            m_questDescriptionText.text = LocalizationSystem.Instance.GetLocalizedText(questEvent.DescriptionKey);
        }

        #endregion


        #region Privates and Protected

        // Variables privées

        #endregion
    }
}

