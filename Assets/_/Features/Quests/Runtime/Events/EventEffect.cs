using System;

namespace Quests.Runtime
{
    public enum EffectType
    {
        Damage,
        Heal,
        Gold,
        Buff,
        Debuff,
        ItemLoss,
        Custom
    }
    
    public enum TargetingType
    {
        RandomHero,
        AllHeroes,
        LowestHp,
        HighestHp,
        LowestMana,
        HighestMana,
        LowestDefense,
        HighestDefense,
        LowestXp,
        HighestXp,
        SpecificClass,
        None
    }
    
    [Serializable]
    public struct EventEffect
    {
        public EffectType Type;
        public int Value;
        public TargetingType Target;
        public string ExtraData;
    }

}
