using System.Collections.Generic;
using UnityEngine;

namespace Core.Runtime
{
    public class UIManager : BaseMonobehaviour
    {

        #region Publics

        public void ShowPanel(GameObject panel)
        {
            panel.SetActive(true);
        }

        public void HideAllPanels()
        {
            foreach (var p in panels)
                p.SetActive(false);
        }

        #endregion


        #region Unity API

        //

        #endregion


        #region Main Methods

        // 

        #endregion


        #region Utils

        /* Fonctions privées utiles */

        #endregion


        #region Privates and Protected

        [SerializeField] private List<GameObject> panels;

        #endregion
    }
}

