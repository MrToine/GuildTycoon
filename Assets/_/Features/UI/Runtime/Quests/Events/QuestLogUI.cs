using Codice.CM.Common;
using Core.Runtime;
using TMPro;
using UnityEngine;

namespace GameUI.Runtime.Events
{
    public class QuestLogUI : BaseMonobehaviour
    {

        #region Publics

        //

        #endregion


        #region Unity API

        //

        #endregion


        #region Main Methods

        public void setDatas(int time, string description)
        {
            _timeLogLabel.text = $"{time.ToString()} secs.";
            description = $"{LocalizationSystem.Instance.GetLocalizedText(description)}";
            _descriptionLogLabel.text = description;
        }

        #endregion


        #region Utils

        /* Fonctions privées utiles */

        #endregion


        #region Privates and Protected

        // Variables privées
        [SerializeField] TMP_Text _timeLogLabel;
        [SerializeField] TMP_Text _descriptionLogLabel;

        #endregion
    }
}

