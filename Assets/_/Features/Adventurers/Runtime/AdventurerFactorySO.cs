using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Adventurer.Runtime
{
    [CreateAssetMenu(fileName = "AdventurerClass Factory", menuName = "Guild Tycoon/Adventurers/Factory", order = 0)]
    public class AdventurerFactorySO : ScriptableObject
    {
        public List<string> m_names;
        public List<AdventurerClassEnum> m_classes;
        public int m_baseLevel;
        public int m_minStat;
        public int m_maxStat;

        public AdventurerClass CreateAdventurer()
        {
            string name = m_names[Random.Range(0, m_names.Count)];
            AdventurerClassEnum chosenClassEnum = m_classes[Random.Range(0, m_classes.Count)];

            int level = m_baseLevel;
            int xp = 0;
            
            int strength = 0;
            int defense = 0;
            int agility = 0;
            int intelligence = 0;
            
            switch (chosenClassEnum)
            {
                case AdventurerClassEnum.Warrior:
                    strength = Random.Range(14, 20);
                    defense = Random.Range(10, 16);
                    agility = Random.Range(6, 14);
                    intelligence = Random.Range(4, 8);
                    break;
                case AdventurerClassEnum.Mage:
                    strength = Random.Range(4, 8);
                    defense = Random.Range(6, 10);
                    agility = Random.Range(6, 10);
                    intelligence = Random.Range(14, 20);
                    break;
                case AdventurerClassEnum.Archer:
                    strength = Random.Range(4, 8);
                    defense = Random.Range(6, 10);
                    agility = Random.Range(14, 20);
                    intelligence = Random.Range(4, 8);
                    break;
                case AdventurerClassEnum.Paladin:
                    strength = Random.Range(10, 14);
                    defense = Random.Range(14, 20);
                    agility = Random.Range(4, 8);
                    intelligence = Random.Range(8, 12);
                    break;
                case AdventurerClassEnum.Priest:
                    strength = Random.Range(6, 10);
                    defense = Random.Range(8, 12);
                    agility = Random.Range(8, 12);
                    intelligence = Random.Range(14, 20);
                    break;
                case AdventurerClassEnum.Thief:
                    strength = Random.Range(8, 14);
                    defense = Random.Range(8, 12);
                    agility = Random.Range(14, 20);
                    intelligence = Random.Range(4, 8);
                    break;
                case AdventurerClassEnum.Barbarian:
                    strength = Random.Range(16, 20);
                    defense = Random.Range(10, 14);
                    agility = Random.Range(6, 10);
                    intelligence = Random.Range(2, 6);
                    break;
                default:
                    // Classe non gérée
                    break;
            }

            var modelParts = new Dictionary<string, string>();
            modelParts["Ears"] = $"EARS/Ears Type {Random.Range(1, 2)}";
            modelParts["Eyebrows"] = $"EYEBROWS/Eyebrow Type {Random.Range(1, 5)} Color {Random.Range(1, 5)}";
            modelParts["Eyes"] = $"EYES/Eyes Type {Random.Range(1, 5)} Color {Random.Range(1, 5)}";
            modelParts["Face Hair"] = $"FACE HAIRS/Face Hair Type {Random.Range(1, 5)} Color {Random.Range(1, 5)}";
            modelParts["Hair"] = $"HAIRS/Hair Type {Random.Range(1, 5)} Color {Random.Range(1, 5)}";
            modelParts["Nose"] = $"NOSES/Nose Type {Random.Range(1, 5)}";
            
            modelParts["Feet Armor"] = $"FEETS/Feet Armor Type {Random.Range(1, 5)} Color {Random.Range(1, 3)}";
            modelParts["Legs Armor"] = $"LEGS/Legs Armor Type {Random.Range(1, 5)} Color {Random.Range(1, 3)}";
            modelParts["Belts Armor"] = $"BELTS/Belts Armor Type {Random.Range(1, 6)} Color {Random.Range(1, 3)}";
            modelParts["Arm Armor"] = $"ARMORS/Arm Armor Type {Random.Range(1, 5)} Color {Random.Range(1, 3)}";
            modelParts["Chest Armor"] = $"CHESTS/Chest Armor Type {Random.Range(1, 5)} Color {Random.Range(1, 3)}";
            modelParts["Head Armor"] = $"HEADS/Head Armor Type {Random.Range(1, 6)} Color {Random.Range(1, 3)}";
            
            DateTime now = DateTime.Now;
            
            return new AdventurerClass(Guid.NewGuid(), name, chosenClassEnum, level, xp, strength, defense, intelligence, agility, modelParts, now);
        }
    }
}
