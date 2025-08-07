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
            
            if (FactExists<Dictionary<string, QuestStateEnum>>("quests", out _))
            {
                Dictionary<string, QuestStateEnum> quests = GetFact<Dictionary<string, QuestStateEnum>>("quests");

                foreach (var quest in quests)
                {
                    Info($"          52. 🚨 [QuestPanel.cs] La quête {quest.Key} est dans l'état : {quest.Value}.");
                    DisplayQuest(quest.Key, quest.Value); //=> Key : Le nom de la quête, Value : Le State
                }
            }
        }
        
        void DisplayQuest(string questName, QuestStateEnum stateQuest)
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
            
            Info($"          71. 🚨 [QuestsPanel.cs.cs] La quête {questName} est dans l'état : {stateQuest}.");
            questGO.GetComponent<InteractionQuestCard>().SetQuest(questName, stateQuest);
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
