using System;
using System.Collections.Generic;
using Adventurer.Runtime;
using Core.Runtime;
using EventSystem.Runtime;
using Quests.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI.Runtime
{
    public class InfoQuestPanel : BaseMonobehaviour
    {

        #region Publics

        public TextMeshProUGUI m_title;
        public TextMeshProUGUI m_description;
        public TextMeshProUGUI m_objective;
        public TextMeshProUGUI m_reward;

        #endregion


        #region Unity API

        void Start()
        {
            AdventurerSignals.OnAdventurerSelected += HandleAddAdventurersFromQuest;
            AdventurerSignals.OnAdventurerUnselected += HandleRemoveAdventurersFromQuest;
            
            foreach (var txt in GetComponentsInChildren<TMP_Text>())
            {
                txt.text = LocalizationSystem.Instance.GetLocalizedText(txt.text);
            }
        }

        void OnDestroy()
        {
            AdventurerSignals.OnAdventurerSelected -= HandleAddAdventurersFromQuest;
            AdventurerSignals.OnAdventurerUnselected -= HandleRemoveAdventurersFromQuest;
            
        }

        void Update()
        {
            if (_adventurersSelected.Count >= 1)
            {
                _buttonActivation.GetComponent<Button>().interactable = true;
                _buttonActivation.GetComponentInChildren<TMP_Text>().color = Color.yellow;
            }
            else
            {
                _buttonActivation.GetComponent<Button>().interactable = false;
                _buttonActivation.GetComponentInChildren<TMP_Text>().color = Color.grey;
            }
        }

        #endregion


        #region Main Methods

        public void ShowInfo(QuestClass quest)
        {
            if (FactExists<List<QuestClass>>("active_quests", out _))
            {
                foreach (QuestClass activeQuest in GetFact<List<QuestClass>>("active_quests"))
                {
                    if (quest.Name == activeQuest.Name)
                    {
                        quest = activeQuest;
                    }
                }
            }
            QuestManager.Instance.CurrentQuest = quest;
            m_title.text = LocalizationSystem.Instance.GetLocalizedText(quest.Name);
            m_description.text = LocalizationSystem.Instance.GetLocalizedText(quest.Description);
            m_objective.text = LocalizationSystem.Instance.GetLocalizedText(quest.Objective);
            m_reward.text = RewardsListJoined(quest.Rewards);

            switch (QuestManager.Instance.CurrentQuest.State)
            {
                case QuestStateEnum.Disponible:
                    _buttonActivation.gameObject.SetActive(true);
                    _adventurersOnThisQuestPanel.SetActive(false);
                    break;
                default:
                    _buttonActivation.gameObject.SetActive(false);
                    _adventurersOnThisQuestPanel.SetActive(true);
                    break;
            }
        }

        public void LaunchQuest()
        {
            if (_adventurersSelected.Count > 0)
            {
                GameTime gameTime = GetFact<GameTime>("game_time");
                QuestManager.Instance.StartQuest(QuestManager.Instance.CurrentQuest, _adventurersSelected, gameTime);
                _adventurersSelected.Clear();
                AdventurerSignals.RaiseRefreshAdventurers();

                List<QuestClass> activeQuests = GetFact<List<QuestClass>>("active_quests");
                activeQuests.Add(QuestManager.Instance.CurrentQuest);
                
                QuestSignals.RaiseRefreshQuests();
                
                SaveFacts();
                GameObject.SetActive(false);
            }
        }

        #endregion


        #region Utils

        /* Fonctions privées utiles */
        string RewardsListJoined(List<string> rewards)
        {
            return string.Join(", ", rewards);
        }

        void HandleAddAdventurersFromQuest(AdventurerClass adventurer)
        {
            _adventurersSelected.Add(adventurer);
            Info($"Nouveau aventurier selectionné. {_adventurersSelected.Count} au total.");
        }

        void HandleRemoveAdventurersFromQuest(AdventurerClass adventurer)
        {
            _adventurersSelected.Remove(adventurer);
            Info($"L'aventurier à été retiré. {_adventurersSelected.Count} au total.");
        }

        #endregion


        #region Privates and Protected

        // Variables privées
        [SerializeField] GameObject _buttonActivation;
        [SerializeField] GameObject _adventurersOnThisQuestPanel;
        List<AdventurerClass> _adventurersSelected = new List<AdventurerClass>();

        #endregion
    }
}

