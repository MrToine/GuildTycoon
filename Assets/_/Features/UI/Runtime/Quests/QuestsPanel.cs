using System;
using System.Collections.Generic;
using System.Linq;
using Core.Runtime;
using EventSystem.Runtime;
using Quests.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameUI.Runtime
{
    public class QuestsPanel : BaseMonobehaviour
    {
        #region Publics
        
        //
        
        #endregion
        
        #region Unity API

        private void Start()
        {
            QuestSignals.OnRefresh += QuestList;
            QuestManager.OnQuestCompleted += QuestCompleted;
            
            foreach (var txt in GetComponentsInChildren<TMP_Text>())
            {
                txt.text = LocalizationSystem.Instance.GetLocalizedText(txt.text);
            }

            QuestList();
        }

        #endregion
        
        #region Utils

        void QuestList()
        {
            foreach (Transform child in _panel.transform)
            {
                Destroy(child.gameObject);
            }
            
            if (FactExists<List<QuestClass>>("quests", out _))
            {
                List<QuestClass>  questsFromSave = GetFact<List<QuestClass>>("quests");

                List<QuestClass> quests = QuestManager.Instance.ResolveQuestsList(questsFromSave);
                foreach (var quest in quests)
                {
                    DisplayQuest(quest); //=> Key : L'id de la quête, Value : Le State
                }
            }
        }
        
        void DisplayQuest(QuestClass quest)
        {
            GameObject questGO = Instantiate(_questMiniPrefab, _panel.transform);
            TMP_Text questNameLabel = questGO.GetComponentInChildren<TMP_Text>();
            questNameLabel.text = LocalizationSystem.Instance.GetLocalizedText(quest.Name);

            var questMini = questGO.GetComponent<QuestMini>();
            if (questMini != null)
            {
                questMini.SetQuestName(quest.Name);
                bool isCompleted = QuestManager.Instance?.IsQuestCompleted(quest.Name) == true;
                questMini.m_check?.SetActive(isCompleted);
            }
            
            questGO.GetComponent<InteractionQuestCard>().SetQuest(quest);
        }

        void QuestCompleted(QuestClass quest)
        {
            CheckCompleted(quest);
        }

        void CheckCompleted(QuestClass quest)
        {
            foreach (Transform child in _panel.transform)
            {
                var questMini = child.GetComponent<QuestMini>();
                if (questMini != null && questMini.MatchesQuest(quest.Name))
                {
                    questMini.m_check?.SetActive(true);
                }
            }
        }
        
        #endregion


        #region privates and protected

        [SerializeField] GameObject _panel;
        [SerializeField] GameObject _questMiniPrefab;
        
        #endregion
    }
}
