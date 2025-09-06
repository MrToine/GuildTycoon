using System;
using System.Collections.Generic;

namespace Quests.Runtime
{
    [Serializable]
    public class QuestSummary
    {
        QuestClass _quest;
        List<QuestEventLog> _events;

        public QuestClass Quest
        {
            get { return _quest; }
            set { _quest = value; }
        }

        public List<QuestEventLog> Events
        {
            get { return _events; }
            set { _events = value; }
        }

        public QuestSummary(QuestClass quest, List<QuestEventLog> events)
        {
            _quest = quest;
            _events = events;
        }
    }
}

