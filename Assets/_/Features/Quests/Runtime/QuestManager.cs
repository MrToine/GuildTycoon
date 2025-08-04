using System.Collections.Generic;
using Adventurer.Runtime;
using UnityEngine;

namespace Quests.Runtime._.Features.Quests.Runtime
{
    public class QuestManager
    {
        private static QuestManager _instance;
        public static QuestManager Instance => _instance ??= new QuestManager();

        private QuestClass _currentQuest;

        public QuestClass CurrentQuest
        {
            get
            {
                return _currentQuest;
            }
            set
            {
                _currentQuest = value;
            }
        }

        public void StartQuest(QuestClass quest, List<AdventurerClass> team)
        {
            if (quest == null)
            {
                Debug.LogWarning("Quest est nulle...");
                return;
            }
            foreach (var adventurer in team)
            {
                adventurer.IsAvailable = false;
                if (quest.AssignedAdventurers == null)
                    quest.AssignedAdventurers = new List<AdventurerClass>();
                quest.AssignedAdventurers.Add(adventurer);
            }
            
            quest.State = QuestStateEnum.Active;
        }

        public void CompleteQuest(QuestClass quest, List<AdventurerClass> team)
        {
            foreach (var adventurer in team)
            {
                adventurer.IsAvailable = true;
            }
            quest.State = QuestStateEnum.Completed;
        }

        public bool CanSelectedAdventurers()
        {
            return _currentQuest != null && _currentQuest.State == QuestStateEnum.Disponible;
        }
    }
}
