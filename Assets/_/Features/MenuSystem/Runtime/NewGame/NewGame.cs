using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Adventurer.Runtime;
using Core.Runtime;
using Player.Runtime;
using Quests.Runtime;
using TMPro;
using UnityEngine;

namespace MenuSystem.Runtime
{
    public class NewGame : BaseMonobehaviour
    {

        #region Publics

        //

        #endregion


        #region Unity API
        
        //

        #endregion


        #region Main Methods
        
        public void CreateProfile()
        {
            if (_guildName.text != "")
            {
                string rawName = _guildName.text;
                string cleanedName = CleanInput(rawName);
                PlayerClass newPlayerClass = new PlayerClass(cleanedName, 1, 1000, 5);
                string saveName = newPlayerClass.GuildName.Replace(" ", "_");

                /*  Creation du FakeProfile "Continuer" */
                SetFact<string>("profile", saveName, FactPersistence.Persistent);;
                SaveFacts("continue");
                RemoveFact<string>("profile");
                
                /* Création du profile joueur */
                SetFact<PlayerClass>(saveName, newPlayerClass, FactPersistence.Persistent);
                SetFact<GameTime>("game_time", new GameTime(), FactPersistence.Persistent);
                SetFact<List<AdventurerClass>>("my_adventurers", new List<AdventurerClass>(), FactPersistence.Persistent);
                SetFact<List<QuestClass>>("quests", new List<QuestClass>(), FactPersistence.Persistent);
                SetFact<List<QuestClass>>("active_quests", new List<QuestClass>(), FactPersistence.Persistent);
                SetFact<List<QuestClass>>("completed_quests", new List<QuestClass>(), FactPersistence.Persistent);
                SetFact<Dictionary<Guid,  List<QuestEventLog>>>("events_quests_history", new Dictionary<Guid, List<QuestEventLog>>(), FactPersistence.Persistent);
                GameManager.Instance.Profile = saveName;
                SaveFacts();
                
                
                
                SceneLoader.Instance.LoadScene("Game");
            }
        }

        #endregion


        #region Utils

        string CleanInput(string input)
        {
            return Regex.Replace(input.Trim(), @"[\u200B-\u200D\uFEFF]", ""); // supprime les espaces invisibles
        }

        #endregion


        #region Privates and Protected

        [SerializeField] private TextMeshProUGUI _guildName;

        #endregion
    }
}

