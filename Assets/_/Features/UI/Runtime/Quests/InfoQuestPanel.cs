using System;
using System.Collections.Generic;
using System.Linq;
using Adventurer.Runtime;
using Core.Runtime;
using EventSystem.Runtime;
using Item.Runtime;
using Quests.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI.Runtime
{
    public class InfoQuestPanel : BaseMonobehaviour
    {
        #region Publics
        [Header("UI Text Elements")]
        public TextMeshProUGUI m_title;
        public TextMeshProUGUI m_description;
        public TextMeshProUGUI m_objective;
        public TextMeshProUGUI m_reward;
        #endregion

        #region Privates and Protected
        [Header("UI Control Elements")]
        [SerializeField] private GameObject _buttonActivation;
        [SerializeField] private GameObject _buttonRecap;
        [SerializeField] private GameObject _adventurersOnThisQuestPanel;
        [SerializeField] private GameObject _adventurersSelection;

        private List<AdventurerClass> _adventurersSelected = new List<AdventurerClass>();
        private Button _activationButton;
        private TMP_Text _activationButtonText;
        #endregion

        #region Unity API
        private void Start()
        {
            InitializeUI();
            RegisterEventHandlers();
            LocalizeTexts();
        }

        private void OnDestroy()
        {
            UnregisterEventHandlers();
        }

        private void Update()
        {
            UpdateActivationButtonState();
        }
        #endregion

        #region Initialization
        private void InitializeUI()
        {
            _activationButton = _buttonActivation.GetComponent<Button>();
            _activationButtonText = _buttonActivation.GetComponentInChildren<TMP_Text>();
        }

        private void RegisterEventHandlers()
        {
            AdventurerSignals.OnAdventurerSelected += HandleAddAdventurersFromQuest;
            AdventurerSignals.OnAdventurerUnselected += HandleRemoveAdventurersFromQuest;
        }

        private void UnregisterEventHandlers()
        {
            AdventurerSignals.OnAdventurerSelected -= HandleAddAdventurersFromQuest;
            AdventurerSignals.OnAdventurerUnselected -= HandleRemoveAdventurersFromQuest;
        }

        private void LocalizeTexts()
        {
            foreach (var txt in GetComponentsInChildren<TMP_Text>())
            {
                txt.text = LocalizationSystem.Instance.GetLocalizedText(txt.text);
            }
        }
        #endregion

        #region UI Updates
        private void UpdateActivationButtonState()
        {
            bool hasAdventurers = _adventurersSelected.Count >= 1;
            _activationButton.interactable = hasAdventurers;
            _activationButtonText.color = hasAdventurers ? Color.yellow : Color.grey;
        }

        private void ConfigureUIForQuestState(QuestStateEnum state)
        {
            switch (state)
            {
                case QuestStateEnum.Disponible:
                    _buttonActivation.SetActive(true);
                    _adventurersOnThisQuestPanel.SetActive(false);
                    _buttonRecap.SetActive(false);
                    break;
                case QuestStateEnum.Completed:
                    _buttonActivation.SetActive(false);
                    _adventurersOnThisQuestPanel.SetActive(false);
                    _adventurersSelection.SetActive(false);
                    _buttonRecap.SetActive(true);
                    break;
                case QuestStateEnum.Active:
                    Info("La quête est active.");
                    _buttonActivation.SetActive(false);
                    _adventurersOnThisQuestPanel.SetActive(true);
                    _adventurersSelection.SetActive(false);
                    _buttonRecap.SetActive(false);
                    break;
                default:
                    _buttonActivation.SetActive(false);
                    _adventurersOnThisQuestPanel.SetActive(true);
                    _buttonRecap.SetActive(false);
                    break;
            }
        }
        #endregion

        #region Main Methods
        public void ShowInfo(QuestClass quest)
        {
            quest = FindMatchingActiveQuest(quest);
            QuestManager.Instance.CurrentQuest = quest;

            UpdateQuestInfoDisplay(quest);
            ConfigureUIForQuestState(quest.State);
        }

        public void LaunchQuest()
        {
            if (_adventurersSelected.Count <= 0) return;

            StartQuestWithSelectedAdventurers();
            CleanupAfterQuestLaunch();
        }
        #endregion

        #region Quest Operations
        private QuestClass FindMatchingActiveQuest(QuestClass quest)
        {
            if (!FactExists<List<QuestClass>>("active_quests", out _)) return quest;

            var activeQuests = GetFact<List<QuestClass>>("active_quests");
            var matchingQuest = activeQuests.FirstOrDefault(q => q.ID == quest.ID);
            return matchingQuest ?? quest;
        }

        private void UpdateQuestInfoDisplay(QuestClass quest)
        {
            m_title.text = LocalizationSystem.Instance.GetLocalizedText(quest?.Name);
            string descKey = quest?.Description;
            if (string.IsNullOrEmpty(descKey))
            {
                Debug.LogWarning($"🧨 Clé de localisation vide ou nulle pour la quête ! ({quest.Description})");
            }
            m_description.text = LocalizationSystem.Instance.GetLocalizedText(descKey);
            m_objective.text = LocalizationSystem.Instance.GetLocalizedText(quest?.Objective);
            m_reward.text = FormatRewardsList(quest.Rewards);
        }

        private void StartQuestWithSelectedAdventurers()
        {
            var gameTime = GetFact<GameTime>("game_time");
            QuestManager.Instance.StartQuest(QuestManager.Instance.CurrentQuest, _adventurersSelected, gameTime);

            // Ajouter la quête à la liste des quêtes actives
            var activeQuests = GetFact<List<QuestClass>>("active_quests");
            activeQuests.Add(QuestManager.Instance.CurrentQuest);
        }

        private void CleanupAfterQuestLaunch()
        {
            _adventurersSelected.Clear();
            AdventurerSignals.RaiseRefreshAdventurers();
            QuestSignals.RaiseRefreshQuests();
            gameObject.SetActive(false);
        }
        #endregion

        #region Utils
        private string FormatRewardsList(List<ItemReward> rewards)
        {
            return string.Join(", ", rewards.Select(r => {
                if (r.m_itemDefinition.DisplayNameKey == null) return "???";
                return LocalizationSystem.Instance.GetLocalizedText(r.m_itemDefinition.DisplayNameKey);
            }));
        }

        private void HandleAddAdventurersFromQuest(AdventurerClass adventurer)
        {
            _adventurersSelected.Add(adventurer);
            Debug.Log($"Aventurier sélectionné: {adventurer.Name}. Total: {_adventurersSelected.Count}");
        }

        private void HandleRemoveAdventurersFromQuest(AdventurerClass adventurer)
        {
            _adventurersSelected.Remove(adventurer);
            Debug.Log($"Aventurier retiré: {adventurer.Name}. Total: {_adventurersSelected.Count}");
        }
        #endregion
    }
}

