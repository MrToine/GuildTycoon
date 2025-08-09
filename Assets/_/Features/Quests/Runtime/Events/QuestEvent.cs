using System;
using System.Collections.Generic;
using System.Linq;
using Adventurer.Runtime;
using Core.Runtime;
using Newtonsoft.Json;
using UnityEngine;

namespace Quests.Runtime
{
    [Serializable]
    public class QuestEvent
    {
        #region Parameters
        
        Guid _id;
        [SerializeField] string _descriptionKey;

        [SerializeField] QuestEventType _type;
        [SerializeField] List<EnvironmentTag> _environments;
        [SerializeField] List<EnemyTag> _enemyTags;

        [SerializeField] int _minLevel;
        [SerializeField] int _minTimeTrigger;
        [SerializeField] int _maxTimeTrigger;
        [SerializeField]  float _percentTrigger;

        [SerializeField] List<EventEffect> _effects;
        [SerializeField] TargetingType _targetingTypes;

        int _time;

        #endregion

        // Optionnel : getters publics si tu veux exposer ça sans accès direct à la modif
        public Guid Id => _id;
        [JsonIgnore] public string DescriptionKey => _descriptionKey;
        [JsonIgnore] public QuestEventType Type => _type;
        [JsonIgnore] public List<EnvironmentTag> Environments => _environments;
        [JsonIgnore] public List<EnemyTag> EnemyTags => _enemyTags;
        [JsonIgnore]public int MinLevel => _minLevel;
        [JsonIgnore]public int MinTimeTrigger => _minTimeTrigger;
        [JsonIgnore]public int MaxTimeTrigger => _maxTimeTrigger;
        [JsonIgnore]public float PercentTrigger => _percentTrigger;
        [JsonIgnore] public List<EventEffect> Effects => _effects;
        [JsonIgnore] public TargetingType TargetingTypes => _targetingTypes;

        public int Time
        {
            get { return _time; }
            set { _time = value; }
        }

        public QuestEvent(Guid id, QuestEventSO data, int time)
        {
            _id = id;
            _descriptionKey = data.m_data.DescriptionKey;
            _environments = data.m_data.Environments;
            _enemyTags = data.m_data.EnemyTags;
            _minLevel = data.m_data.MinLevel;
            _minTimeTrigger = data.m_data.MinTimeTrigger;
            _maxTimeTrigger = data.m_data.MaxTimeTrigger;
            _percentTrigger = data.m_data.PercentTrigger;
            _effects = data.m_data.Effects;
            _targetingTypes = data.m_data.TargetingTypes;
            _time = time;
        }
        
        public List<AdventurerClass> GetTargets(List<Guid> assignedAdventurersById)
        {
            List<AdventurerClass> assignedAdventurers = QuestClass.GetAdventurersFromId(assignedAdventurersById);
            switch (_targetingTypes)
            {
                case TargetingType.AllHeroes:
                    return assignedAdventurers;
                case TargetingType.RandomHero:
                    return new List<AdventurerClass> { assignedAdventurers[UnityEngine.Random.Range(0, assignedAdventurers.Count)] };
                case TargetingType.LowestHp:
                    return assignedAdventurers
                        .OrderBy(adventurer => adventurer.Health)
                        .Take(1)
                        .ToList();
                case TargetingType.None:
                default:
                    return new List<AdventurerClass>();
            }
        }
    }
}
