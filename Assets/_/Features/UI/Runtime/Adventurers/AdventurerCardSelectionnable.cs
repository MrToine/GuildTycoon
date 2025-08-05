using System;
using Adventurer.Runtime;
using Core.Runtime;
using EventSystem.Runtime;
using Quests.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI.Runtime
{
    public class AdventurerCardSelectionnable : BaseMonobehaviour
    {

        #region Publics

        public Sprite m_selectedSprite;
        public Sprite m_unselectedSprite;
        public GameObject m_BGSprite;

        #endregion


        #region Unity API

        void Start()
        {
            _adventurer = GetComponent<AdventurerCardUI>().Adventurer;
        }

        void Update()
        {
            if (_isSelected)
            {
                m_BGSprite.GetComponent<Image>().sprite = m_selectedSprite;
            }
            else
            {
                m_BGSprite.GetComponent<Image>().sprite = m_unselectedSprite;
            }
        }

        #endregion


        #region Main Methods

        public void OnClick()
        {
            if (!QuestManager.Instance.CanSelectedAdventurers())
            {
                gameObject.GetComponent<Button>().interactable = false;
                return;
            }
            
            _isSelected = !_isSelected;
            if (_isSelected) 
                AdventurerSignals.RaiseAdventurerSelected(_adventurer);
            else
                AdventurerSignals.RaiseAdventurerUnselected(_adventurer);
        }

        public void OnPointerEnter()
        {
            if (!QuestManager.Instance.CanSelectedAdventurers()) return;
            transform.localScale = Vector3.one * 1.1f;
        }

        public void OnPointerExit()
        {
            transform.localScale = Vector3.one;
        }

        #endregion


        #region Utils

        /* Fonctions privées utiles */

        #endregion


        #region Privates and Protected

        bool _isSelected;
        AdventurerClass _adventurer;
        
        #endregion
    }
}

