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
        [SerializeField] QuestFactoryDatabase _questDatabase;

        QuestClass _quest;

        public void SetQuest(string questName, QuestStateEnum stateQuest)
        {
            Info($"          18. 🚨 [InteractionQuestCard.cs] La quête {questName} est dans l'état : {stateQuest}.");
            QuestTemplate template = _questDatabase.GetTemplatesByTitle(questName);

            if (template != null)
            {
                _quest = template.ToQuestClass(stateQuest);
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
                Info($"         ℹ️[InteractionQuestCard.cs] La quête {_quest.Name} est dans l'état : {_quest.State}.");
                QuestSignals.RaiseInfoQuestPanel(_quest);
            }
        }
    }
}
