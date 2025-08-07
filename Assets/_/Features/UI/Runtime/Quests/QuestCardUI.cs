using System.Collections.Generic;
using System.Linq;
using Core.Runtime;
using EventSystem.Runtime;
using Item.Runtime;
using Quests.Runtime;
using TMPro;
using UnityEngine;

namespace GameUI.Runtime
{
    public class QuestCardUI : BaseMonobehaviour
    {

        #region Publics

        [Header("UI References")]
        public TMPro.TextMeshProUGUI m_title;
        public TMPro.TextMeshProUGUI m_description;
        public TMPro.TextMeshProUGUI m_objective;
        public TMPro.TextMeshProUGUI m_duration;
        public TMPro.TextMeshProUGUI m_difficulty;
        public TMPro.TextMeshProUGUI m_reward;
        public TMPro.TextMeshProUGUI m_minLevel;

        public QuestClass Quest => _quest;
        
        #endregion


        #region Unity API

        void Start()
        {
            foreach (var txt in GetComponentsInChildren<TMP_Text>())
            {
                txt.text = LocalizationSystem.Instance.GetLocalizedText(txt.text);
            }
        }

        #endregion


        #region Main Methods

        public void Setup(QuestClass quest)
        {
            _quest = quest;
            m_title.text = quest.Name;
            m_description.text = quest.Description;
            m_objective.text = quest.Objective;
            //m_duration.text = quest.Duration.ToString();
            m_difficulty.text = quest.Difficulty.ToString();
            m_reward.text = RewardsListJoined(quest.Rewards);
            m_minLevel.text = quest.MinLevel.ToString();
        }

        public void AcceptQuest()
        {
            Info($"Le joueur à accepté la quête {_quest.Name}");
            Dictionary<string, QuestStateEnum> quests = GetFact<Dictionary<string, QuestStateEnum>>("quests");
            quests.Add(_quest.Name, QuestStateEnum.Disponible);
            SaveFacts();
            QuestSignals.RaiseRefreshQuests();
            Destroy(gameObject); 
        }

        #endregion


        #region Utils

        string RewardsListJoined(List<ItemReward> rewards)
        {
            return string.Join(", ", rewards.Select(r => r.m_itemDefinition.DisplayNameKey != null
                ? LocalizationSystem.Instance.GetLocalizedText(r.m_itemDefinition.DisplayNameKey)
                : "???"));
        }

        #endregion


        #region Privates and Protected

        QuestClass _quest;

        #endregion
    }
}
