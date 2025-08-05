using System.Collections.Generic;
using Core.Runtime;
using EventSystem.Runtime;
using Quest.Runtime;
using Quests.Runtime;
using UnityEngine;

namespace GameUI.Runtime
{
    public class InteractionQuestCard : BaseMonobehaviour
    {
        [SerializeField] private QuestFactoryDatabase _questDatabase;

        private QuestClass _quest;

        public void SetQuest(string questName)
        {
            List<string> myQuests = GetFact<List<string>>("quests");

            QuestTemplate template = _questDatabase.GetTemplatesByTitle(questName);

            if (template != null)
            {
                _quest = template.ToQuestClass();
            }
            else
            {
                Warning($"La quête {questName} n'a pas été trouvée dans la base !");
            }
        }

        public void OnClick()
        {
            if (_quest != null)
            {
                QuestSignals.RaiseInfoQuestPanel(_quest);
            }
        }
    }
}
