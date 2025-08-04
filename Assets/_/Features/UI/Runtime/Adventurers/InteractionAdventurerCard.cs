using System;
using Adventurer.Runtime;
using Core.Runtime;
using EventSystem.Runtime;
using UnityEngine;

namespace GameUI.Runtime
{
    public class InteractionAdventurerCard : BaseMonobehaviour
    {

        #region Publics

        //

        #endregion


        #region Unity API

        //

        #endregion


        #region Main Methods

        public void SetAdventurer(AdventurerClass adventurer)
        {
            _adventurer = adventurer;
        }

        public void OnClick()
        {
            AdventurerSignals.RaiseInfoAdventurerPanel(_adventurer);
        }

        #endregion


        #region Utils

        //

        #endregion


        #region Privates and Protected

        AdventurerClass _adventurer;

        #endregion
    }
}

