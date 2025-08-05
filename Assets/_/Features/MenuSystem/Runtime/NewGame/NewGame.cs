using System.Collections.Generic;
using System.Text.RegularExpressions;
using Adventurer.Runtime;
using Core.Runtime;
using Player.Runtime;
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
                SetFact<PlayerClass>(saveName, newPlayerClass, FactPersistence.Persistent);
                SetFact<GameTime>("game_time", new GameTime(), FactPersistence.Persistent);
                SetFact<List<AdventurerClass>>("my_adventurers", new List<AdventurerClass>(), FactPersistence.Persistent);
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

