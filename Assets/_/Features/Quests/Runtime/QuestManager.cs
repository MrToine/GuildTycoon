using System;
using System.Collections.Generic;
using System.Linq;
using Adventurer.Runtime;
using Core.Runtime;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Quests.Runtime
{
    public class QuestManager: BaseMonobehaviour
    {
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

        public List<QuestClass> CompletedQuests
        {
            get
            {
                return _completedQuests;
            }
            set
            {
                _completedQuests = value;
            }
        }

        public List<Guid> AssignedAdventurers
        {
            get
            {
                return _currentQuest.AssignedAdventurersID;;
            }
        }
        
        public static event Action<QuestClass> OnQuestCompleted;
        public static event Action<QuestEvent> OnEventReceived;
        public static event Action<QuestClass> OnEventFromQuest;
        
        public QuestManager()
        {
            GameManager.OnTimeAdvanced += CheckMissionsProgress;
        }

        public void StartQuest(QuestClass quest, List<AdventurerClass> team, GameTime gameTime)
        {
            foreach (var adventurer in team)
            {
                adventurer.IsAvailable = false;
                if (quest.AssignedAdventurersID == null)
                    quest.AssignedAdventurersID = new List<Guid>();
                quest.AssignedAdventurersID.Add(adventurer.ID);
            }
    
            quest.State = QuestStateEnum.Active;
            quest.StartSeconds = gameTime.TotalSeconds;
            quest.EndSeconds = gameTime.TotalSeconds + (quest.Duration * 60);

            _activeQuests.Add(quest);
        }
        
        void CheckMissionsProgress(int currentSeconds)
        {
            if(_activeQuests == null) return;
            var questsToComplete = new List<QuestClass>();
            foreach (var quest in _activeQuests.Where(q => q.State == QuestStateEnum.Active).ToList())
            { 
                int snapTime = currentSeconds - quest.StartSeconds;
                //Debug.Log($"🎯 {quest.Name} | State: {quest.State} | current: {currentSeconds} / end: {quest.EndSeconds}");
                foreach (var questEvent in quest.ActiveEvents)
                {
                    if (quest.TriggeredEventsDescriptionKeys.Contains(questEvent.DescriptionKey)) continue;
                    
                    if (snapTime >= questEvent.MinTimeTrigger && snapTime <= questEvent.MaxTimeTrigger)
                    {
                        if (Random.Range(0f, 100f) <= questEvent.PercentTrigger)
                        {
                            TriggerEvent(questEvent, quest);
                            quest.TriggeredEventsDescriptionKeys.Add(questEvent.DescriptionKey);
                        }
                    }
                }
                
                if (quest.State == QuestStateEnum.Active && currentSeconds >= quest.EndSeconds)
                {
                    questsToComplete.Add(quest);
                }
            }
            foreach (var quest in questsToComplete)
            {
                CompleteQuest(quest, quest.AssignedAdventurersID);
                Debug.Log($"✅ Quête terminée : {quest.Name}");
            }
        }

        void TriggerEvent(QuestEvent questEvent, QuestClass quest)
        {
            List<Guid> assignedAdventurersId = quest.AssignedAdventurersID;
            
            string description = LocalizationSystem.Instance.GetLocalizedText(questEvent.DescriptionKey);
            Debug.LogWarning($"🎯[{quest.Name}] Un Event survint : {description}");
            OnEventReceived?.Invoke(questEvent);
            OnEventFromQuest?.Invoke(quest);
            
            var targets = questEvent.GetTargets(assignedAdventurersId);
            ApplyEffect(questEvent.Effects, targets);
        }

        void ApplyEffect(List<EventEffect> effects, List<AdventurerClass> assignedAdventurers)
        {
            foreach (var effect in effects)
            {
                foreach (var target in assignedAdventurers)
                {
                    switch (effect.Type)
                    {
                        case EffectType.Damage:
                            target.TakeDamage(effect.Value);
                            break;
                        case EffectType.Heal:
                            target.Heal(effect.Value);
                            break;
                        case EffectType.Buff:
                            target.ApplyBuff(effect.Value);
                            break;
                    }
                        
                }
            }
        }

        public void CompleteQuest(QuestClass quest, List<Guid> team)
        {
            Debug.Log("🥊 Complétion de quete");
            if (quest.State != QuestStateEnum.Active) return;
            foreach (var adventurerId in team)
            {
                AdventurerClass adventurer = QuestClass.GetOneAdventurerFromId(adventurerId);
                adventurer.IsAvailable = true;
            }
            quest.State = QuestStateEnum.Completed;
            _activeQuests.RemoveAll(q => q.Name == quest.Name);
            _completedQuests.Add(quest);
            
            Dictionary<string, QuestStateEnum> quests = GetFact<Dictionary<string, QuestStateEnum>>("quests");
            if (quests.ContainsKey(quest.Name))
            {
                quests[quest.Name] = QuestStateEnum.Completed;
            }
            NotifyCompletedQuests();
            SaveFacts();
        }
        
        public void NotifyCompletedQuests()
        {
            foreach (var quest in _completedQuests)
            {
                Debug.Log($"⛳️ La quête est finie : {quest.Name}");
                OnQuestCompleted?.Invoke(quest);
            }
        }

        public bool CanSelectedAdventurers()
        {
            return _currentQuest != null && _currentQuest.State == QuestStateEnum.Disponible;
        }
        
        public bool IsQuestCompleted(string questName)
        {
            return ActiveQuests != null 
                   && ActiveQuests.Any(q => q.Name == questName && q.State == QuestStateEnum.Completed);
        }
        
        List<QuestEvent> inActiveEvents;
        static QuestManager _instance;
        List<QuestClass> _activeQuests;
        List<QuestClass> _completedQuests;
    }
}
