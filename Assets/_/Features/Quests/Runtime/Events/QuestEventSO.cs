using UnityEngine;

namespace Quests.Runtime
{
    [CreateAssetMenu(fileName = "QuestEventSO", menuName = "Guild Tycoon/Quests/Event", order = 0)]
    public class QuestEventSO : ScriptableObject
    {
        public QuestEvent m_data;

        public QuestEvent ToRuntime()
        {
            return m_data;
        }
    }
}
