using Adventurer.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI.Runtime
{
    public class AdventurerCardUI : MonoBehaviour
    {
        #region Publics
        
        [Header("UI References")]
        public TMPro.TextMeshProUGUI m_name;
        public TMPro.TextMeshProUGUI m_class;
        //public TMPro.TextMeshProUGUI m_traits;
        
        //public TMPro.TextMeshProUGUI m_experience;
        public TMPro.TextMeshProUGUI m_level;
        
        public TMPro.TextMeshProUGUI m_strength;
        public TMPro.TextMeshProUGUI m_defense;
        public TMPro.TextMeshProUGUI m_agility;
        public TMPro.TextMeshProUGUI m_intelligence;
        public Image m_portrait;
        
        [Header("Only Shop")]
        public Button m_buyButton;
        public GameObject m_footer;
        public TMPro.TextMeshProUGUI m_price;
        
        AdventurerClass _adventurer;

        #endregion

        public AdventurerClass Adventurer => _adventurer;

        public void setup(AdventurerClass adventurerClass)
        {
            _adventurer = adventurerClass;
            m_name.text = adventurerClass.Name;
            m_class.text = adventurerClass.AdventurerClassEnum.ToString();
            m_level.text = adventurerClass.Level.ToString();
            m_strength.text = adventurerClass.Strength.ToString();
            m_defense.text = adventurerClass.Defense.ToString();
            m_agility.text = adventurerClass.Agility.ToString();
            m_intelligence.text = adventurerClass.Intelligence.ToString();

            var interaction = GetComponent<InteractionAdventurerCard>();
            if (interaction != null)
                interaction.SetAdventurer(adventurerClass);
        }
        
        public void SetPortrait(Sprite portrait)
        {
            if (m_portrait != null)
                m_portrait.sprite = portrait;
        }
    }
}
