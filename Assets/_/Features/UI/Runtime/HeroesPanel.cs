using System.Collections.Generic;
using EventSystem.Runtime;
using Adventurer.Runtime;
using Core.Runtime;
using TMPro;
using UnityEngine;

namespace GameUI.Runtime
{
    public class HeroesPanel : BaseMonobehaviour
    {
        #region Unity API

        private void Start()
        {
            AdventurerSignals.OnAdventurerSpawnRequested += OnHeroRecruitedHandler;
            AdventurerSignals.OnPortraitCaptured += OnPortraitCapturedHandler;
            
            foreach (var txt in GetComponentsInChildren<TMP_Text>())
            {
                txt.text = LocalizationSystem.Instance.GetLocalizedText(txt.text);
            }

            List<AdventurerClass> adventurers = GetAllFactsOfType<AdventurerClass>();
            foreach (AdventurerClass adventurer in adventurers)
            {
                    OnHeroRecruitedHandler(adventurer);
            }
        }


        private void OnDestroy()
        {
            AdventurerSignals.OnAdventurerSpawnRequested -= OnHeroRecruitedHandler;
            AdventurerSignals.OnPortraitCaptured -= OnPortraitCapturedHandler;
        }

        #endregion


        #region Utils
        void OnPortraitCapturedHandler(AdventurerClass adventurer, Sprite portrait)
        {
            Info("Portrait ;D");
            foreach (Transform child in _heroesPanel.transform)
            {
                AdventurerCardUI card = child.GetComponent<AdventurerCardUI>();
                if (card != null && card.Adventurer == adventurer)
                {
                    card.SetPortrait(portrait);
                    break;
                }
            }
        }

        private void OnHeroRecruitedHandler(AdventurerClass newRecruit)
        {
            GameObject cardAdventurerGO = Instantiate(_adventurerPrefab, _heroesPanel.transform);
            cardAdventurerGO.transform.SetAsLastSibling();
            AdventurerCardUI card = cardAdventurerGO.GetComponent<AdventurerCardUI>();
            card.setup(newRecruit);
        }

        #endregion
        
        #region Privates and Protected
        
        [SerializeField] GameObject _adventurerPrefab;
        [SerializeField] GameObject _heroesPanel;
        
        #endregion
    }
}
