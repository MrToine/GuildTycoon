using System;
using Core.Runtime;
using EventSystem.Runtime;
using UnityEngine;

namespace GameUI.Runtime
{
    public class BasePanel : BaseMonobehaviour
    {

        #region Publics

        //

        #endregion


        #region Unity API

        void Start()
        {
            PanelSignals.OnRefresh += Refresh;
        }

        void OnDestroy()
        {
            PanelSignals.OnRefresh -= Refresh;
        }

        #endregion


        #region Main Methods

        public void Refresh(string panelName)
        {
            Warning($"{panelName} -> Actualisé");
        }

        #endregion


        #region Utils

        /* Fonctions privées utiles */

        #endregion


        #region Privates and Protected

        // Variables privées

        #endregion
    }
}

