using System.Collections.Generic;
using System.Linq;
using Core.Runtime;
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
            foreach (var txt in GetComponentsInChildren<TMP_Text>())
            {
                txt.text = LocalizationSystem.Instance.GetLocalizedText(txt.text);
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

        #endregion
        
        #region Utils

        void DisplayQuest(string questName)
        {
            GameObject questGO = Instantiate(_questMiniPrefab, _panel.transform);
            TMP_Text questNameLabel = questGO.GetComponentInChildren<TMP_Text>();
            questNameLabel.text = LocalizationSystem.Instance.GetLocalizedText(questName);
            questGO.GetComponent<InteractionQuestCard>().SetQuest(questName);
        }
        
        #endregion


        #region privates and protected

        [SerializeField] GameObject _panel;
        [SerializeField] GameObject _questMiniPrefab;

        #endregion
    }
}

