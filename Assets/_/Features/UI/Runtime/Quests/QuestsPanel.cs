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
            
            if (FactExists<List<string>>("quests", out _))
            {
                List<string> quests = GetFact<List<string>>("quests");

                foreach (var quest in quests)
                {
                    DisplayQuest(quest);
                }
            }
        }
        
        void DisplayQuest(string questName)
        {
            GameObject questGO = Instantiate(_questMiniPrefab, _panel.transform);
            TMP_Text questNameLabel = questGO.GetComponentInChildren<TMP_Text>();
            questNameLabel.text = LocalizationSystem.Instance.GetLocalizedText(questName);

            var questMini = questGO.GetComponent<QuestMini>();
            if (questMini != null)
            {
                questMini.SetQuestName(questName);
                bool isCompleted = QuestManager.Instance?.IsQuestCompleted(questName) == true;
                questMini.m_check?.SetActive(isCompleted);
            }
            
            questGO.GetComponent<InteractionQuestCard>().SetQuest(questName);
        }

        void QuestCompleted(QuestClass quest)
        {
            Info("Information d'event reçu dans QuestPanel.cs >>>> depuis QuestManager.cs (OnQuestCompleted)");
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
