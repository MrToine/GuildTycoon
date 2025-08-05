using System;
using System.Collections.Generic;
using Adventurer.Runtime;
using Core.Runtime;
using UnityEngine;

namespace Quests.Runtime
{
    public class QuestManager
    {
        private static QuestManager _instance;
        List<QuestClass> _activeQuests;
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

        public List<QuestClass> ActiveQuests
        {
            get
            {
                return _activeQuests;
            }
            set
            {
                _activeQuests = value;
            }
        }

        public List<AdventurerClass> AssignedAdventurers
        {
            get
            {
                Debug.Log($"{_currentQuest.AssignedAdventurers.Count} aventuriers lié a la quête");
                return _currentQuest.AssignedAdventurers;;
            }
        }
        
        public static event Action<QuestClass> OnQuestCompleted;

        public QuestManager()
        {
            GameManager.OnTimeAdvanced += CheckMissionsProgress;
        }

        public void StartQuest(QuestClass quest, List<AdventurerClass> team, GameTime gameTime)
        {
            if (quest == null)
            {
                Debug.LogWarning("Quest est nulle...");
                return;
            }

            if (team.Count == 0)
            {
                Debug.LogWarning("0 aventuriers pour la quête !");
            }
            
            foreach (var adventurer in team)
            {
                Debug.LogWarning($"Via StartQuest() il y a {adventurer.Name} qui est lié.");
                adventurer.IsAvailable = false;
                if (quest.AssignedAdventurers == null)
                    quest.AssignedAdventurers = new List<AdventurerClass>();
                quest.AssignedAdventurers.Add(adventurer);
            }

            quest.State = QuestStateEnum.Active;
            quest.EndGameSeconds = gameTime.TotalSeconds + (quest.Duration * 60);
        }
        
        void CheckMissionsProgress(int currentSeconds)
        {
            Debug.LogWarning($"Check mission. Time : {currentSeconds}");
            if(_activeQuests == null) return;
            foreach (var quest in _activeQuests)
            {
                if (quest.State == QuestStateEnum.Active && currentSeconds >= quest.EndGameSeconds)
                {
                    CompleteQuest(quest, quest.AssignedAdventurers);
                    Debug.Log($"✅ Quête terminée : {quest.Name}");
                }
            }
        }

        public void CompleteQuest(QuestClass quest, List<AdventurerClass> team)
        {
            foreach (var adventurer in team)
            {
                adventurer.IsAvailable = true;
            }
            quest.State = QuestStateEnum.Completed;
            OnQuestCompleted?.Invoke(quest);
        }

        public bool CanSelectedAdventurers()
        {
            return _currentQuest != null && _currentQuest.State == QuestStateEnum.Disponible;
        }
    }
}
