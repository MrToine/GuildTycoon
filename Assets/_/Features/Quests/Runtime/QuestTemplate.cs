using System;
using System.Collections.Generic;
using Core.Runtime;
using UnityEngine;

namespace Quests.Runtime
{
    [CreateAssetMenu(fileName = "Quest Template", menuName = "Guild Tycoon/Quests/Template", order = 0)]
    public class QuestTemplate : ScriptableObject
    {
        public QuestClass data;
        public QuestClass ToQuestClass(QuestStateEnum stateQuest)
        {
            List<string> rewardsName = new List<string>();
            foreach (var reward in data.Rewards)
            {
                if (reward == null)
                {
                    Debug.LogWarning("Reward is null in QuestTemplate: " + data.Name);
                    continue;
                }
                if (reward.m_itemDefinition == null)
                {
                    Debug.LogWarning("ItemDefinition is null for a reward in quest: " + data.Name);
                    continue;
                }
                string reward_name_amount =
                    $"{LocalizationSystem.Instance.GetLocalizedText(reward.m_itemDefinition.name)}";
                rewardsName.Add($"{reward_name_amount} x{reward.m_quantity}");
            }
            QuestClass quest = new QuestClass(new Guid(), data.Name, data.Description, data.Objective, data.Duration, data.Difficulty, data.Rewards, data.MinLevel);
            quest.State = stateQuest;
            
            quest.EventPack = data.EventPack;
            quest.InitializeEvents(data.EventPack);
            return quest;
        }
    }
}
