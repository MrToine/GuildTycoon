using System;
using System.Collections.Generic;
using System.Linq;
using Adventurer.Runtime;
using Core.Runtime;
using Quest.Runtime;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Quests.Runtime
{
    public class QuestManager : BaseMonobehaviour
    {
        #region Singleton

        static QuestManager _instance;
        public static QuestManager Instance => _instance ??= new QuestManager();

        #endregion

        #region Events

        public static event Action<QuestClass> OnQuestCompleted;
        public static event Action<QuestEvent> OnEventReceived;
        public static event Action<QuestClass> OnEventFromQuest;

        #endregion

        #region Properties

         QuestClass _currentQuest;
        public QuestClass CurrentQuest
        {
            get => _currentQuest;
            set => _currentQuest = value;
        }

         List<QuestClass> _activeQuests;
        public List<QuestClass> ActiveQuests
        {
            get => _activeQuests;
            set => _activeQuests = value;
        }

         List<QuestClass> _completedQuests;
        public List<QuestClass> CompletedQuests
        {
            get => _completedQuests;
            set => _completedQuests = value;
        }

        public List<Guid> AssignedAdventurers => _currentQuest?.AssignedAdventurersID;

        [SerializeField]  QuestFactoryDatabase _questDatabase;
        public QuestFactoryDatabase QuestDatabase
        {
            get => _questDatabase;
            set => _questDatabase = value;
        }

        public int currentTimeInQuest => _snapTime;
        int _snapTime = 0;
        List<QuestEvent> _activeEvents;

        // Champ inutilisé - conservé pour référence future
         List<QuestEvent> inActiveEvents;

        #endregion

        #region Initialization

        public QuestManager()
        {
            GameManager.OnTimeAdvanced += CheckMissionsProgress;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Démarre une quête avec une équipe d'aventuriers
        /// </summary>
        public void StartQuest(QuestClass quest, List<AdventurerClass> team, GameTime gameTime)
        {
            AssignAdventurersToQuest(quest, team);
            SetQuestTimings(quest, gameTime);
            UpdateQuestStatus(quest);
        }

        /// <summary>
        /// Complète une quête et libère les aventuriers assignés
        /// </summary>
        public void CompleteQuest(QuestClass quest, List<Guid> team)
        {
            if (quest.State != QuestStateEnum.Active) return;

            ReleaseAdventurers(team);
            UpdateQuestCompletionStatus(quest);
            SaveQuestProgress();
        }

        /// <summary>
        /// Notifie les observateurs des quêtes complétées
        /// </summary>
        public void NotifyCompletedQuests()
        {
            foreach (var quest in _completedQuests)
            {
                OnQuestCompleted?.Invoke(quest);
            }
        }

        /// <summary>
        /// Récupère l'historique des events lié à une quête
        /// </summary>
        public QuestSummary GetQuestHistory(Guid questId)
        {
            QuestClass quest = GetQuestById(questId);

            List<QuestEventLog> events = GetFact<Dictionary<Guid, List<QuestEventLog>>>("events_quests_history")[questId];
            
            return new QuestSummary(quest, events);
        }

        /// <summary>
        /// Vérifie si des aventuriers peuvent être sélectionnés pour la quête courante
        /// </summary>
        public bool CanSelectedAdventurers()
        {
            return _currentQuest != null && _currentQuest.State == QuestStateEnum.Disponible;
        }

        /// <summary>
        /// Vérifie si une quête est complétée par son nom
        /// </summary>
        public bool IsQuestCompleted(string questName)
        {
            return ActiveQuests != null 
                   && ActiveQuests.Any(q => q.Name == questName && q.State == QuestStateEnum.Completed);
        }

        /// <summary>
        /// Résout une liste de quêtes à partir des données sauvegardées
        /// </summary>
        public List<QuestClass> ResolveQuestsList(List<QuestClass> questsFromSave)
        {
            List<QuestClass> quests = new List<QuestClass>();
            foreach (var quest in questsFromSave)
            {
                QuestTemplate template = _questDatabase.GetTemplatesByName(quest.Name);
                if (template == null) continue;
                quests.Add(template.ToQuestClass(quest.State, quest.ID));
            }
            return quests;
        }

        #endregion

        #region  Methods

        /// <summary>
        /// Assigne des aventuriers à une quête
        /// </summary>
       void AssignAdventurersToQuest(QuestClass quest, List<AdventurerClass> team)
        {
            foreach (var adventurer in team)
            {
                adventurer.IsAvailable = false;
                if (quest.AssignedAdventurersID == null)
                    quest.AssignedAdventurersID = new List<Guid>();
                quest.AssignedAdventurersID.Add(adventurer.ID);
            }
        }

        /// <summary>
        /// Configure les temps de début et de fin d'une quête
        /// </summary>
       void SetQuestTimings(QuestClass quest, GameTime gameTime)
        {
            quest.State = QuestStateEnum.Active;
            quest.StartSeconds = gameTime.TotalSeconds;
            quest.EndSeconds = gameTime.TotalSeconds + (quest.Duration * 60);
        }

        /// <summary>
        /// Met à jour le statut d'une quête dans les listes actives et sauvegardées
        /// </summary>
       void UpdateQuestStatus(QuestClass quest)
        {
            _activeQuests.Add(quest);
            List<QuestClass> saveQuests = GetFact<List<QuestClass>>("quests");
            foreach (var saveQuest in saveQuests)
            {
                if (saveQuest.Name == quest.Name)
                {
                    saveQuest.State = QuestStateEnum.Active;
                    saveQuest.StartSeconds = quest.StartSeconds;
                    saveQuest.EndSeconds = quest.EndSeconds;
                }
            }
            SaveFacts();
        }

        /// <summary>
        /// Libère les aventuriers assignés à une quête
        /// </summary>
       void ReleaseAdventurers(List<Guid> team)
        {
            foreach (var adventurerId in team)
            {
                AdventurerClass adventurer = QuestClass.GetOneAdventurerFromId(adventurerId);
                if (adventurer != null)
                    adventurer.IsAvailable = true;
            }
        }

        /// <summary>
        /// Met à jour le statut de complétion d'une quête
        /// </summary>
       void UpdateQuestCompletionStatus(QuestClass quest)
        {
            quest.State = QuestStateEnum.Completed;
            _activeQuests.RemoveAll(q => q.Name == quest.Name);
            _completedQuests.Add(quest);

            List<QuestClass> quests = GetFact<List<QuestClass>>("quests");
            QuestClass questToUpdate = quests.FirstOrDefault(q => q.ID == quest.ID);
            if (questToUpdate != null)
            {
                questToUpdate.State = QuestStateEnum.Completed;
            }

            NotifyCompletedQuests();
        }

        /// <summary>
        /// Sauvegarde les progrès des quêtes
        /// </summary>
       void SaveQuestProgress()
        {
            SaveFacts();
        }

        /// <summary>
        /// Vérifie la progression des missions actives
        /// </summary>
       void CheckMissionsProgress(int currentSeconds)
        {
            if(_activeQuests == null) return;

            var questsToComplete = new List<QuestClass>();
            var activeQuests = _activeQuests.Where(q => q.State == QuestStateEnum.Active).ToList();

            foreach (var quest in activeQuests)
            { 
                _snapTime = currentSeconds - quest.StartSeconds;
                CheckQuestEvents(quest, currentSeconds);

                if (quest.State == QuestStateEnum.Active && currentSeconds >= quest.EndSeconds)
                {
                    questsToComplete.Add(quest);
                }
            }

            foreach (var quest in questsToComplete)
            {
                CompleteQuest(quest, quest.AssignedAdventurersID);
            }
        }

        /// <summary>
        /// Vérifie et déclenche les événements de quête lorsque les conditions sont remplies
        /// </summary>
       void CheckQuestEvents(QuestClass quest, int currentSeconds)
        {
            foreach (var questEvent in quest.ActiveEvents)
            {
                if (quest.TriggeredEventsDescriptionKeys.Contains(questEvent.DescriptionKey)) 
                    continue;

                if (_snapTime >= questEvent.MinTimeTrigger && _snapTime <= questEvent.MaxTimeTrigger)
                {
                    if (Random.Range(0f, 100f) <= questEvent.PercentTrigger)
                    {
                        TriggerEvent(questEvent, quest);
                        quest.TriggeredEventsDescriptionKeys.Add(questEvent.DescriptionKey);
                    }
                }
            }
        }

        /// <summary>
        /// Déclenche un événement de quête
        /// </summary>
       void TriggerEvent(QuestEvent questEvent, QuestClass quest)
        {
            questEvent.Time = _snapTime;
            OnEventReceived?.Invoke(questEvent);
            OnEventFromQuest?.Invoke(quest);

            var targets = questEvent.GetTargets(quest.AssignedAdventurersID);
            ApplyEffect(questEvent.Effects, targets);
            Dictionary<Guid, List<QuestEventLog>> events = GetFact<Dictionary<Guid, List<QuestEventLog>>>("events_quests_history");
            
            if(!events.ContainsKey(quest.ID))
            {
                events.Add(quest.ID, new List<QuestEventLog>());
            }

            QuestEventLog questEventLog = new QuestEventLog(_snapTime,  questEvent.Id);
            
            events[quest.ID].Add(questEventLog);
            _activeEvents.Add(questEvent);
            SaveFacts();
        }

        /// <summary>
        /// Applique les effets aux aventuriers ciblés
        /// </summary>
       void ApplyEffect(List<EventEffect> effects, List<AdventurerClass> targets)
        {
            foreach (var effect in effects)
            {
                foreach (var target in targets)
                {
                    ApplyEffectToTarget(effect, target);
                }
            }
        }

        /// <summary>
        /// Applique un effet spécifique à un aventurier cible
        /// </summary>
      void ApplyEffectToTarget(EventEffect effect, AdventurerClass target)
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
        
        /// <summary>
        /// Récupère une quête via son ID
        /// </summary>
        /// <param name="questId"></param>
        /// <returns></returns>
        QuestClass GetQuestById(Guid questId)
        {
            QuestClass quest = _activeQuests.FirstOrDefault(q => q.ID == questId);
            
            if(quest != null)
                return quest;
            
            quest = _completedQuests.FirstOrDefault(q => q.ID == questId);
            
            if(quest != null)
                return quest;

            foreach (var factory in _questDatabase.GetAll())
            {
                foreach (var template in factory.questTemplates)
                {
                    if (Guid.TryParse(template.m_assetGuid, out Guid guid) && guid == questId)
                    {
                        return template.ToQuestClass(QuestStateEnum.Disponible, questId);
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Récupère un event via son ID
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns>QuestEvent</returns>
        public QuestEvent GetEventById(Guid eventId)
        {
            if (_activeEvents == null) return null;
            return _activeEvents.FirstOrDefault(e => e.Id == eventId);
        }

        #endregion
    }
}
