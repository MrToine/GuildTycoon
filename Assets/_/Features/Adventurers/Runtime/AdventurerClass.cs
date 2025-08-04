using System;
using System.Collections.Generic;

namespace Adventurer.Runtime
{
    public class AdventurerClass
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

        public AdventurerClassEnum AdventurerClassEnum
        {
            get { return _adventurerClassEnum; }
        }

        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public int Experience
        {
            get { return _experience; }
            set { _experience = value; }
        }

        public int Strength
        {
            get { return _strength; }
            set { _strength = value; }
        }

        public int Defense
        {
            get { return _defense; }
            set { _defense = value; }
        }

        public int Intelligence
        {
            get { return _intelligence; }
            set { _intelligence = value; }
        }

        public int Agility
        {
            get { return _agility; }
            set { _agility = value; }
        }

        public bool IsAvailable
        {
            get { return _isAvailable; }
            set { _isAvailable = value; }
        }

        public DateTime RecruitmentDate
        {
            get { return _recruitmentDate; }
            set { _recruitmentDate = value; }
        }

        public Dictionary<string, string> Equipments
        {
            get { return _equipments; }
            set { _equipments = value; }
        }

        public Dictionary<string, string> ModelParts { get; private set; }
        
        #endregion
        
        #region Parameters
        
        Guid _id;
        string _name;
        AdventurerClassEnum _adventurerClassEnum;
        int _level;
        int _experience;
        int _strength;
        int _defense;
        int _intelligence;
        int _agility;

        bool _isAvailable;
        DateTime _recruitmentDate;
        Dictionary<string, string> _equipments;
        
        #endregion

        #region Constructor
        
        public AdventurerClass(Guid id, string name, AdventurerClassEnum adventurerClassEnum, int level, int experience,
            int strength, int defense,
            int intelligence, int agility, Dictionary<string, string> modelParts, DateTime RecruitmentDate = default(DateTime))
        {
            _id = id;
            _name = name;
            _adventurerClassEnum = adventurerClassEnum;
            _level = level;
            _experience = experience;
            _strength = strength;
            _defense = defense;
            _intelligence = intelligence;
            _agility = agility;
            ModelParts = modelParts;
            _recruitmentDate = RecruitmentDate;
            _isAvailable = true;
            _equipments = new Dictionary<string, string>();
        }
        
        #endregion
        
        #region Methods

        public void SendOnMission()
        {
            _isAvailable = false;
        }

        public void ReturnFromMission()
        {
            _isAvailable = true;
        }

        public void ReceiveXP(int xp)
        {
            _experience += xp;
        }

        private void TryLevelUp()
        {
            int requiredXP = _level * 100;
            if (_experience >= requiredXP)
            {
                _level++;
                _experience -= requiredXP;
            }
        }
        
        #endregion
    }
}
