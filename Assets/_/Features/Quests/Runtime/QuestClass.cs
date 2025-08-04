using System;
using System.Collections.Generic;
using Adventurer.Runtime;
using Goals.Runtime;
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

        public float Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        public QuestDifficultyEnum Difficulty
        {
            get { return _difficulty; }
            set { _difficulty = value; }
        }

        public List<string> Rewards
        {
            get { return _rewards; }
            set { _rewards = new List<string>(); }
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

        public List<AdventurerClass> AssignedAdventurers
        {
            get
            {
                return _assignedAdventurers;
            }
            set
            {
                _assignedAdventurers = value;
            }
        }

        public GoalSystem Goals;

        #endregion
        
        #region Parameters

        Guid _id;
        string _name;
        string _description;
        string _objective;
        float _duration;
        QuestDifficultyEnum _difficulty;
        List<string> _rewards;
        QuestStateEnum _state;
        int _minLevel;
        List<AdventurerClass> _assignedAdventurers;

        public QuestClass(Guid id, string name, string description, string objective, float duration, QuestDifficultyEnum difficulty, List<string> reward, int minLevel = 1)
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
    }
}

