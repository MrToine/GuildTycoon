using System;
using Adventurer.Runtime;
using UnityEngine;
using Core.Runtime;
using EventSystem.Runtime;
using Random = UnityEngine.Random;

namespace GameUI.Runtime
{
    public class RecruitementPanel: BaseMonobehaviour
    {

        #region Publics

        public AdventurerFactorySO m_adventurersSO;
        
        #endregion


        #region Unity API

        private void OnEnable()
        {
            GenerateAdventurer(20);
        }

        private void OnDisable()
        {
            foreach (Transform child in transform)
            {
                AdventurerCardUI card = child.GetComponent<AdventurerCardUI>();
                if (card != null)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        #endregion


        #region Main Methods
        
        [ContextMenu("Effacer le container")] 
        public void Clear()
        {
            foreach (Transform child in transform)
            {
                AdventurerCardUI card = child.GetComponent<AdventurerCardUI>();
                if (card != null)
                {
                    DestroyImmediate(child.gameObject);
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
            int numbers = Random.Range(5, nbAdventurers);
            for (int i = 0; i < numbers; i++)
            {
                AdventurerClass newRecruit = m_adventurersSO.CreateAdventurer(System.Guid.NewGuid());
                
                DisplayHeroCard(newRecruit);
            }
        }

        private void DisplayHeroCard(AdventurerClass newRecruit)
        {
            GameObject adventurerGO = Instantiate(_adventurerPrefab, transform);
            adventurerGO.transform.SetAsLastSibling();
            AdventurerCardUI card = adventurerGO.GetComponent<AdventurerCardUI>();
            
            card.setup(newRecruit);
            
            string price = ((newRecruit.Agility + newRecruit.Defense + newRecruit.Intelligence + newRecruit.Strength) / 4 * 50).ToString();
            card.m_price.text = price;
            card.m_footer.gameObject.SetActive(true);
            
            card.m_buyButton.onClick.RemoveAllListeners();
            card.m_buyButton.onClick.AddListener(() => BuyHero(adventurerGO, newRecruit, int.Parse(price)));
        }

        private void BuyHero(GameObject go, AdventurerClass newRecruit, int price = 0)
        {
            Player.Runtime.PlayerClass playerClass = GetFact<Player.Runtime.PlayerClass>(GameManager.Instance.Profile);
            if (playerClass.AdventurersCount == playerClass.AdventurersMax)
            {
                Warning("Max des héros atteins");
                return;
            }
            if (playerClass.Money >= price)
            {
                playerClass.AdventurersCount += 1;
                playerClass.Money -= price;
                SetFact<AdventurerClass>(newRecruit.ID.ToString(), newRecruit, FactPersistence.Persistent);
                SaveFacts();
                AdventurerSignals.RaiseSpawnRequested(newRecruit);
                Destroy(go);
            }
        }

        #endregion


        #region Privates and Protected

        private int _nextId = 0;
        [SerializeField] GameObject _adventurerPrefab;

        #endregion
    }
}