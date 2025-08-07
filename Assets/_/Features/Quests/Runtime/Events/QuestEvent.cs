using System;
using System.Collections.Generic;
using System.Linq;
using Adventurer.Runtime;
using Core.Runtime;
using UnityEngine;

namespace Quests.Runtime
{
    [Serializable]
    public class QuestEvent
    {
        #region Parameters

        string _id;
        [SerializeField] string _descriptionKey;

        [SerializeField] QuestEventType type;
        [SerializeField] List<EnvironmentTag> environments;
        [SerializeField] List<EnemyTag> enemyTags;

        [SerializeField] int minLevel;
        [SerializeField] int minTimeTrigger;
        [SerializeField] int maxTimeTrigger;
        [SerializeField]  float percentTrigger;

        [SerializeField] List<EventEffect> effects;
        [SerializeField] TargetingType targetingTypes;

        #endregion

        // Optionnel : getters publics si tu veux exposer ça sans accès direct à la modif
        public string Id => _id;
        public string DescriptionKey => _descriptionKey;
        public QuestEventType Type => type;
        public List<EnvironmentTag> Environments => environments;
        public List<EnemyTag> EnemyTags => enemyTags;
        public int MinLevel => minLevel;
        public int MinTimeTrigger => minTimeTrigger;
        public int MaxTimeTrigger => maxTimeTrigger;
        public float PercentTrigger => percentTrigger;
        public List<EventEffect> Effects => effects;
        
        public List<AdventurerClass> GetTargets(List<Guid> assignedAdventurersById)
        {
            List<AdventurerClass> assignedAdventurers = QuestClass.GetAdventurersFromId(assignedAdventurersById);
            switch (targetingTypes)
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
