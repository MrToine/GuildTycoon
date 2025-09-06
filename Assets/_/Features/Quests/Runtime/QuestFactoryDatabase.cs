using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Quests.Runtime;
using UnityEngine;
using UnityEngine.Serialization;

namespace Quest.Runtime
{
    [CreateAssetMenu(fileName = "Database", menuName = "Guild Tycoon/Quests/FactoryDatabase", order = 0)]
    public class QuestFactoryDatabase : ScriptableObject
    {
        [FormerlySerializedAs("factories")]
        public List<QuestFactorySO> m_factories;

        public QuestFactorySO GetFactoryForLevel(int guildLevel)
        {
            QuestFactorySO factory = m_factories
                .Where(factory => factory.questTemplates.Any(template => template.data.MinLevel <= guildLevel))
                .OrderBy(f => Random.value)
                .FirstOrDefault();
            
            return factory;
        }

        public QuestTemplate GetTemplatesByName(string name)
        {
            foreach (var factory in m_factories)
            {
                foreach (var template in factory.questTemplates)
                {
                    if (template.data.Name == name)
                    {
                        return template;
                    }
                }
            }
            return null;
        }

        public List<QuestFactorySO> GetAll()
        {
            return m_factories;
        }
    }
}
