using System;
using System.Collections.Generic;
using UnityEngine;

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
        
        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }
        
        public int MaxHealth
        {
            get { return _maxHealth; }
            set { _maxHealth = value; }
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

        int _health;
        int _maxHealth;
        
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
            
            // Calcule de la vie Max
            _maxHealth = CalculateMaxHp();
            //Debug.Log($">>>>>>> {_name }[{_adventurerClassEnum}] | Niveau {_level} | {_experience} exp | {_strength} force | {_defense} def | {_agility} agi | {_intelligence} int | {_maxHealth} Hp Max <<<<<<<<<");
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

        int CalculateMaxHp()
        {
            float classMultiplier = GetClassMultiplier();
            return Mathf.RoundToInt((_strength + _defense + _level * 2) * classMultiplier);
        }
        
        private float GetClassMultiplier()
        {
            switch (_adventurerClassEnum)
            {
                case AdventurerClassEnum.Barbarian: return 1.5f;
                case AdventurerClassEnum.Warrior: return 1.3f;
                case AdventurerClassEnum.Paladin: return 1.2f;
                case AdventurerClassEnum.Archer: return 1.0f;
                case AdventurerClassEnum.Thief: return 0.9f;
                case AdventurerClassEnum.Priest: return 0.8f;
                default: return 1.0f;
            }
        }

        public void TakeDamage(int effectValue)
        {
            Debug.Log($"Taking damage: {effectValue}");
        }

        public void Heal(int effectValue)
        {
            throw new NotImplementedException();
        }

        public void ApplyBuff(int effectValue)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
