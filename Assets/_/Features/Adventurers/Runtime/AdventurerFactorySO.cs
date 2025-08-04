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

        public AdventurerClass CreateAdventurer(Guid id)
        {
            string name = m_names[Random.Range(0, m_names.Count)];
            AdventurerClassEnum chosenClassEnum = m_classes[Random.Range(0, m_classes.Count)];

            int level = m_baseLevel;
            int xp = 0;
            int strength = Random.Range(m_minStat, m_maxStat);
            int defense = Random.Range(m_minStat, m_maxStat);
            int agility = Random.Range(m_minStat, m_maxStat);
            int intelligence = Random.Range(m_minStat, m_maxStat);

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
            
            return new AdventurerClass(id, name, chosenClassEnum, level, xp, strength, defense, agility, intelligence, modelParts, now);
        }
    }
}
