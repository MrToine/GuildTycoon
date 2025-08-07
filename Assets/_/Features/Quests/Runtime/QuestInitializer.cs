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
            if (!FactExists<List<QuestClass>>("active_quests", out var _))
            {
                SetFact<List<QuestClass>>("active_quests", new List<QuestClass>(), FactPersistence.Persistent);
            }
            QuestManager.Instance.ActiveQuests = GetFact<List<QuestClass>>("active_quests");
            if (!FactExists<List<QuestClass>>("active_quests", out var _))
            {
                SetFact<List<QuestClass>>("completed_quests", new List<QuestClass>(), FactPersistence.Persistent);
            }
            QuestManager.Instance.CompletedQuests = GetFact<List<QuestClass>>("completed_quests");
            Invoke(nameof(NotifyLater), 0.1f);
        }

        void NotifyLater()
        {
            QuestManager.Instance.NotifyCompletedQuests();
        }
    }
}

