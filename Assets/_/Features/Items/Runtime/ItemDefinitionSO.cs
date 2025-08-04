using System.Collections;
using UnityEngine;

namespace Item.Runtime
{
    [CreateAssetMenu(fileName = "New Item Definition", menuName = "Guild Tycoon/Items/Generation", order = 0)]
    public class ItemDefinitionSO : ScriptableObject
    {
        public Sprite m_icon;
        public string m_name;
        public string m_description;
        public int m_value;
        public ItemRarityEnum m_rarity;
        public ItemTypeEnum m_type;
        public float m_baseEffectValue;
    }
}
