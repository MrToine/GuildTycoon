using System.Collections.Generic;
using EventSystem.Runtime;
using Adventurer.Runtime;
using Core.Runtime;
using Quests.Runtime;
using Quests.Runtime._.Features.Quests.Runtime;
using TMPro;
using UnityEngine;

namespace GameUI.Runtime
{
    public class AdventurersPanel : BaseMonobehaviour
    {
        #region Unity API

        private void Start()
        {
            AdventurerSignals.OnPortraitCaptured += OnPortraitCapturedHandler;
            AdventurerSignals.OnRefresh += DisplayAdventurers;
            
            foreach (var txt in GetComponentsInChildren<TMP_Text>())
            {
                txt.text = LocalizationSystem.Instance.GetLocalizedText(txt.text);
            }

            DisplayAdventurers();
        }


        private void OnDestroy()
        {
            AdventurerSignals.OnPortraitCaptured -= OnPortraitCapturedHandler;
            AdventurerSignals.OnRefresh -= DisplayAdventurers;

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

        void DisplayAdventurers()
        {
            foreach (Transform child in _heroesPanel.transform)
            {
                Destroy(child.gameObject);
            }

            List<AdventurerClass> filtered = FilterAdventurers(_sort);

            foreach (AdventurerClass adventurer in filtered)
            {
                GameObject cardAdventurerGO = Instantiate(_adventurerPrefab, _heroesPanel.transform);
                cardAdventurerGO.transform.SetAsLastSibling();
                AdventurerCardUI card = cardAdventurerGO.GetComponent<AdventurerCardUI>();
                card.setup(adventurer);
            }
        }

        List<AdventurerClass> FilterAdventurers(AdventurerSortEnum sortType)
        {
            List<AdventurerClass> allAdventurers = GetAllFactsOfType<AdventurerClass>();

            switch (sortType)
            {
                case AdventurerSortEnum.Available:
                    return allAdventurers.FindAll(a => a.IsAvailable);
                case AdventurerSortEnum.NotAvailable:
                    return allAdventurers.FindAll(a => !a.IsAvailable);
                case AdventurerSortEnum.None:
                    return allAdventurers;
                default:
                    return allAdventurers;
            }
        }

        #endregion
        
        #region Privates and Protected
        
        [SerializeField] GameObject _adventurerPrefab;
        [SerializeField] GameObject _heroesPanel;
        [SerializeField] AdventurerSortEnum _sort;
        
        #endregion
    }
}
