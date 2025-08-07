using System.Collections.Generic;
using UnityEngine;

namespace Quests.Runtime
{
    [CreateAssetMenu(fileName = "QuestEventPack", menuName = "Guild Tycoon/Quests/EventPack", order = 0)]
    public class QuestEventPackSO : ScriptableObject
    {
        
        public List<QuestEventSO> availableEvents;
        public int maxEventsToPick = 2;
    }
}
