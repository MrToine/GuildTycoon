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

        void Start()
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

                Dictionary<string, QuestStateEnum> acceptedQuestNames = new Dictionary<string, QuestStateEnum>();
                if (FactExists<Dictionary<string, QuestStateEnum>>("quests", out _))
                {
                    Info("Les quêtes existes");
                    acceptedQuestNames = GetFact<Dictionary<string, QuestStateEnum>>("quests");
                }

                foreach (var quest in availableTemplates)
                {
                    if (acceptedQuestNames != null && acceptedQuestNames.ContainsKey(quest.data.Name))
                        continue;

                    DisplayCard(quest);
                }
            }
            else
            {
                Info("Pas de quête ! :/");
                return;
            }
        }

        #endregion


        #region Main Methods

        // 

        #endregion


        #region Utils

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
