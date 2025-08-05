using System.Collections.Generic;
using Core.Runtime;

namespace Quests.Runtime._.Features.Quests
{
    public class QuestInitializer : BaseMonobehaviour
    {
        void Awake()
        {
            var _ = QuestManager.Instance;
        }

        void Start()
        {
            QuestManager.Instance.ActiveQuests = GetFact<List<QuestClass>>("active_quests");
        }
    }
}

