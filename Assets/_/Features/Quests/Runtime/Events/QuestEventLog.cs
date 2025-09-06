using System;
using System.Collections.Generic;

namespace Quests.Runtime
{
    [Serializable]
    public class QuestEventLog
    {
        public int Time;
        public Guid EventId;
        public QuestEventLog(int time, Guid eventId)
        {
            Time = time;
            EventId = eventId;
        }
    }
}
