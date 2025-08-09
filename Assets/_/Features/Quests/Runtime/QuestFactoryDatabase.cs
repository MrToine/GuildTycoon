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
            return m_factories
                .Where(factory => factory.questTemplates.Any(template => template.data.MinLevel <= guildLevel))
                .OrderBy(f => Random.value)
                .FirstOrDefault();
        }

        public QuestTemplate GetTemplatesByName(string name)
        {
            Debug.Log($"On Cherche la quête avec le nom : {name}");
            foreach (var factory in m_factories)
            {
                Debug.Log("⏱️ On parcours la liste des templates de la factory");
                foreach (var template in factory.questTemplates)
                {
                    Debug.Log($"🤖On vérifie que ça matche entre");
                    Debug.Log($"💾 La save: {name}");
                    Debug.Log($"💻 La factory: {template.data.Name}");
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
