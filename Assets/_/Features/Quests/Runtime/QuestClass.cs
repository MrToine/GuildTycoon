using System;
using System.Collections.Generic;
using Adventurer.Runtime;
using Core.Runtime;
using Item.Runtime;
using UnityEngine;

namespace Quests.Runtime
{
    [Serializable]
    public class QuestClass
    {
        #region Getters and Setters

        public Guid ID
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        
        public string Objective
        {
            get { return _objective; }
            set { _objective = value; }
        }

        public int Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        public QuestDifficultyEnum Difficulty
        {
            get { return _difficulty; }
            set { _difficulty = value; }
        }

        public List<ItemReward> Rewards
        {
            get { return _rewards; }
            set { _rewards = new List<ItemReward>(); }
        }

        public QuestStateEnum State
        {
            get { return _state; }
            set { _state = value; }
        }
        
        public int MinLevel
        {
            get { return _minLevel; }
            set { _minLevel = value; }
        }

        public List<Guid> AssignedAdventurersID
        {
            get
            {
                return _assignedAdventurersID;
            }
            set
            {
                _assignedAdventurersID = value;
            }
        }
        
        public int StartSeconds
        {
            get
            {
                return _StartSeconds;
            }
            set
            {
                _StartSeconds = value;
            }
        }

        public int EndSeconds
        {
            get
            {
                return _endSeconds;
            }
            set
            {
                _endSeconds = value;
            }
        }

        public QuestEventPackSO EventPack
        {
            get
            {
                return _eventPack;
            }
            set
            {
                _eventPack = value;
            }
        }

        public List<QuestEvent> ActiveEvents => _activeEvents;
        public List<string> TriggeredEventsDescriptionKeys = new List<string>();
        
        #endregion
        
        #region Parameters

        [SerializeField]  string _name;
        [SerializeField]  string _description;
        [SerializeField]  string _objective;
        [SerializeField]  int _duration;
        [SerializeField]  QuestDifficultyEnum _difficulty;
        [SerializeField]  List<ItemReward> _rewards;
        [SerializeField]  int _minLevel; 
        [SerializeField]  QuestEventPackSO _eventPack;
        
        Guid _id;
        QuestStateEnum _state;
        int _StartSeconds;
        int _endSeconds;
        List<Guid> _assignedAdventurersID = new List<Guid>();
        List<QuestEvent> _activeEvents = new List<QuestEvent>();
        
        #endregion
        
        #region Constructor

        public QuestClass(Guid id, string name, string description, string objective, int duration, QuestDifficultyEnum difficulty, List<ItemReward> reward, int minLevel = 1)
        {
            _id = id;
            _name = name;
            _description = description;
            _objective = objective;
            _duration = duration;
            _difficulty = difficulty;
            _rewards = reward;
            _minLevel = minLevel;
            _state = QuestStateEnum.Disponible;
        }

        #endregion
        
        #region Methods

        public void InitializeEvents(QuestEventPackSO pack)
        {
            ActiveEvents.Clear();
            
            var pool = new List<QuestEventSO>(pack.availableEvents);
            int toPick = Mathf.Min(pack.maxEventsToPick, pool.Count);

            for (int i = 0; i < toPick; i++)
            {
                int index = UnityEngine.Random.Range(0, pool.Count);
                var pickedSO = pool[index];
                pool.RemoveAt(index);

                ActiveEvents.Add(pickedSO.ToRuntime()); 
            }
        }
        
        public static List<AdventurerClass> GetAdventurersFromId(List<Guid> adventurersId)
        {
            var adventurers = new List<AdventurerClass>();

            if (adventurersId == null || GameManager.Instance == null || GameManager.Instance.Fact == null)
                return adventurers;

            foreach (var adventurerId in adventurersId)
            {
                var adventurer = GetOneAdventurerFromId(adventurerId);
                if (adventurer != null)
                    adventurers.Add(adventurer);
            }

            return adventurers;
        }

        public static AdventurerClass GetOneAdventurerFromId(Guid adventurerId)
        {
            if (GameManager.Instance.Fact.GetFact<List<AdventurerClass>>("my_adventurers") == null)
            {
                return null;
            }
            List<AdventurerClass> currentAdventurers = GameManager.Instance.Fact.GetFact<List<AdventurerClass>>("my_adventurers");
            return currentAdventurers.Find(adventurer => adventurer.ID == adventurerId);
        }

        public static List<Guid> GetIdFromAdventurers(List<AdventurerClass> adventurers)
        {
            List<Guid> adventurersId = new List<Guid>();
            foreach (var adventurer in adventurers)
            {
                adventurersId.Add(adventurer.ID);
            }
            return adventurersId;
        }

        public static Guid GetOneIdFromAdventurer(AdventurerClass adventurer)
        {
            List<AdventurerClass> currentAdventurers = GameManager.Instance.Fact.GetFact<List<AdventurerClass>>("my_adventurers");
            return currentAdventurers.Find(adv => adv.ID == adventurer.ID).ID;
        }
        
        #endregion
    }
}