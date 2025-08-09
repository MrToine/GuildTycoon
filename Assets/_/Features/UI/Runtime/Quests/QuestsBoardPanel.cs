using System;
using System.Collections.Generic;
using System.Linq;
using Core.Runtime;
using Player.Runtime;
using Quest.Runtime;
using Quests.Runtime;
using TMPro;
using UnityEngine;

namespace GameUI.Runtime
{
    public class QuestsBoardPanel : BaseMonobehaviour
    {

        #region Publics

        public QuestFactoryDatabase _questFactoryDatabase;

        #endregion


        #region Unity API
        
        void OnEnable()
        {
            _player = GetFact<PlayerClass>(GameManager.Instance.Profile);
        }

        /*void Start()
        {
            foreach (var txt in GetComponentsInChildren<TMP_Text>())
            {
                txt.text = LocalizationSystem.Instance.GetLocalizedText(txt.text);
            }

            var factory = _questFactoryDatabase.GetFactoryForLevel(_player.GuildLevel);

            if (factory != null)
            {
                var availableTemplates = factory.questTemplates
                    .Where(q => q.data.MinLevel <= _player.GuildLevel)
                    .ToList();

                List<QuestClass> acceptedQuestNames = new List<QuestClass>();
                if (FactExists<List<QuestClass>>("quests", out _))
                {
                    acceptedQuestNames = GetFact<List<QuestClass>>("quests");
                }

                foreach (var quest in availableTemplates)
                {
                    if (acceptedQuestNames != null && acceptedQuestNames.Any(q => q.ID == quest.data.ID))
                        continue;

                    DisplayCard(quest);
                }
            }
        }*/
        void Start()
        {
            InitializeLocalization();

            List<QuestTemplate> availableTemplates = GetAvailableQuests();
            
            List<QuestClass> acceptedQuests = GetAcceptedQuests();
            
            DisplayAvailableQuests(availableTemplates, acceptedQuests);
        }

        #endregion


        #region Main Methods

        // 

        #endregion


        #region Utils

        void InitializeLocalization()
        {
            foreach (var txt in GetComponentsInChildren<TMP_Text>())
            {
                txt.text = LocalizationSystem.Instance.GetLocalizedText(txt.text);
            }
        }

        List<QuestTemplate> GetAvailableQuests()
        {
            var factory = _questFactoryDatabase.GetFactoryForLevel(_player.GuildLevel);
            if (factory == null)
                return new List<QuestTemplate>();
            return factory.questTemplates
                .Where(q => q.data.MinLevel <= _player.GuildLevel)
                .ToList();
        }
        
        List<QuestClass> GetAcceptedQuests()
        {
            if (FactExists<List<QuestClass>>("quests", out _))
            {
                return GetFact<List<QuestClass>>("quests");
            }
            return new List<QuestClass>();
        }

        void DisplayAvailableQuests(List<QuestTemplate> availableTemplates, List<QuestClass> acceptedQuests)
        {
            foreach(var quest in availableTemplates)
            {
                if (acceptedQuests.Any(q => q.Name == quest.data.Name))
                    continue;
                DisplayCard(quest);
            }
        }
        
        void DisplayCard(QuestTemplate quest)
        {
            GameObject GO = Instantiate(_questCardPrefab, _panel.transform);
            QuestCardUI card = GO.GetComponent<QuestCardUI>();
            card.Setup(quest.ToQuestClass(QuestStateEnum.Disponible));
        }

        #endregion


        #region Privates and Protected

        PlayerClass _player;
        [SerializeField] GameObject _panel;
        [SerializeField] GameObject _questCardPrefab;

        #endregion
    }
}
