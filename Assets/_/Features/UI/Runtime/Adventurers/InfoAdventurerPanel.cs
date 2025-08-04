using Adventurer.Runtime;
using Core.Runtime;
using TMPro;
using UnityEngine;

namespace GameUI.Runtime
{
    public class InfoAdventurerPanel : BaseMonobehaviour
    {

        #region Publics

        public TextMeshProUGUI m_name;
        public TextMeshProUGUI m_level;
        public TextMeshProUGUI m_adventurerClass;
        public TextMeshProUGUI m_strenght;
        public TextMeshProUGUI m_defense;
        public TextMeshProUGUI m_agility;
        public TextMeshProUGUI m_intelligence;
        public TextMeshProUGUI m_isAvailable;

        #endregion


        #region Unity API

        //

        #endregion


        #region Main Methods
        
        public void ShowInfo(AdventurerClass adventurer)
        {
            m_name.text = adventurer.Name;
            m_level.text = $"Level {adventurer.Level.ToString()}";
            m_adventurerClass.text = adventurer.AdventurerClassEnum.ToString();
            m_strenght.text = adventurer.Strength.ToString();
            m_defense.text = adventurer.Defense.ToString();
            m_agility.text = adventurer.Agility.ToString();
            m_intelligence.text = adventurer.Intelligence.ToString();

            if (adventurer.IsAvailable)
            {
                m_isAvailable.color = new Color(51, 171, 32);
                m_isAvailable.text = LocalizationSystem.Instance.GetLocalizedText("in_qg");
            }
            else
            {
                m_isAvailable.color = new Color(171, 32, 32);
                m_isAvailable.text = LocalizationSystem.Instance.GetLocalizedText("in_quest");
            }
        }
        
        #endregion


        #region Utils

        //

        #endregion


        #region Privates and Protected

        //

        #endregion
    }
}
