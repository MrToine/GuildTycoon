using System;
using System.Collections.Generic;
using Core.Runtime;
using Item.Runtime;
using UnityEngine;

namespace Quests.Runtime
{
    [CreateAssetMenu(fileName = "Quest Template", menuName = "Guild Tycoon/Quests/Template", order = 0)]
    public class QuestTemplate : ScriptableObject
    {
        public string m_title;
        public string m_description;
        public string m_objective;
        public QuestDifficultyEnum m_difficulty;
        public int m_duration;
        public List<ItemReward> m_rewards;
        public int m_minLevel;
        public QuestClass ToQuestClass()
        {
            List<string> rewardsName = new List<string>();
            foreach (var reward in m_rewards)
            {
                if (reward == null)
                {
                    Debug.LogWarning("Reward is null in QuestTemplate: " + m_title);
                    continue;
                }
                if (reward.m_itemDefinition == null)
                {
                    Debug.LogWarning("ItemDefinition is null for a reward in quest: " + m_title);
                    continue;
                }
                string reward_name_amount =
                    $"{LocalizationSystem.Instance.GetLocalizedText(reward.m_itemDefinition.name)}";
                rewardsName.Add($"{reward_name_amount} x{reward.m_quantity}");
            }
            return new QuestClass(Guid.NewGuid(), m_title, m_description, m_objective, m_duration, m_difficulty, rewardsName, m_minLevel);
        }
    }
}
