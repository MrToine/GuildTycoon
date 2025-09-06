using System;
using System.Collections;
using System.Collections.Generic;
using Adventurer.Runtime;
using UnityEngine;
using Core.Runtime;
using EventSystem.Runtime;
using Random = UnityEngine.Random;
using UnityEngine.UI;

namespace GameUI.Runtime
{
    public class RecruitementPanel: BaseMonobehaviour
    {

        #region Publics

        public AdventurerFactorySO m_adventurersSO;
        
        #endregion


        #region Unity API

        void OnEnable()
        {
            if (_content == null)
            {
                _content = GetComponent<RectTransform>();
            }
            StartCoroutine(OpenRoutine());
        }

        void OnDisable()
        {
            // Intentionally left empty during debug to validate first-open rendering.
            // (We avoid destroying children here to keep the list intact between toggles.)
        }

        IEnumerator OpenRoutine()
        {
            // Defer one frame to let Canvas/Scaler/Mask finish their activation cycle
            yield return null;

            // Generate only if empty (prevents duplicate spawns on reopen)
            if (_content != null && _content.childCount == 0)
            {
                Clear();
                GenerateAdventurer(20);
            }

            // Wait until end of frame to ensure instantiated elements are present before forcing layout
            yield return new WaitForEndOfFrame();

            Canvas.ForceUpdateCanvases();
            if (_content != null)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(_content);
                // Also rebuild the parent (Viewport) if present to ensure mask/scroll region updates
                var parentRT = _content.parent as RectTransform;
                if (parentRT != null)
                {
                    LayoutRebuilder.ForceRebuildLayoutImmediate(parentRT);
                }
            }
            Canvas.ForceUpdateCanvases();
        }

        #endregion


        #region Main Methods
        
        [ContextMenu("Effacer le container")] 
        public void Clear()
        {
            if (_content == null)
            {
                _content = GetComponent<RectTransform>();
            }

            if (_content == null) return;

            foreach (Transform child in _content)
            {
                AdventurerCardUI card = child.GetComponent<AdventurerCardUI>();
                if (card != null)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        [ContextMenu("Générer un aventurier")]
        public void Generate()
        {
            GenerateAdventurer();
        }

        #endregion


        #region Utils

        public void GenerateAdventurer(int nbAdventurers = 1)
        {
            if (_content == null)
            {
                _content = GetComponent<RectTransform>();
            }

            Info($"Génération de {nbAdventurers} aventuriers");

            for (int i = 0; i < nbAdventurers; i++)
            {
                AdventurerClass newRecruit = m_adventurersSO.CreateAdventurer();
                Info($"Aventurier n°{i}/{nbAdventurers}. Nom : {newRecruit.Name}");
                DisplayHeroCard(newRecruit);
            }

            // Ensure the first-time open displays items correctly
            StartCoroutine(RefreshLayoutNextFrame());
        }

        void DisplayHeroCard(AdventurerClass newRecruit)
        {
            if (_content == null)
            {
                _content = GetComponent<RectTransform>();
            }

            GameObject adventurerGO = Instantiate(_adventurerPrefab, _content != null ? _content : transform);
            adventurerGO.transform.SetAsLastSibling();
            AdventurerCardUI card = adventurerGO.GetComponent<AdventurerCardUI>();
            
            card.setup(newRecruit);
            
            string price = ((newRecruit.Agility + newRecruit.Defense + newRecruit.Intelligence + newRecruit.Strength) / 4 * 50).ToString();
            card.m_price.text = price;
            card.m_footer.gameObject.SetActive(true);
            
            card.m_buyButton.onClick.RemoveAllListeners();
            card.m_buyButton.onClick.AddListener(() => BuyHero(adventurerGO, newRecruit, int.Parse(price)));
        }

        IEnumerator RefreshLayoutNextFrame()
        {
            yield return new WaitForEndOfFrame();
            Canvas.ForceUpdateCanvases();
            if (_content != null)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(_content);
            }
            Canvas.ForceUpdateCanvases();
        }

        void BuyHero(GameObject go, AdventurerClass newRecruit, int price = 0)
        {
            Player.Runtime.PlayerClass playerClass = GetFact<Player.Runtime.PlayerClass>(GameManager.Instance.Profile);
            if (playerClass.AdventurersCount == playerClass.AdventurersMax)
            {
                return;
            }
            if (playerClass.Money >= price)
            {
                playerClass.AdventurersCount += 1;
                playerClass.Money -= price;
                if (!FactExists<List<AdventurerClass>>("my_adventurers", out _))
                {
                    SetFact<List<AdventurerClass>>("my_adventurers", new List<AdventurerClass>(), FactPersistence.Persistent);
                }
                List<AdventurerClass> myAdventurers = GetFact<List<AdventurerClass>>("my_adventurers");
                myAdventurers.Add(newRecruit);
                SaveFacts();
                _photoStudio.SetActive(true);
                AdventurerSignals.RaiseSpawnRequested(newRecruit);
                AdventurerSignals.RaiseRefreshAdventurers();
                Destroy(go);
            }
        }

        #endregion


        #region Privates and Protected

        [SerializeField] RectTransform _content; // Assign this to ScrollView/Viewport/Content in the inspector
        private int _nextId = 0;
        [SerializeField] GameObject _adventurerPrefab;
        [SerializeField] GameObject _photoStudio;

        #endregion
    }
}