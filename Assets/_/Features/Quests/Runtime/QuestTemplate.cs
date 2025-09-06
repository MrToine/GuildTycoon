using System;
using System.Collections.Generic;
using Core.Runtime;
using UnityEngine;

namespace Quests.Runtime
{
    [CreateAssetMenu(fileName = "Quest Template", menuName = "Guild Tycoon/Quests/Template", order = 0)]
    public class QuestTemplate : ScriptableObject
    {
        public string m_assetGuid;
        public QuestClass data;
        public QuestClass ToQuestClass(QuestStateEnum stateQuest, Guid? id = null)
        {
            List<string> rewardsName = new List<string>();
            Guid questId = id.HasValue ? id.Value : Guid.NewGuid();
            foreach (var reward in data.Rewards)
            {
                if (reward == null)
                {
                    continue;
                }
                if (reward.m_itemDefinition == null)
                {
                    continue;
                }
                string reward_name_amount =
                    $"{LocalizationSystem.Instance.GetLocalizedText(reward.m_itemDefinition.name)}";
                rewardsName.Add($"{reward_name_amount} x{reward.m_quantity}");
            }
            QuestClass quest = new QuestClass(questId, data.Name, data.Description, data.Objective, data.Duration, data.Difficulty, data.Rewards, data.MinLevel);
            quest.State = stateQuest;
            
            quest.EventPack = data.EventPack;
            quest.InitializeEvents(data.EventPack);
            return quest;
        }
        
        #if UNITY_EDITOR
        private void OnValidate()
        {
            Guid tempGuid;
            if (string.IsNullOrEmpty(m_assetGuid) || !Guid.TryParse(m_assetGuid, out tempGuid))
            {
                m_assetGuid = Guid.NewGuid().ToString();
                UnityEditor.EditorUtility.SetDirty(this);
            }
        }
        #endif
    }
}
