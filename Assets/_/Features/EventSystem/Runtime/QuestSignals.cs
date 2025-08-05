using System;
using Quests.Runtime;

namespace EventSystem.Runtime
{
    public static class QuestSignals
    {
        public static event Action<QuestClass> OnInfoQuestPanel;
        public static event Action OnRefresh;
        
        public static void RaiseInfoQuestPanel(QuestClass questClass)
        {
            OnInfoQuestPanel?.Invoke(questClass);
        }
        
        public static void RaiseRefreshQuests()
        {
            OnRefresh?.Invoke();
        }
    }
}
