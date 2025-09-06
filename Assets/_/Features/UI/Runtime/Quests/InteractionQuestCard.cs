using System;
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

        public void SetQuest(QuestClass quest)
        {
            _quest = quest;
        }

        public void OnClick()
        {
            if (_quest != null)
            {
                QuestManager.Instance.CurrentQuest = _quest;
                Info($"⏱️MAJ de la current quest {QuestManager.Instance.CurrentQuest.Name}");
                QuestSignals.RaiseInfoQuestPanel(_quest);
            }
        }
    }
}
