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

        #region Publics

        //

        #endregion


        #region Unity API

        //

        #endregion


        #region Main Methods

        public void SetQuest(string questName)
        {
            List<string> myQuests = GetFact<List<string>>("quests"); 
            Info("On parcours la liste des quêtes ...");
            QuestTemplate template = _questDatabase.GetTemplatesByTitle(questName);

            if (template != null)
            {
                Info("Quête trouvée");
                _quest = template.ToQuestClass();
                Info($"Titre > {_quest.Name}");
            }
            else
            {
                Warning($"La quête {questName} n'a pas été trouvée dans la base !");
            }
        }

        public void OnClick()
        {
            QuestSignals.RaiseInfoQuestPanel(_quest);
        }

        #endregion


        #region Utils

        /* Fonctions privées utiles */

        #endregion


        #region Privates and Protected

        [SerializeField] QuestFactoryDatabase _questDatabase;
        QuestClass _quest;

        #endregion
    }
}

