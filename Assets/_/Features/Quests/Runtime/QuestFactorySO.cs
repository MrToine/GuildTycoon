using System.Collections.Generic;
using UnityEngine;

namespace Quests.Runtime
{
    [CreateAssetMenu(fileName = "Quest Factory", menuName = "Guild Tycoon/Quests/Factory", order = 0)]
    public class QuestFactorySO : ScriptableObject
    {
        public List<QuestTemplate> questTemplates;

        public QuestClass GenerateRandomQuest()
        {
            var randomTemplate = questTemplates[Random.Range(0, questTemplates.Count)];
            return randomTemplate.ToQuestClass(QuestStateEnum.Disponible);
        }
    }
}
